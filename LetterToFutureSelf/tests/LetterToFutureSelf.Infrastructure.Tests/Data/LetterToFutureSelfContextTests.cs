// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace LetterToFutureSelf.Infrastructure.Tests;

/// <summary>
/// Unit tests for the LetterToFutureSelfContext.
/// </summary>
[TestFixture]
public class LetterToFutureSelfContextTests
{
    private LetterToFutureSelfContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<LetterToFutureSelfContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new LetterToFutureSelfContext(options);
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
    /// Tests that Letters can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Letters_CanAddAndRetrieve()
    {
        // Arrange
        var letter = new Letter
        {
            LetterId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Subject = "Test Letter",
            Content = "This is a test letter to my future self",
            WrittenDate = DateTime.UtcNow,
            ScheduledDeliveryDate = DateTime.UtcNow.AddYears(1),
            DeliveryStatus = DeliveryStatus.Pending,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Letters.Add(letter);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Letters.FindAsync(letter.LetterId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Subject, Is.EqualTo("Test Letter"));
        Assert.That(retrieved.Content, Is.EqualTo("This is a test letter to my future self"));
        Assert.That(retrieved.DeliveryStatus, Is.EqualTo(DeliveryStatus.Pending));
    }

    /// <summary>
    /// Tests that DeliverySchedules can be added and retrieved.
    /// </summary>
    [Test]
    public async Task DeliverySchedules_CanAddAndRetrieve()
    {
        // Arrange
        var letter = new Letter
        {
            LetterId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Subject = "Test Letter",
            Content = "Content",
            WrittenDate = DateTime.UtcNow,
            ScheduledDeliveryDate = DateTime.UtcNow.AddYears(1),
            DeliveryStatus = DeliveryStatus.Pending,
            CreatedAt = DateTime.UtcNow,
        };

        var deliverySchedule = new DeliverySchedule
        {
            DeliveryScheduleId = Guid.NewGuid(),
            LetterId = letter.LetterId,
            ScheduledDateTime = DateTime.UtcNow.AddYears(1),
            DeliveryMethod = "Email",
            RecipientContact = "test@example.com",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Letters.Add(letter);
        _context.DeliverySchedules.Add(deliverySchedule);
        await _context.SaveChangesAsync();

        var retrieved = await _context.DeliverySchedules.FindAsync(deliverySchedule.DeliveryScheduleId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.LetterId, Is.EqualTo(letter.LetterId));
        Assert.That(retrieved.DeliveryMethod, Is.EqualTo("Email"));
        Assert.That(retrieved.IsActive, Is.True);
    }

    /// <summary>
    /// Tests that cascade delete works for delivery schedules.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesDeliverySchedules()
    {
        // Arrange
        var letter = new Letter
        {
            LetterId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Subject = "Test Letter",
            Content = "Content",
            WrittenDate = DateTime.UtcNow,
            ScheduledDeliveryDate = DateTime.UtcNow.AddYears(1),
            DeliveryStatus = DeliveryStatus.Pending,
            CreatedAt = DateTime.UtcNow,
        };

        var deliverySchedule = new DeliverySchedule
        {
            DeliveryScheduleId = Guid.NewGuid(),
            LetterId = letter.LetterId,
            ScheduledDateTime = DateTime.UtcNow.AddYears(1),
            DeliveryMethod = "Email",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Letters.Add(letter);
        _context.DeliverySchedules.Add(deliverySchedule);
        await _context.SaveChangesAsync();

        // Act
        _context.Letters.Remove(letter);
        await _context.SaveChangesAsync();

        var retrievedSchedule = await _context.DeliverySchedules.FindAsync(deliverySchedule.DeliveryScheduleId);

        // Assert
        Assert.That(retrievedSchedule, Is.Null);
    }
}
