// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace AnniversaryBirthdayReminder.Infrastructure.Tests;

/// <summary>
/// Unit tests for the AnniversaryBirthdayReminderContext.
/// </summary>
[TestFixture]
public class AnniversaryBirthdayReminderContextTests
{
    private AnniversaryBirthdayReminderContext _context = null!;

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
    /// Tests that ImportantDates can be added and retrieved.
    /// </summary>
    [Test]
    public async Task ImportantDates_CanAddAndRetrieve()
    {
        // Arrange
        var importantDate = new ImportantDate
        {
            ImportantDateId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            PersonName = "Test Person",
            DateType = DateType.Birthday,
            DateValue = DateTime.UtcNow,
            RecurrencePattern = RecurrencePattern.Annual,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.ImportantDates.Add(importantDate);
        await _context.SaveChangesAsync();

        var retrieved = await _context.ImportantDates.FindAsync(importantDate.ImportantDateId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.PersonName, Is.EqualTo("Test Person"));
        Assert.That(retrieved.DateType, Is.EqualTo(DateType.Birthday));
    }

    /// <summary>
    /// Tests that Reminders can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Reminders_CanAddAndRetrieve()
    {
        // Arrange
        var importantDate = new ImportantDate
        {
            ImportantDateId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            PersonName = "Test Person",
            DateType = DateType.Birthday,
            DateValue = DateTime.UtcNow,
            RecurrencePattern = RecurrencePattern.Annual,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        var reminder = new Reminder
        {
            ReminderId = Guid.NewGuid(),
            ImportantDateId = importantDate.ImportantDateId,
            ScheduledTime = DateTime.UtcNow.AddDays(7),
            AdvanceNoticeDays = 7,
            DeliveryChannel = DeliveryChannel.Email,
            Status = ReminderStatus.Scheduled,
        };

        // Act
        _context.ImportantDates.Add(importantDate);
        _context.Reminders.Add(reminder);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Reminders.FindAsync(reminder.ReminderId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.ImportantDateId, Is.EqualTo(importantDate.ImportantDateId));
        Assert.That(retrieved.DeliveryChannel, Is.EqualTo(DeliveryChannel.Email));
    }

    /// <summary>
    /// Tests that Gifts can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Gifts_CanAddAndRetrieve()
    {
        // Arrange
        var importantDate = new ImportantDate
        {
            ImportantDateId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            PersonName = "Test Person",
            DateType = DateType.Birthday,
            DateValue = DateTime.UtcNow,
            RecurrencePattern = RecurrencePattern.Annual,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        var gift = new Gift
        {
            GiftId = Guid.NewGuid(),
            ImportantDateId = importantDate.ImportantDateId,
            Description = "Test Gift",
            EstimatedPrice = 50.00m,
            Status = GiftStatus.Idea,
        };

        // Act
        _context.ImportantDates.Add(importantDate);
        _context.Gifts.Add(gift);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Gifts.FindAsync(gift.GiftId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Description, Is.EqualTo("Test Gift"));
        Assert.That(retrieved.EstimatedPrice, Is.EqualTo(50.00m));
    }

    /// <summary>
    /// Tests that Celebrations can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Celebrations_CanAddAndRetrieve()
    {
        // Arrange
        var importantDate = new ImportantDate
        {
            ImportantDateId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            PersonName = "Test Person",
            DateType = DateType.Birthday,
            DateValue = DateTime.UtcNow,
            RecurrencePattern = RecurrencePattern.Annual,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        var celebration = new Celebration
        {
            CelebrationId = Guid.NewGuid(),
            ImportantDateId = importantDate.ImportantDateId,
            CelebrationDate = DateTime.UtcNow,
            Notes = "Great party!",
            Rating = 5,
            Status = CelebrationStatus.Completed,
        };

        // Act
        _context.ImportantDates.Add(importantDate);
        _context.Celebrations.Add(celebration);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Celebrations.FindAsync(celebration.CelebrationId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Notes, Is.EqualTo("Great party!"));
        Assert.That(retrieved.Rating, Is.EqualTo(5));
        Assert.That(retrieved.Status, Is.EqualTo(CelebrationStatus.Completed));
    }

    /// <summary>
    /// Tests that cascade delete works for related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedEntities()
    {
        // Arrange
        var importantDate = new ImportantDate
        {
            ImportantDateId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            PersonName = "Test Person",
            DateType = DateType.Birthday,
            DateValue = DateTime.UtcNow,
            RecurrencePattern = RecurrencePattern.Annual,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        var reminder = new Reminder
        {
            ReminderId = Guid.NewGuid(),
            ImportantDateId = importantDate.ImportantDateId,
            ScheduledTime = DateTime.UtcNow.AddDays(7),
            AdvanceNoticeDays = 7,
            DeliveryChannel = DeliveryChannel.Email,
            Status = ReminderStatus.Scheduled,
        };

        _context.ImportantDates.Add(importantDate);
        _context.Reminders.Add(reminder);
        await _context.SaveChangesAsync();

        // Act
        _context.ImportantDates.Remove(importantDate);
        await _context.SaveChangesAsync();

        var retrievedReminder = await _context.Reminders.FindAsync(reminder.ReminderId);

        // Assert
        Assert.That(retrievedReminder, Is.Null);
    }
}
