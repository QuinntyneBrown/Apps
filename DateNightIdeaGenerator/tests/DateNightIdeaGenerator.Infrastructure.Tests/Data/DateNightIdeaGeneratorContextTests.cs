// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace DateNightIdeaGenerator.Infrastructure.Tests;

/// <summary>
/// Unit tests for the DateNightIdeaGeneratorContext.
/// </summary>
[TestFixture]
public class DateNightIdeaGeneratorContextTests
{
    private DateNightIdeaGeneratorContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<DateNightIdeaGeneratorContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new DateNightIdeaGeneratorContext(options);
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
    /// Tests that DateIdeas can be added and retrieved.
    /// </summary>
    [Test]
    public async Task DateIdeas_CanAddAndRetrieve()
    {
        // Arrange
        var dateIdea = new DateIdea
        {
            DateIdeaId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Sunset Picnic",
            Description = "Romantic picnic at sunset",
            Category = Category.Outdoor,
            BudgetRange = BudgetRange.Low,
            IsFavorite = true,
            HasBeenTried = false,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.DateIdeas.Add(dateIdea);
        await _context.SaveChangesAsync();

        var retrieved = await _context.DateIdeas.FindAsync(dateIdea.DateIdeaId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Sunset Picnic"));
        Assert.That(retrieved.Category, Is.EqualTo(Category.Outdoor));
    }

    /// <summary>
    /// Tests that Experiences can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Experiences_CanAddAndRetrieve()
    {
        // Arrange
        var dateIdea = new DateIdea
        {
            DateIdeaId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Museum Visit",
            Description = "Art museum tour",
            Category = Category.Cultural,
            BudgetRange = BudgetRange.Medium,
            CreatedAt = DateTime.UtcNow,
        };

        var experience = new Experience
        {
            ExperienceId = Guid.NewGuid(),
            DateIdeaId = dateIdea.DateIdeaId,
            UserId = dateIdea.UserId,
            ExperienceDate = DateTime.UtcNow,
            Notes = "Had a wonderful time!",
            ActualCost = 45.50m,
            WasSuccessful = true,
            WouldRepeat = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.DateIdeas.Add(dateIdea);
        _context.Experiences.Add(experience);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Experiences.FindAsync(experience.ExperienceId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Notes, Is.EqualTo("Had a wonderful time!"));
        Assert.That(retrieved.ActualCost, Is.EqualTo(45.50m));
    }

    /// <summary>
    /// Tests that Ratings can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Ratings_CanAddAndRetrieve()
    {
        // Arrange
        var dateIdea = new DateIdea
        {
            DateIdeaId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Cooking Class",
            Description = "Learn to cook Italian cuisine",
            Category = Category.FoodAndDining,
            BudgetRange = BudgetRange.High,
            CreatedAt = DateTime.UtcNow,
        };

        var rating = new Rating
        {
            RatingId = Guid.NewGuid(),
            DateIdeaId = dateIdea.DateIdeaId,
            UserId = dateIdea.UserId,
            Score = 5,
            Review = "Absolutely amazing experience!",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.DateIdeas.Add(dateIdea);
        _context.Ratings.Add(rating);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Ratings.FindAsync(rating.RatingId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Score, Is.EqualTo(5));
        Assert.That(retrieved.Review, Is.EqualTo("Absolutely amazing experience!"));
    }

    /// <summary>
    /// Tests that cascade delete works for related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedEntities()
    {
        // Arrange
        var dateIdea = new DateIdea
        {
            DateIdeaId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Star Gazing",
            Description = "Watch the stars together",
            Category = Category.Romantic,
            BudgetRange = BudgetRange.Free,
            CreatedAt = DateTime.UtcNow,
        };

        var experience = new Experience
        {
            ExperienceId = Guid.NewGuid(),
            DateIdeaId = dateIdea.DateIdeaId,
            UserId = dateIdea.UserId,
            ExperienceDate = DateTime.UtcNow,
            Notes = "Perfect night!",
            WasSuccessful = true,
            WouldRepeat = true,
            CreatedAt = DateTime.UtcNow,
        };

        var rating = new Rating
        {
            RatingId = Guid.NewGuid(),
            DateIdeaId = dateIdea.DateIdeaId,
            UserId = dateIdea.UserId,
            Score = 5,
            CreatedAt = DateTime.UtcNow,
        };

        _context.DateIdeas.Add(dateIdea);
        _context.Experiences.Add(experience);
        _context.Ratings.Add(rating);
        await _context.SaveChangesAsync();

        // Act
        _context.DateIdeas.Remove(dateIdea);
        await _context.SaveChangesAsync();

        var retrievedExperience = await _context.Experiences.FindAsync(experience.ExperienceId);
        var retrievedRating = await _context.Ratings.FindAsync(rating.RatingId);

        // Assert
        Assert.That(retrievedExperience, Is.Null);
        Assert.That(retrievedRating, Is.Null);
    }

    /// <summary>
    /// Tests that a date idea can have multiple experiences.
    /// </summary>
    [Test]
    public async Task DateIdea_CanHaveMultipleExperiences()
    {
        // Arrange
        var dateIdea = new DateIdea
        {
            DateIdeaId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Hiking Trail",
            Description = "Scenic mountain hike",
            Category = Category.Adventure,
            BudgetRange = BudgetRange.Free,
            CreatedAt = DateTime.UtcNow,
        };

        _context.DateIdeas.Add(dateIdea);
        await _context.SaveChangesAsync();

        var experience1 = new Experience
        {
            ExperienceId = Guid.NewGuid(),
            DateIdeaId = dateIdea.DateIdeaId,
            UserId = dateIdea.UserId,
            ExperienceDate = DateTime.UtcNow.AddMonths(-2),
            Notes = "First hike - took the easy trail",
            WasSuccessful = true,
            WouldRepeat = true,
            CreatedAt = DateTime.UtcNow.AddMonths(-2),
        };

        var experience2 = new Experience
        {
            ExperienceId = Guid.NewGuid(),
            DateIdeaId = dateIdea.DateIdeaId,
            UserId = dateIdea.UserId,
            ExperienceDate = DateTime.UtcNow,
            Notes = "Second hike - tackled the advanced trail",
            WasSuccessful = true,
            WouldRepeat = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Experiences.AddRange(experience1, experience2);
        await _context.SaveChangesAsync();

        var experiences = await _context.Experiences
            .Where(e => e.DateIdeaId == dateIdea.DateIdeaId)
            .ToListAsync();

        // Assert
        Assert.That(experiences, Has.Count.EqualTo(2));
        Assert.That(experiences.Any(e => e.Notes.Contains("easy trail")), Is.True);
        Assert.That(experiences.Any(e => e.Notes.Contains("advanced trail")), Is.True);
    }

    /// <summary>
    /// Tests that ratings can be associated with both date ideas and experiences.
    /// </summary>
    [Test]
    public async Task Ratings_CanBeAssociatedWithDateIdeasAndExperiences()
    {
        // Arrange
        var dateIdea = new DateIdea
        {
            DateIdeaId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Concert Date",
            Description = "Live music concert",
            Category = Category.Entertainment,
            BudgetRange = BudgetRange.High,
            CreatedAt = DateTime.UtcNow,
        };

        var experience = new Experience
        {
            ExperienceId = Guid.NewGuid(),
            DateIdeaId = dateIdea.DateIdeaId,
            UserId = dateIdea.UserId,
            ExperienceDate = DateTime.UtcNow,
            Notes = "Amazing concert!",
            WasSuccessful = true,
            WouldRepeat = true,
            CreatedAt = DateTime.UtcNow,
        };

        var ideaRating = new Rating
        {
            RatingId = Guid.NewGuid(),
            DateIdeaId = dateIdea.DateIdeaId,
            UserId = dateIdea.UserId,
            Score = 4,
            Review = "Great idea for a date",
            CreatedAt = DateTime.UtcNow,
        };

        var experienceRating = new Rating
        {
            RatingId = Guid.NewGuid(),
            ExperienceId = experience.ExperienceId,
            UserId = dateIdea.UserId,
            Score = 5,
            Review = "The actual experience exceeded expectations!",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.DateIdeas.Add(dateIdea);
        _context.Experiences.Add(experience);
        _context.Ratings.AddRange(ideaRating, experienceRating);
        await _context.SaveChangesAsync();

        var ideaRatings = await _context.Ratings
            .Where(r => r.DateIdeaId == dateIdea.DateIdeaId)
            .ToListAsync();

        var expRatings = await _context.Ratings
            .Where(r => r.ExperienceId == experience.ExperienceId)
            .ToListAsync();

        // Assert
        Assert.That(ideaRatings, Has.Count.EqualTo(1));
        Assert.That(expRatings, Has.Count.EqualTo(1));
        Assert.That(ideaRatings[0].Score, Is.EqualTo(4));
        Assert.That(expRatings[0].Score, Is.EqualTo(5));
    }
}
