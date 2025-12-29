// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace AnniversaryBirthdayReminder.Api.Tests;

/// <summary>
/// Unit tests for AddGiftIdeaCommandHandler.
/// </summary>
[TestFixture]
public class AddGiftIdeaCommandTests
{
    private AnniversaryBirthdayReminderContext _context = null!;
    private Mock<ILogger<AddGiftIdeaCommandHandler>> _loggerMock = null!;

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
        _loggerMock = new Mock<ILogger<AddGiftIdeaCommandHandler>>();
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
    /// Tests that Handle creates a gift idea.
    /// </summary>
    [Test]
    public async Task Handle_CreatesGiftIdea()
    {
        // Arrange
        var handler = new AddGiftIdeaCommandHandler(_context, _loggerMock.Object);
        var command = new AddGiftIdeaCommand
        {
            ImportantDateId = Guid.NewGuid(),
            Description = "Bluetooth Headphones",
            EstimatedPrice = 79.99m,
            PurchaseUrl = "https://example.com/headphones",
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Description, Is.EqualTo("Bluetooth Headphones"));
        Assert.That(result.EstimatedPrice, Is.EqualTo(79.99m));
        Assert.That(result.Status, Is.EqualTo(GiftStatus.Idea));

        var savedEntity = await _context.Gifts.FindAsync(result.GiftId);
        Assert.That(savedEntity, Is.Not.Null);
    }
}
