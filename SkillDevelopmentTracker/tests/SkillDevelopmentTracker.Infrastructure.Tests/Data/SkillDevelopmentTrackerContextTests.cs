// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SkillDevelopmentTracker.Infrastructure.Tests;

/// <summary>
/// Unit tests for the SkillDevelopmentTrackerContext.
/// </summary>
[TestFixture]
public class SkillDevelopmentTrackerContextTests
{
    private SkillDevelopmentTrackerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<SkillDevelopmentTrackerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new SkillDevelopmentTrackerContext(options);
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
            Name = "C#",
            Category = "Programming",
            ProficiencyLevel = ProficiencyLevel.Intermediate,
            StartDate = DateTime.UtcNow,
            HoursSpent = 50m,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Skills.Add(skill);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Skills.FindAsync(skill.SkillId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("C#"));
        Assert.That(retrieved.ProficiencyLevel, Is.EqualTo(ProficiencyLevel.Intermediate));
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
            UserId = Guid.NewGuid(),
            Title = "Test Course",
            Provider = "Udemy",
            ProgressPercentage = 50,
            ActualHours = 10m,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Courses.FindAsync(course.CourseId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Test Course"));
        Assert.That(retrieved.Provider, Is.EqualTo("Udemy"));
        Assert.That(retrieved.ProgressPercentage, Is.EqualTo(50));
    }

    /// <summary>
    /// Tests that Certifications can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Certifications_CanAddAndRetrieve()
    {
        // Arrange
        var certification = new Certification
        {
            CertificationId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Certification",
            IssuingOrganization = "Test Org",
            IssueDate = DateTime.UtcNow,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Certifications.Add(certification);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Certifications.FindAsync(certification.CertificationId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Certification"));
        Assert.That(retrieved.IssuingOrganization, Is.EqualTo("Test Org"));
        Assert.That(retrieved.IsActive, Is.True);
    }

    /// <summary>
    /// Tests that LearningPaths can be added and retrieved.
    /// </summary>
    [Test]
    public async Task LearningPaths_CanAddAndRetrieve()
    {
        // Arrange
        var learningPath = new LearningPath
        {
            LearningPathId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Learning Path",
            Description = "Test Description",
            StartDate = DateTime.UtcNow,
            ProgressPercentage = 25,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.LearningPaths.Add(learningPath);
        await _context.SaveChangesAsync();

        var retrieved = await _context.LearningPaths.FindAsync(learningPath.LearningPathId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Test Learning Path"));
        Assert.That(retrieved.Description, Is.EqualTo("Test Description"));
        Assert.That(retrieved.ProgressPercentage, Is.EqualTo(25));
    }

    /// <summary>
    /// Tests that skills can be updated.
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
            ProficiencyLevel = ProficiencyLevel.Beginner,
            StartDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Skills.Add(skill);
        await _context.SaveChangesAsync();

        // Act
        skill.ProficiencyLevel = ProficiencyLevel.Intermediate;
        skill.HoursSpent = 100m;
        skill.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        var retrieved = await _context.Skills.FindAsync(skill.SkillId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.ProficiencyLevel, Is.EqualTo(ProficiencyLevel.Intermediate));
        Assert.That(retrieved.HoursSpent, Is.EqualTo(100m));
        Assert.That(retrieved.UpdatedAt, Is.Not.Null);
    }

    /// <summary>
    /// Tests that skills can be deleted.
    /// </summary>
    [Test]
    public async Task Skills_CanDelete()
    {
        // Arrange
        var skill = new Skill
        {
            SkillId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "TypeScript",
            Category = "Programming",
            ProficiencyLevel = ProficiencyLevel.Novice,
            StartDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Skills.Add(skill);
        await _context.SaveChangesAsync();

        // Act
        _context.Skills.Remove(skill);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Skills.FindAsync(skill.SkillId);

        // Assert
        Assert.That(retrieved, Is.Null);
    }
}
