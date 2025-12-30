// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnnualHealthScreeningReminder.Api.Features.Reminders.Commands;
using AnnualHealthScreeningReminder.Core;
using AnnualHealthScreeningReminder.Infrastructure.Data;
using FluentAssertions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AnnualHealthScreeningReminder.Api.Tests.Features.Reminders;

[TestFixture]
public class CreateReminderTests
{
    private AnnualHealthScreeningReminderContext _context;
    private CreateReminder.Handler _handler;
    private CreateReminder.Validator _validator;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<AnnualHealthScreeningReminderContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AnnualHealthScreeningReminderContext(options);
        _validator = new CreateReminder.Validator();
        _handler = new CreateReminder.Handler(_context, _validator);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Test]
    public async Task Handle_ValidCommand_CreatesReminder()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var screeningId = Guid.NewGuid();
        var reminderDate = DateTime.UtcNow.AddDays(7);
        var command = new CreateReminder.Command(
            userId,
            screeningId,
            reminderDate,
            "Time for your annual physical exam");

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.Should().NotBeNull();
        result.ReminderId.Should().NotBeEmpty();
        result.UserId.Should().Be(userId);
        result.ScreeningId.Should().Be(screeningId);
        result.Message.Should().Be("Time for your annual physical exam");
        result.IsSent.Should().BeFalse();

        var reminder = await _context.Reminders.FindAsync(result.ReminderId);
        reminder.Should().NotBeNull();
        reminder!.Message.Should().Be("Time for your annual physical exam");
    }

    [Test]
    public void Validator_EmptyUserId_ThrowsValidationException()
    {
        // Arrange
        var command = new CreateReminder.Command(
            Guid.Empty,
            Guid.NewGuid(),
            DateTime.UtcNow.AddDays(7),
            "Reminder message");

        // Act & Assert
        var act = async () => await _handler.Handle(command, default);
        act.Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public void Validator_EmptyScreeningId_ThrowsValidationException()
    {
        // Arrange
        var command = new CreateReminder.Command(
            Guid.NewGuid(),
            Guid.Empty,
            DateTime.UtcNow.AddDays(7),
            "Reminder message");

        // Act & Assert
        var act = async () => await _handler.Handle(command, default);
        act.Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task Handle_NewReminder_IsSentIsFalse()
    {
        // Arrange
        var command = new CreateReminder.Command(
            Guid.NewGuid(),
            Guid.NewGuid(),
            DateTime.UtcNow.AddDays(7),
            "Reminder message");

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.IsSent.Should().BeFalse();
    }
}
