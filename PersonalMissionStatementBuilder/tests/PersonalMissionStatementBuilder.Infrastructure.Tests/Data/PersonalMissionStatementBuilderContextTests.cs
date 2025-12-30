// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalMissionStatementBuilder.Infrastructure.Tests;

/// <summary>
/// Unit tests for the PersonalMissionStatementBuilderContext.
/// </summary>
[TestFixture]
public class PersonalMissionStatementBuilderContextTests
{
    private PersonalMissionStatementBuilderContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<PersonalMissionStatementBuilderContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new PersonalMissionStatementBuilderContext(options);
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
    /// Tests that MissionStatements can be added and retrieved.
    /// </summary>
    [Test]
    public async Task MissionStatements_CanAddAndRetrieve()
    {
        // Arrange
        var missionStatement = new MissionStatement
        {
            MissionStatementId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Mission Statement",
            Text = "This is a test mission statement",
            Version = 1,
            IsCurrentVersion = true,
            StatementDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.MissionStatements.Add(missionStatement);
        await _context.SaveChangesAsync();

        var retrieved = await _context.MissionStatements.FindAsync(missionStatement.MissionStatementId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Test Mission Statement"));
        Assert.That(retrieved.Text, Is.EqualTo("This is a test mission statement"));
        Assert.That(retrieved.Version, Is.EqualTo(1));
    }

    /// <summary>
    /// Tests that Values can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Values_CanAddAndRetrieve()
    {
        // Arrange
        var missionStatement = new MissionStatement
        {
            MissionStatementId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Mission Statement",
            Text = "This is a test mission statement",
            Version = 1,
            IsCurrentVersion = true,
            StatementDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        var value = new Value
        {
            ValueId = Guid.NewGuid(),
            MissionStatementId = missionStatement.MissionStatementId,
            UserId = missionStatement.UserId,
            Name = "Integrity",
            Description = "Always be honest",
            Priority = 1,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.MissionStatements.Add(missionStatement);
        _context.Values.Add(value);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Values.FindAsync(value.ValueId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Integrity"));
        Assert.That(retrieved.Description, Is.EqualTo("Always be honest"));
        Assert.That(retrieved.Priority, Is.EqualTo(1));
    }

    /// <summary>
    /// Tests that Goals can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Goals_CanAddAndRetrieve()
    {
        // Arrange
        var missionStatement = new MissionStatement
        {
            MissionStatementId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Mission Statement",
            Text = "This is a test mission statement",
            Version = 1,
            IsCurrentVersion = true,
            StatementDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            MissionStatementId = missionStatement.MissionStatementId,
            UserId = missionStatement.UserId,
            Title = "Complete Certification",
            Description = "Earn a professional certification",
            Status = GoalStatus.InProgress,
            TargetDate = DateTime.UtcNow.AddMonths(6),
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.MissionStatements.Add(missionStatement);
        _context.Goals.Add(goal);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Goals.FindAsync(goal.GoalId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Complete Certification"));
        Assert.That(retrieved.Status, Is.EqualTo(GoalStatus.InProgress));
    }

    /// <summary>
    /// Tests that Progresses can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Progresses_CanAddAndRetrieve()
    {
        // Arrange
        var missionStatement = new MissionStatement
        {
            MissionStatementId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Mission Statement",
            Text = "This is a test mission statement",
            Version = 1,
            IsCurrentVersion = true,
            StatementDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            MissionStatementId = missionStatement.MissionStatementId,
            UserId = missionStatement.UserId,
            Title = "Complete Certification",
            Description = "Earn a professional certification",
            Status = GoalStatus.InProgress,
            CreatedAt = DateTime.UtcNow,
        };

        var progress = new Progress
        {
            ProgressId = Guid.NewGuid(),
            GoalId = goal.GoalId,
            UserId = goal.UserId,
            ProgressDate = DateTime.UtcNow,
            Notes = "Completed first module",
            CompletionPercentage = 25,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.MissionStatements.Add(missionStatement);
        _context.Goals.Add(goal);
        _context.Progresses.Add(progress);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Progresses.FindAsync(progress.ProgressId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Notes, Is.EqualTo("Completed first module"));
        Assert.That(retrieved.CompletionPercentage, Is.EqualTo(25));
    }

    /// <summary>
    /// Tests that cascade delete works for related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedEntities()
    {
        // Arrange
        var missionStatement = new MissionStatement
        {
            MissionStatementId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Mission Statement",
            Text = "This is a test mission statement",
            Version = 1,
            IsCurrentVersion = true,
            StatementDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        var value = new Value
        {
            ValueId = Guid.NewGuid(),
            MissionStatementId = missionStatement.MissionStatementId,
            UserId = missionStatement.UserId,
            Name = "Integrity",
            Priority = 1,
            CreatedAt = DateTime.UtcNow,
        };

        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            MissionStatementId = missionStatement.MissionStatementId,
            UserId = missionStatement.UserId,
            Title = "Complete Certification",
            Status = GoalStatus.InProgress,
            CreatedAt = DateTime.UtcNow,
        };

        _context.MissionStatements.Add(missionStatement);
        _context.Values.Add(value);
        _context.Goals.Add(goal);
        await _context.SaveChangesAsync();

        // Act
        _context.MissionStatements.Remove(missionStatement);
        await _context.SaveChangesAsync();

        var retrievedValue = await _context.Values.FindAsync(value.ValueId);
        var retrievedGoal = await _context.Goals.FindAsync(goal.GoalId);

        // Assert
        Assert.That(retrievedValue, Is.Null);
        Assert.That(retrievedGoal, Is.Null);
    }
}
