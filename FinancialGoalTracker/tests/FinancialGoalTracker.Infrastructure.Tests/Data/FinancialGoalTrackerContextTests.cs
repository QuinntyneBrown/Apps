// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FinancialGoalTracker.Infrastructure.Tests;

/// <summary>
/// Unit tests for the FinancialGoalTrackerContext.
/// </summary>
[TestFixture]
public class FinancialGoalTrackerContextTests
{
    private FinancialGoalTrackerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<FinancialGoalTrackerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new FinancialGoalTrackerContext(options);
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
    /// Tests that Goals can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Goals_CanAddAndRetrieve()
    {
        // Arrange
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            Name = "Test Goal",
            Description = "A test financial goal",
            GoalType = GoalType.Savings,
            TargetAmount = 10000.00m,
            CurrentAmount = 2500.00m,
            TargetDate = DateTime.UtcNow.AddMonths(12),
            Status = GoalStatus.InProgress,
            Notes = "Test notes",
        };

        // Act
        _context.Goals.Add(goal);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Goals.FindAsync(goal.GoalId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Goal"));
        Assert.That(retrieved.GoalType, Is.EqualTo(GoalType.Savings));
        Assert.That(retrieved.TargetAmount, Is.EqualTo(10000.00m));
        Assert.That(retrieved.CurrentAmount, Is.EqualTo(2500.00m));
    }

    /// <summary>
    /// Tests that Milestones can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Milestones_CanAddAndRetrieve()
    {
        // Arrange
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            Name = "Test Goal",
            Description = "A test goal",
            GoalType = GoalType.Purchase,
            TargetAmount = 5000.00m,
            CurrentAmount = 1000.00m,
            TargetDate = DateTime.UtcNow.AddMonths(6),
            Status = GoalStatus.InProgress,
        };

        var milestone = new Milestone
        {
            MilestoneId = Guid.NewGuid(),
            GoalId = goal.GoalId,
            Name = "First Milestone",
            TargetAmount = 2500.00m,
            TargetDate = DateTime.UtcNow.AddMonths(3),
            IsCompleted = false,
            Notes = "Halfway there!",
        };

        // Act
        _context.Goals.Add(goal);
        _context.Milestones.Add(milestone);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Milestones.FindAsync(milestone.MilestoneId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("First Milestone"));
        Assert.That(retrieved.TargetAmount, Is.EqualTo(2500.00m));
        Assert.That(retrieved.IsCompleted, Is.False);
    }

    /// <summary>
    /// Tests that Contributions can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Contributions_CanAddAndRetrieve()
    {
        // Arrange
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            Name = "Savings Goal",
            Description = "Test savings",
            GoalType = GoalType.Emergency,
            TargetAmount = 10000.00m,
            CurrentAmount = 500.00m,
            TargetDate = DateTime.UtcNow.AddYears(1),
            Status = GoalStatus.InProgress,
        };

        var contribution = new Contribution
        {
            ContributionId = Guid.NewGuid(),
            GoalId = goal.GoalId,
            Amount = 500.00m,
            ContributionDate = DateTime.UtcNow,
            Notes = "First contribution",
        };

        // Act
        _context.Goals.Add(goal);
        _context.Contributions.Add(contribution);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Contributions.FindAsync(contribution.ContributionId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Amount, Is.EqualTo(500.00m));
        Assert.That(retrieved.Notes, Is.EqualTo("First contribution"));
    }

    /// <summary>
    /// Tests that Goals can be updated.
    /// </summary>
    [Test]
    public async Task Goals_CanUpdate()
    {
        // Arrange
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            Name = "Original Name",
            Description = "Original description",
            GoalType = GoalType.Investment,
            TargetAmount = 5000.00m,
            CurrentAmount = 1000.00m,
            TargetDate = DateTime.UtcNow.AddMonths(6),
            Status = GoalStatus.InProgress,
        };

        _context.Goals.Add(goal);
        await _context.SaveChangesAsync();

        // Act
        goal.Name = "Updated Name";
        goal.CurrentAmount = 2000.00m;
        goal.Status = GoalStatus.InProgress;
        await _context.SaveChangesAsync();

        var retrieved = await _context.Goals.FindAsync(goal.GoalId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Updated Name"));
        Assert.That(retrieved.CurrentAmount, Is.EqualTo(2000.00m));
    }

    /// <summary>
    /// Tests cascade delete for Goal and related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedEntities()
    {
        // Arrange
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            Name = "Goal to Delete",
            Description = "Will be deleted",
            GoalType = GoalType.DebtPayoff,
            TargetAmount = 3000.00m,
            CurrentAmount = 500.00m,
            TargetDate = DateTime.UtcNow.AddMonths(12),
            Status = GoalStatus.InProgress,
        };

        var milestone = new Milestone
        {
            MilestoneId = Guid.NewGuid(),
            GoalId = goal.GoalId,
            Name = "Milestone to Delete",
            TargetAmount = 1500.00m,
            TargetDate = DateTime.UtcNow.AddMonths(6),
            IsCompleted = false,
        };

        var contribution = new Contribution
        {
            ContributionId = Guid.NewGuid(),
            GoalId = goal.GoalId,
            Amount = 500.00m,
            ContributionDate = DateTime.UtcNow,
        };

        _context.Goals.Add(goal);
        _context.Milestones.Add(milestone);
        _context.Contributions.Add(contribution);
        await _context.SaveChangesAsync();

        // Act
        _context.Goals.Remove(goal);
        await _context.SaveChangesAsync();

        var retrievedMilestone = await _context.Milestones.FindAsync(milestone.MilestoneId);
        var retrievedContribution = await _context.Contributions.FindAsync(contribution.ContributionId);

        // Assert
        Assert.That(retrievedMilestone, Is.Null);
        Assert.That(retrievedContribution, Is.Null);
    }

    /// <summary>
    /// Tests that Milestones can be marked as completed.
    /// </summary>
    [Test]
    public async Task Milestones_CanMarkAsCompleted()
    {
        // Arrange
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            Name = "Test Goal",
            Description = "Test",
            GoalType = GoalType.Retirement,
            TargetAmount = 20000.00m,
            CurrentAmount = 5000.00m,
            TargetDate = DateTime.UtcNow.AddYears(5),
            Status = GoalStatus.InProgress,
        };

        var milestone = new Milestone
        {
            MilestoneId = Guid.NewGuid(),
            GoalId = goal.GoalId,
            Name = "Test Milestone",
            TargetAmount = 5000.00m,
            TargetDate = DateTime.UtcNow.AddMonths(12),
            IsCompleted = false,
        };

        _context.Goals.Add(goal);
        _context.Milestones.Add(milestone);
        await _context.SaveChangesAsync();

        // Act
        milestone.IsCompleted = true;
        milestone.CompletedDate = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        var retrieved = await _context.Milestones.FindAsync(milestone.MilestoneId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.IsCompleted, Is.True);
        Assert.That(retrieved.CompletedDate, Is.Not.Null);
    }
}
