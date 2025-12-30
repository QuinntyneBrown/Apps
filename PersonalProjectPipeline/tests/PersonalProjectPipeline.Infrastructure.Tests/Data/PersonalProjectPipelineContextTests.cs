// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalProjectPipeline.Infrastructure.Tests;

/// <summary>
/// Unit tests for the PersonalProjectPipelineContext.
/// </summary>
[TestFixture]
public class PersonalProjectPipelineContextTests
{
    private PersonalProjectPipelineContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<PersonalProjectPipelineContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new PersonalProjectPipelineContext(options);
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
            Name = "Test Project",
            Description = "This is a test project",
            Status = ProjectStatus.InProgress,
            Priority = ProjectPriority.High,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Projects.FindAsync(project.ProjectId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Project"));
        Assert.That(retrieved.Status, Is.EqualTo(ProjectStatus.InProgress));
        Assert.That(retrieved.Priority, Is.EqualTo(ProjectPriority.High));
    }

    /// <summary>
    /// Tests that Milestones can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Milestones_CanAddAndRetrieve()
    {
        // Arrange
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Project",
            Status = ProjectStatus.InProgress,
            Priority = ProjectPriority.Medium,
            CreatedAt = DateTime.UtcNow,
        };

        var milestone = new Milestone
        {
            MilestoneId = Guid.NewGuid(),
            ProjectId = project.ProjectId,
            Name = "Test Milestone",
            Description = "This is a test milestone",
            IsAchieved = false,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Projects.Add(project);
        _context.Milestones.Add(milestone);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Milestones.FindAsync(milestone.MilestoneId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Milestone"));
        Assert.That(retrieved.IsAchieved, Is.False);
    }

    /// <summary>
    /// Tests that Tasks can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Tasks_CanAddAndRetrieve()
    {
        // Arrange
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Project",
            Status = ProjectStatus.InProgress,
            Priority = ProjectPriority.Medium,
            CreatedAt = DateTime.UtcNow,
        };

        var task = new ProjectTask
        {
            ProjectTaskId = Guid.NewGuid(),
            ProjectId = project.ProjectId,
            Title = "Test Task",
            Description = "This is a test task",
            IsCompleted = false,
            EstimatedHours = 5,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Projects.Add(project);
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Tasks.FindAsync(task.ProjectTaskId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Test Task"));
        Assert.That(retrieved.IsCompleted, Is.False);
        Assert.That(retrieved.EstimatedHours, Is.EqualTo(5));
    }

    /// <summary>
    /// Tests that cascade delete works for related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedEntities()
    {
        // Arrange
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Project",
            Status = ProjectStatus.InProgress,
            Priority = ProjectPriority.Medium,
            CreatedAt = DateTime.UtcNow,
        };

        var milestone = new Milestone
        {
            MilestoneId = Guid.NewGuid(),
            ProjectId = project.ProjectId,
            Name = "Test Milestone",
            IsAchieved = false,
            CreatedAt = DateTime.UtcNow,
        };

        var task = new ProjectTask
        {
            ProjectTaskId = Guid.NewGuid(),
            ProjectId = project.ProjectId,
            Title = "Test Task",
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Projects.Add(project);
        _context.Milestones.Add(milestone);
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        // Act
        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();

        var retrievedMilestone = await _context.Milestones.FindAsync(milestone.MilestoneId);
        var retrievedTask = await _context.Tasks.FindAsync(task.ProjectTaskId);

        // Assert
        Assert.That(retrievedMilestone, Is.Null);
        Assert.That(retrievedTask, Is.Null);
    }

    /// <summary>
    /// Tests that project status can be updated.
    /// </summary>
    [Test]
    public async Task Project_CanUpdateStatus()
    {
        // Arrange
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Project",
            Status = ProjectStatus.Planning,
            Priority = ProjectPriority.Medium,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        // Act
        project.Start();
        await _context.SaveChangesAsync();

        var retrieved = await _context.Projects.FindAsync(project.ProjectId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Status, Is.EqualTo(ProjectStatus.InProgress));
        Assert.That(retrieved.StartDate, Is.Not.Null);
    }
}
