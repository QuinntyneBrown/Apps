// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace GolfScoreTracker.Infrastructure.Tests;

/// <summary>
/// Unit tests for the GolfScoreTrackerContext.
/// </summary>
[TestFixture]
public class GolfScoreTrackerContextTests
{
    private GolfScoreTrackerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<GolfScoreTrackerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new GolfScoreTrackerContext(options);
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
    /// Tests that Courses can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Courses_CanAddAndRetrieve()
    {
        // Arrange
        var course = new Course
        {
            CourseId = Guid.NewGuid(),
            Name = "Test Course",
            Location = "Test Location",
            NumberOfHoles = 18,
            TotalPar = 72,
            CourseRating = 72.5m,
            SlopeRating = 130,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Courses.FindAsync(course.CourseId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Course"));
        Assert.That(retrieved.TotalPar, Is.EqualTo(72));
    }

    /// <summary>
    /// Tests that Rounds can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Rounds_CanAddAndRetrieve()
    {
        // Arrange
        var course = new Course
        {
            CourseId = Guid.NewGuid(),
            Name = "Test Course",
            NumberOfHoles = 18,
            TotalPar = 72,
            CreatedAt = DateTime.UtcNow,
        };

        var round = new Round
        {
            RoundId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            CourseId = course.CourseId,
            PlayedDate = DateTime.UtcNow,
            TotalScore = 85,
            TotalPar = 72,
            Weather = "Sunny",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Courses.Add(course);
        _context.Rounds.Add(round);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Rounds.FindAsync(round.RoundId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.TotalScore, Is.EqualTo(85));
        Assert.That(retrieved.Weather, Is.EqualTo("Sunny"));
    }

    /// <summary>
    /// Tests that HoleScores can be added and retrieved.
    /// </summary>
    [Test]
    public async Task HoleScores_CanAddAndRetrieve()
    {
        // Arrange
        var course = new Course
        {
            CourseId = Guid.NewGuid(),
            Name = "Test Course",
            NumberOfHoles = 18,
            TotalPar = 72,
            CreatedAt = DateTime.UtcNow,
        };

        var round = new Round
        {
            RoundId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            CourseId = course.CourseId,
            PlayedDate = DateTime.UtcNow,
            TotalScore = 85,
            TotalPar = 72,
            CreatedAt = DateTime.UtcNow,
        };

        var holeScore = new HoleScore
        {
            HoleScoreId = Guid.NewGuid(),
            RoundId = round.RoundId,
            HoleNumber = 1,
            Par = 4,
            Score = 5,
            Putts = 2,
            FairwayHit = true,
            GreenInRegulation = false,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Courses.Add(course);
        _context.Rounds.Add(round);
        _context.HoleScores.Add(holeScore);
        await _context.SaveChangesAsync();

        var retrieved = await _context.HoleScores.FindAsync(holeScore.HoleScoreId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.HoleNumber, Is.EqualTo(1));
        Assert.That(retrieved.Score, Is.EqualTo(5));
        Assert.That(retrieved.FairwayHit, Is.True);
    }

    /// <summary>
    /// Tests that Handicaps can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Handicaps_CanAddAndRetrieve()
    {
        // Arrange
        var handicap = new Handicap
        {
            HandicapId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            HandicapIndex = 15.5m,
            CalculatedDate = DateTime.UtcNow,
            RoundsUsed = 20,
            Notes = "Test handicap",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Handicaps.Add(handicap);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Handicaps.FindAsync(handicap.HandicapId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.HandicapIndex, Is.EqualTo(15.5m));
        Assert.That(retrieved.RoundsUsed, Is.EqualTo(20));
    }

    /// <summary>
    /// Tests that cascade delete works for related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedHoleScores()
    {
        // Arrange
        var course = new Course
        {
            CourseId = Guid.NewGuid(),
            Name = "Test Course",
            NumberOfHoles = 18,
            TotalPar = 72,
            CreatedAt = DateTime.UtcNow,
        };

        var round = new Round
        {
            RoundId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            CourseId = course.CourseId,
            PlayedDate = DateTime.UtcNow,
            TotalScore = 85,
            TotalPar = 72,
            CreatedAt = DateTime.UtcNow,
        };

        var holeScore = new HoleScore
        {
            HoleScoreId = Guid.NewGuid(),
            RoundId = round.RoundId,
            HoleNumber = 1,
            Par = 4,
            Score = 5,
            Putts = 2,
            FairwayHit = true,
            GreenInRegulation = false,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Courses.Add(course);
        _context.Rounds.Add(round);
        _context.HoleScores.Add(holeScore);
        await _context.SaveChangesAsync();

        // Act
        _context.Rounds.Remove(round);
        await _context.SaveChangesAsync();

        var retrievedHoleScore = await _context.HoleScores.FindAsync(holeScore.HoleScoreId);

        // Assert
        Assert.That(retrievedHoleScore, Is.Null);
    }
}
