// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PokerGameTracker.Infrastructure.Tests;

/// <summary>
/// Unit tests for the PokerGameTrackerContext.
/// </summary>
[TestFixture]
public class PokerGameTrackerContextTests
{
    private PokerGameTrackerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<PokerGameTrackerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new PokerGameTrackerContext(options);
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
    /// Tests that Sessions can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Sessions_CanAddAndRetrieve()
    {
        // Arrange
        var session = new Session
        {
            SessionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            GameType = GameType.TexasHoldem,
            StartTime = DateTime.UtcNow,
            EndTime = DateTime.UtcNow.AddHours(4),
            BuyIn = 200.00m,
            CashOut = 350.00m,
            Location = "Test Casino",
            Notes = "Test notes",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Sessions.Add(session);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Sessions.FindAsync(session.SessionId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.GameType, Is.EqualTo(GameType.TexasHoldem));
        Assert.That(retrieved.BuyIn, Is.EqualTo(200.00m));
    }

    /// <summary>
    /// Tests that Hands can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Hands_CanAddAndRetrieve()
    {
        // Arrange
        var session = new Session
        {
            SessionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            GameType = GameType.TexasHoldem,
            StartTime = DateTime.UtcNow,
            BuyIn = 200.00m,
            CreatedAt = DateTime.UtcNow,
        };

        var hand = new Hand
        {
            HandId = Guid.NewGuid(),
            UserId = session.UserId,
            SessionId = session.SessionId,
            StartingCards = "As Kd",
            PotSize = 85.00m,
            WasWon = true,
            Notes = "Test hand",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Sessions.Add(session);
        _context.Hands.Add(hand);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Hands.FindAsync(hand.HandId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.StartingCards, Is.EqualTo("As Kd"));
        Assert.That(retrieved.WasWon, Is.True);
    }

    /// <summary>
    /// Tests that Bankrolls can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Bankrolls_CanAddAndRetrieve()
    {
        // Arrange
        var bankroll = new Bankroll
        {
            BankrollId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Amount = 1000.00m,
            RecordedDate = DateTime.UtcNow,
            Notes = "Initial bankroll",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Bankrolls.Add(bankroll);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Bankrolls.FindAsync(bankroll.BankrollId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Amount, Is.EqualTo(1000.00m));
        Assert.That(retrieved.Notes, Is.EqualTo("Initial bankroll"));
    }

    /// <summary>
    /// Tests that cascade delete works for Hands when Session is deleted.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedHands()
    {
        // Arrange
        var session = new Session
        {
            SessionId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            GameType = GameType.TexasHoldem,
            StartTime = DateTime.UtcNow,
            BuyIn = 200.00m,
            CreatedAt = DateTime.UtcNow,
        };

        var hand = new Hand
        {
            HandId = Guid.NewGuid(),
            UserId = session.UserId,
            SessionId = session.SessionId,
            StartingCards = "As Kd",
            WasWon = true,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Sessions.Add(session);
        _context.Hands.Add(hand);
        await _context.SaveChangesAsync();

        // Act
        _context.Sessions.Remove(session);
        await _context.SaveChangesAsync();

        var retrievedHand = await _context.Hands.FindAsync(hand.HandId);

        // Assert
        Assert.That(retrievedHand, Is.Null);
    }
}
