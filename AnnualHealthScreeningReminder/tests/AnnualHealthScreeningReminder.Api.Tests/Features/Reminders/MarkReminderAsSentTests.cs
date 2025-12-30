// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using AnnualHealthScreeningReminder.Api.Features.Reminders.Commands;
using AnnualHealthScreeningReminder.Core;
using AnnualHealthScreeningReminder.Infrastructure.Data;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace AnnualHealthScreeningReminder.Api.Tests.Features.Reminders;

[TestFixture]
public class MarkReminderAsSentTests
{
    private AnnualHealthScreeningReminderContext _context;
    private MarkReminderAsSent.Handler _handler;
    private MarkReminderAsSent.Validator _validator;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<AnnualHealthScreeningReminderContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AnnualHealthScreeningReminderContext(options);
        _validator = new MarkReminderAsSent.Validator();
        _handler = new MarkReminderAsSent.Handler(_context, _validator);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Test]
    public async Task Handle_ExistingReminder_MarksAsSent()
    {
        // Arrange
        var reminder = new Reminder
        {
            ReminderId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ScreeningId = Guid.NewGuid(),
            ReminderDate = DateTime.UtcNow.AddDays(7),
            Message = "Test reminder",
            IsSent = false,
            CreatedAt = DateTime.UtcNow
        };
        _context.Reminders.Add(reminder);
        await _context.SaveChangesAsync();

        var command = new MarkReminderAsSent.Command(reminder.ReminderId);

        // Act
        var result = await _handler.Handle(command, default);

        // Assert
        result.Should().NotBeNull();
        result.IsSent.Should().BeTrue();

        var updatedReminder = await _context.Reminders.FindAsync(reminder.ReminderId);
        updatedReminder!.IsSent.Should().BeTrue();
    }

    [Test]
    public async Task Handle_NonExistingReminder_ThrowsKeyNotFoundException()
    {
        // Arrange
        var command = new MarkReminderAsSent.Command(Guid.NewGuid());

        // Act & Assert
        var act = async () => await _handler.Handle(command, default);
        await act.Should().ThrowAsync<KeyNotFoundException>();
    }
}
