// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace AnniversaryBirthdayReminder.Api.Tests;

/// <summary>
/// Unit tests for ScheduleReminderCommandHandler.
/// </summary>
[TestFixture]
public class ScheduleReminderCommandTests
{
    private AnniversaryBirthdayReminderContext _context = null!;
    private Mock<ILogger<ScheduleReminderCommandHandler>> _loggerMock = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<AnniversaryBirthdayReminderContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AnniversaryBirthdayReminderContext(options);
        _loggerMock = new Mock<ILogger<ScheduleReminderCommandHandler>>();
    }

    /// <summary>
    /// Tears down the test context.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    /// <summary>
    /// Tests that Handle creates a scheduled reminder.
    /// </summary>
    [Test]
    public async Task Handle_WhenDateExists_CreatesReminder()
    {
        // Arrange
        var futureDate = DateTime.UtcNow.AddMonths(2);
        var importantDate = new ImportantDate
        {
            ImportantDateId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            PersonName = "Test Person",
            DateType = DateType.Birthday,
            DateValue = new DateTime(1990, futureDate.Month, futureDate.Day),
            RecurrencePattern = RecurrencePattern.Annual,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        _context.ImportantDates.Add(importantDate);
        await _context.SaveChangesAsync();

        var handler = new ScheduleReminderCommandHandler(_context, _loggerMock.Object);
        var command = new ScheduleReminderCommand
        {
            ImportantDateId = importantDate.ImportantDateId,
            AdvanceNoticeDays = 7,
            DeliveryChannel = DeliveryChannel.Email,
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.AdvanceNoticeDays, Is.EqualTo(7));
        Assert.That(result.DeliveryChannel, Is.EqualTo(DeliveryChannel.Email));
        Assert.That(result.Status, Is.EqualTo(ReminderStatus.Scheduled));

        var savedEntity = await _context.Reminders.FindAsync(result.ReminderId);
        Assert.That(savedEntity, Is.Not.Null);
    }

    /// <summary>
    /// Tests that Handle returns null when the important date does not exist.
    /// </summary>
    [Test]
    public async Task Handle_WhenDateDoesNotExist_ReturnsNull()
    {
        // Arrange
        var handler = new ScheduleReminderCommandHandler(_context, _loggerMock.Object);
        var command = new ScheduleReminderCommand
        {
            ImportantDateId = Guid.NewGuid(),
            AdvanceNoticeDays = 7,
            DeliveryChannel = DeliveryChannel.Email,
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Null);
    }
}
