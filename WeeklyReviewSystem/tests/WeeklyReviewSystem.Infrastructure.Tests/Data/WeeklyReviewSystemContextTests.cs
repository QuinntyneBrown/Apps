// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WeeklyReviewSystem.Infrastructure.Tests;

/// <summary>
/// Unit tests for the WeeklyReviewSystemContext.
/// </summary>
[TestFixture]
public class WeeklyReviewSystemContextTests
{
    private WeeklyReviewSystemContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<WeeklyReviewSystemContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new WeeklyReviewSystemContext(options);
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
    /// Tests that Reviews can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Reviews_CanAddAndRetrieve()
    {
        // Arrange
        var review = new WeeklyReview
        {
            WeeklyReviewId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WeekStartDate = DateTime.UtcNow.AddDays(-7),
            WeekEndDate = DateTime.UtcNow,
            OverallRating = 8,
            Reflections = "Great week!",
            IsCompleted = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Reviews.FindAsync(review.WeeklyReviewId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.OverallRating, Is.EqualTo(8));
        Assert.That(retrieved.IsCompleted, Is.True);
    }

    /// <summary>
    /// Tests that Accomplishments can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Accomplishments_CanAddAndRetrieve()
    {
        // Arrange
        var review = new WeeklyReview
        {
            WeeklyReviewId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WeekStartDate = DateTime.UtcNow.AddDays(-7),
            WeekEndDate = DateTime.UtcNow,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
        };

        var accomplishment = new Accomplishment
        {
            AccomplishmentId = Guid.NewGuid(),
            WeeklyReviewId = review.WeeklyReviewId,
            Title = "Completed Project",
            Description = "Finished the main deliverable.",
            Category = "Work",
            ImpactLevel = 9,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Reviews.Add(review);
        _context.Accomplishments.Add(accomplishment);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Accomplishments.FindAsync(accomplishment.AccomplishmentId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Completed Project"));
        Assert.That(retrieved.ImpactLevel, Is.EqualTo(9));
    }

    /// <summary>
    /// Tests that Challenges can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Challenges_CanAddAndRetrieve()
    {
        // Arrange
        var review = new WeeklyReview
        {
            WeeklyReviewId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WeekStartDate = DateTime.UtcNow.AddDays(-7),
            WeekEndDate = DateTime.UtcNow,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
        };

        var challenge = new Challenge
        {
            ChallengeId = Guid.NewGuid(),
            WeeklyReviewId = review.WeeklyReviewId,
            Title = "Time Management",
            Description = "Struggled with meeting deadlines.",
            IsResolved = true,
            Resolution = "Created a better schedule.",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Reviews.Add(review);
        _context.Challenges.Add(challenge);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Challenges.FindAsync(challenge.ChallengeId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Time Management"));
        Assert.That(retrieved.IsResolved, Is.True);
    }

    /// <summary>
    /// Tests that Priorities can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Priorities_CanAddAndRetrieve()
    {
        // Arrange
        var review = new WeeklyReview
        {
            WeeklyReviewId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WeekStartDate = DateTime.UtcNow,
            WeekEndDate = DateTime.UtcNow.AddDays(7),
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
        };

        var priority = new WeeklyPriority
        {
            WeeklyPriorityId = Guid.NewGuid(),
            WeeklyReviewId = review.WeeklyReviewId,
            Title = "Complete Documentation",
            Level = PriorityLevel.High,
            Category = "Work",
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Reviews.Add(review);
        _context.Priorities.Add(priority);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Priorities.FindAsync(priority.WeeklyPriorityId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Complete Documentation"));
        Assert.That(retrieved.Level, Is.EqualTo(PriorityLevel.High));
    }

    /// <summary>
    /// Tests that cascade delete works for Review and related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedEntities()
    {
        // Arrange
        var review = new WeeklyReview
        {
            WeeklyReviewId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            WeekStartDate = DateTime.UtcNow.AddDays(-7),
            WeekEndDate = DateTime.UtcNow,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
        };

        var accomplishment = new Accomplishment
        {
            AccomplishmentId = Guid.NewGuid(),
            WeeklyReviewId = review.WeeklyReviewId,
            Title = "Test Accomplishment",
            CreatedAt = DateTime.UtcNow,
        };

        var challenge = new Challenge
        {
            ChallengeId = Guid.NewGuid(),
            WeeklyReviewId = review.WeeklyReviewId,
            Title = "Test Challenge",
            IsResolved = false,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Reviews.Add(review);
        _context.Accomplishments.Add(accomplishment);
        _context.Challenges.Add(challenge);
        await _context.SaveChangesAsync();

        // Act
        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();

        var retrievedAccomplishment = await _context.Accomplishments.FindAsync(accomplishment.AccomplishmentId);
        var retrievedChallenge = await _context.Challenges.FindAsync(challenge.ChallengeId);

        // Assert
        Assert.That(retrievedAccomplishment, Is.Null);
        Assert.That(retrievedChallenge, Is.Null);
    }
}
