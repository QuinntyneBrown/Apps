// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace AnniversaryBirthdayReminder.Api.Tests;

/// <summary>
/// Unit tests for DeleteImportantDateCommandHandler.
/// </summary>
[TestFixture]
public class DeleteImportantDateCommandTests
{
    private AnniversaryBirthdayReminderContext _context = null!;
    private Mock<ILogger<DeleteImportantDateCommandHandler>> _loggerMock = null!;

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
        _loggerMock = new Mock<ILogger<DeleteImportantDateCommandHandler>>();
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
    /// Tests that Handle deletes the important date when it exists and has no pending gifts.
    /// </summary>
    [Test]
    public async Task Handle_WhenNoPendingGifts_DeletesDate()
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

        _context.ImportantDates.Add(importantDate);
        await _context.SaveChangesAsync();

        var handler = new DeleteImportantDateCommandHandler(_context, _loggerMock.Object);
        var command = new DeleteImportantDateCommand { ImportantDateId = importantDate.ImportantDateId };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.True);
        Assert.That(await _context.ImportantDates.FindAsync(importantDate.ImportantDateId), Is.Null);
    }

    /// <summary>
    /// Tests that Handle returns false when the important date does not exist.
    /// </summary>
    [Test]
    public async Task Handle_WhenDateDoesNotExist_ReturnsFalse()
    {
        // Arrange
        var handler = new DeleteImportantDateCommandHandler(_context, _loggerMock.Object);
        var command = new DeleteImportantDateCommand { ImportantDateId = Guid.NewGuid() };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.False);
    }

    /// <summary>
    /// Tests that Handle returns false when there are pending gifts.
    /// </summary>
    [Test]
    public async Task Handle_WhenHasPendingGifts_ReturnsFalse()
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
            Description = "Pending Gift",
            EstimatedPrice = 50m,
            Status = GiftStatus.Idea,
        };

        _context.ImportantDates.Add(importantDate);
        _context.Gifts.Add(gift);
        await _context.SaveChangesAsync();

        var handler = new DeleteImportantDateCommandHandler(_context, _loggerMock.Object);
        var command = new DeleteImportantDateCommand { ImportantDateId = importantDate.ImportantDateId };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.False);
        Assert.That(await _context.ImportantDates.FindAsync(importantDate.ImportantDateId), Is.Not.Null);
    }
}
