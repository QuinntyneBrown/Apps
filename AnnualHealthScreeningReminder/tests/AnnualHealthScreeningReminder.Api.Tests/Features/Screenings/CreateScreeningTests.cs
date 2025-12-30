// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnnualHealthScreeningReminder.Api.Features.Screenings.Commands;
using AnnualHealthScreeningReminder.Core;
using AnnualHealthScreeningReminder.Infrastructure.Data;
using FluentAssertions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AnnualHealthScreeningReminder.Api.Tests.Features.Screenings;

[TestFixture]
public class CreateScreeningTests
{
    private AnnualHealthScreeningReminderContext _context;
    private CreateScreening.Handler _handler;
    private CreateScreening.Validator _validator;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<AnnualHealthScreeningReminderContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AnnualHealthScreeningReminderContext(options);
        _validator = new CreateScreening.Validator();
        _handler = new CreateScreening.Handler(_context, _validator);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Test]
    public async Task Handle_ValidCommand_CreatesScreening()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var command = new CreateScreening.Command(
            userId,
            ScreeningType.PhysicalExam,
            "Annual Physical Exam",
            12,
            DateTime.UtcNow.AddMonths(-6),
            "Dr. Smith",
            "Regular checkup");

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.Should().NotBeNull();
        result.ScreeningId.Should().NotBeEmpty();
        result.UserId.Should().Be(userId);
        result.Name.Should().Be("Annual Physical Exam");
        result.ScreeningType.Should().Be(ScreeningType.PhysicalExam);
        result.RecommendedFrequencyMonths.Should().Be(12);

        var screening = await _context.Screenings.FindAsync(result.ScreeningId);
        screening.Should().NotBeNull();
        screening!.Name.Should().Be("Annual Physical Exam");
    }

    [Test]
    public void Validator_EmptyUserId_ThrowsValidationException()
    {
        // Arrange
        var command = new CreateScreening.Command(
            Guid.Empty,
            ScreeningType.PhysicalExam,
            "Annual Physical Exam",
            12,
            null,
            "Dr. Smith",
            "Regular checkup");

        // Act & Assert
        var act = async () => await _handler.Handle(command, default);
        act.Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public void Validator_EmptyName_ThrowsValidationException()
    {
        // Arrange
        var command = new CreateScreening.Command(
            Guid.NewGuid(),
            ScreeningType.PhysicalExam,
            "",
            12,
            null,
            "Dr. Smith",
            "Regular checkup");

        // Act & Assert
        var act = async () => await _handler.Handle(command, default);
        act.Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public void Validator_InvalidFrequency_ThrowsValidationException()
    {
        // Arrange
        var command = new CreateScreening.Command(
            Guid.NewGuid(),
            ScreeningType.PhysicalExam,
            "Annual Physical Exam",
            0,
            null,
            "Dr. Smith",
            "Regular checkup");

        // Act & Assert
        var act = async () => await _handler.Handle(command, default);
        act.Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task Handle_WithLastScreeningDate_CalculatesNextDueDate()
    {
        // Arrange
        var lastScreeningDate = DateTime.UtcNow.AddMonths(-6);
        var command = new CreateScreening.Command(
            Guid.NewGuid(),
            ScreeningType.PhysicalExam,
            "Annual Physical Exam",
            12,
            lastScreeningDate,
            "Dr. Smith",
            "Regular checkup");

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.NextDueDate.Should().NotBeNull();
        result.NextDueDate!.Value.Date.Should().Be(lastScreeningDate.AddMonths(12).Date);
    }
}
