// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace AnniversaryBirthdayReminder.Api.Tests;

/// <summary>
/// Unit tests for GetDateByIdQueryHandler.
/// </summary>
[TestFixture]
public class GetDateByIdQueryTests
{
    private AnniversaryBirthdayReminderContext _context = null!;
    private Mock<ILogger<GetDateByIdQueryHandler>> _loggerMock = null!;

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
        _loggerMock = new Mock<ILogger<GetDateByIdQueryHandler>>();
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
    /// Tests that Handle returns the important date when it exists.
    /// </summary>
    [Test]
    public async Task Handle_WhenDateExists_ReturnsDto()
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

        var handler = new GetDateByIdQueryHandler(_context, _loggerMock.Object);
        var query = new GetDateByIdQuery { ImportantDateId = importantDate.ImportantDateId };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.ImportantDateId, Is.EqualTo(importantDate.ImportantDateId));
        Assert.That(result.PersonName, Is.EqualTo("Test Person"));
    }

    /// <summary>
    /// Tests that Handle returns null when the important date does not exist.
    /// </summary>
    [Test]
    public async Task Handle_WhenDateDoesNotExist_ReturnsNull()
    {
        // Arrange
        var handler = new GetDateByIdQueryHandler(_context, _loggerMock.Object);
        var query = new GetDateByIdQuery { ImportantDateId = Guid.NewGuid() };

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Null);
    }
}
