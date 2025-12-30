// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ResumeCareerAchievementTracker.Infrastructure.Tests;

/// <summary>
/// Unit tests for the ResumeCareerAchievementTrackerContext.
/// </summary>
[TestFixture]
public class ResumeCareerAchievementTrackerContextTests
{
    private ResumeCareerAchievementTrackerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<ResumeCareerAchievementTrackerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ResumeCareerAchievementTrackerContext(options);
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
    /// Tests that Skills can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Skills_CanAddAndRetrieve()
    {
        // Arrange
        var skill = new Skill
        {
            SkillId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "C# Programming",
            Category = "Programming",
            ProficiencyLevel = "Expert",
            YearsOfExperience = 5,
            IsFeatured = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Skills.Add(skill);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Skills.FindAsync(skill.SkillId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("C# Programming"));
        Assert.That(retrieved.Category, Is.EqualTo("Programming"));
    }

    /// <summary>
    /// Tests that Projects can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Projects_CanAddAndRetrieve()
    {
        // Arrange
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "E-Commerce Platform",
            Description = "Built a scalable e-commerce platform",
            StartDate = DateTime.UtcNow.AddMonths(-6),
            IsFeatured = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Projects.FindAsync(project.ProjectId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("E-Commerce Platform"));
        Assert.That(retrieved.Description, Is.EqualTo("Built a scalable e-commerce platform"));
    }

    /// <summary>
    /// Tests that Achievements can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Achievements_CanAddAndRetrieve()
    {
        // Arrange
        var achievement = new Achievement
        {
            AchievementId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Improved System Performance",
            Description = "Optimized database queries resulting in 50% performance improvement",
            AchievementType = AchievementType.TechnicalAccomplishment,
            AchievedDate = DateTime.UtcNow.AddMonths(-3),
            IsFeatured = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Achievements.Add(achievement);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Achievements.FindAsync(achievement.AchievementId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Improved System Performance"));
        Assert.That(retrieved.AchievementType, Is.EqualTo(AchievementType.TechnicalAccomplishment));
    }

    /// <summary>
    /// Tests that Skills can be updated.
    /// </summary>
    [Test]
    public async Task Skills_CanUpdate()
    {
        // Arrange
        var skill = new Skill
        {
            SkillId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "JavaScript",
            Category = "Programming",
            ProficiencyLevel = "Intermediate",
            IsFeatured = false,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Skills.Add(skill);
        await _context.SaveChangesAsync();

        // Act
        skill.UpdateProficiency("Advanced");
        skill.ToggleFeatured();
        await _context.SaveChangesAsync();

        var retrieved = await _context.Skills.FindAsync(skill.SkillId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.ProficiencyLevel, Is.EqualTo("Advanced"));
        Assert.That(retrieved.IsFeatured, Is.True);
    }

    /// <summary>
    /// Tests that Projects can store Technologies and Outcomes as lists.
    /// </summary>
    [Test]
    public async Task Projects_CanStoreLists()
    {
        // Arrange
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Mobile App",
            Description = "Developed a mobile application",
            StartDate = DateTime.UtcNow.AddMonths(-3),
            Technologies = new List<string> { "React Native", "TypeScript", "Firebase" },
            Outcomes = new List<string> { "100K+ downloads", "4.5 star rating" },
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Projects.FindAsync(project.ProjectId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Technologies, Has.Count.EqualTo(3));
        Assert.That(retrieved.Technologies, Contains.Item("React Native"));
        Assert.That(retrieved.Outcomes, Has.Count.EqualTo(2));
        Assert.That(retrieved.Outcomes, Contains.Item("100K+ downloads"));
    }
}
