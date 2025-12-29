// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace AnniversaryBirthdayReminder.Api.Tests;

/// <summary>
/// Unit tests for CreateImportantDateCommandHandler.
/// </summary>
[TestFixture]
public class CreateImportantDateCommandTests
{
    private AnniversaryBirthdayReminderContext _context = null!;
    private Mock<ILogger<CreateImportantDateCommandHandler>> _loggerMock = null!;

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
        _loggerMock = new Mock<ILogger<CreateImportantDateCommandHandler>>();
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
    /// Tests that Handle creates an important date successfully.
    /// </summary>
    [Test]
    public async Task Handle_CreatesImportantDate()
    {
        // Arrange
        var handler = new CreateImportantDateCommandHandler(_context, _loggerMock.Object);
        var command = new CreateImportantDateCommand
        {
            UserId = Guid.NewGuid(),
            PersonName = "John Doe",
            DateType = DateType.Birthday,
            DateValue = new DateTime(1990, 5, 15),
            RecurrencePattern = RecurrencePattern.Annual,
            Relationship = "Friend",
            Notes = "Likes tech gadgets",
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.PersonName, Is.EqualTo("John Doe"));
        Assert.That(result.DateType, Is.EqualTo(DateType.Birthday));
        Assert.That(result.IsActive, Is.True);

        var savedEntity = await _context.ImportantDates.FindAsync(result.ImportantDateId);
        Assert.That(savedEntity, Is.Not.Null);
    }

    /// <summary>
    /// Tests that Handle returns DTO with correct next occurrence.
    /// </summary>
    [Test]
    public async Task Handle_ReturnsCorrectNextOccurrence()
    {
        // Arrange
        var handler = new CreateImportantDateCommandHandler(_context, _loggerMock.Object);
        var futureMonth = DateTime.UtcNow.AddMonths(2).Month;
        var command = new CreateImportantDateCommand
        {
            UserId = Guid.NewGuid(),
            PersonName = "Jane Doe",
            DateType = DateType.Anniversary,
            DateValue = new DateTime(2015, futureMonth, 15),
            RecurrencePattern = RecurrencePattern.Annual,
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result.NextOccurrence.Year, Is.EqualTo(DateTime.UtcNow.Year));
        Assert.That(result.NextOccurrence.Month, Is.EqualTo(futureMonth));
    }
}
