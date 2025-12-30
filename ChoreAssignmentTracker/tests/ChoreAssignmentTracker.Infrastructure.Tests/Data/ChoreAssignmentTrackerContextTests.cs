// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ChoreAssignmentTracker.Infrastructure.Tests.Data;

/// <summary>
/// Contains unit tests for the <see cref="ChoreAssignmentTrackerContext"/> class.
/// </summary>
[TestFixture]
public class ChoreAssignmentTrackerContextTests
{
    private DbContextOptions<ChoreAssignmentTrackerContext> _options = null!;
    private ChoreAssignmentTrackerContext _context = null!;
    private Guid _testUserId;

    /// <summary>
    /// Sets up the test context before each test.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        _testUserId = Guid.NewGuid();
        _options = new DbContextOptionsBuilder<ChoreAssignmentTrackerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new ChoreAssignmentTrackerContext(_options);
    }

    /// <summary>
    /// Cleans up resources after each test.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    #region Chore Tests

    /// <summary>
    /// Tests that a chore can be created successfully.
    /// </summary>
    [Test]
    public async Task CreateChore_ShouldAddChoreToDatabase()
    {
        // Arrange
        var chore = new Chore
        {
            ChoreId = Guid.NewGuid(),
            UserId = _testUserId,
            Name = "Wash Dishes",
            Description = "Clean all dishes",
            Frequency = ChoreFrequency.Daily,
            EstimatedMinutes = 20,
            Points = 10,
            Category = "Kitchen",
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        // Act
        _context.Chores.Add(chore);
        await _context.SaveChangesAsync();

        // Assert
        var retrievedChore = await _context.Chores.FindAsync(chore.ChoreId);
        Assert.That(retrievedChore, Is.Not.Null);
        Assert.That(retrievedChore.Name, Is.EqualTo("Wash Dishes"));
        Assert.That(retrievedChore.Frequency, Is.EqualTo(ChoreFrequency.Daily));
        Assert.That(retrievedChore.Points, Is.EqualTo(10));
    }

    /// <summary>
    /// Tests that a chore can be updated successfully.
    /// </summary>
    [Test]
    public async Task UpdateChore_ShouldModifyExistingChore()
    {
        // Arrange
        var chore = new Chore
        {
            ChoreId = Guid.NewGuid(),
            UserId = _testUserId,
            Name = "Original Name",
            Frequency = ChoreFrequency.Daily,
            Points = 10,
            CreatedAt = DateTime.UtcNow
        };
        _context.Chores.Add(chore);
        await _context.SaveChangesAsync();

        // Act
        chore.Name = "Updated Name";
        chore.Points = 20;
        await _context.SaveChangesAsync();

        // Assert
        var updatedChore = await _context.Chores.FindAsync(chore.ChoreId);
        Assert.That(updatedChore, Is.Not.Null);
        Assert.That(updatedChore.Name, Is.EqualTo("Updated Name"));
        Assert.That(updatedChore.Points, Is.EqualTo(20));
    }

    /// <summary>
    /// Tests that a chore can be deleted successfully.
    /// </summary>
    [Test]
    public async Task DeleteChore_ShouldRemoveChoreFromDatabase()
    {
        // Arrange
        var chore = new Chore
        {
            ChoreId = Guid.NewGuid(),
            UserId = _testUserId,
            Name = "To Delete",
            Frequency = ChoreFrequency.Daily,
            Points = 10,
            CreatedAt = DateTime.UtcNow
        };
        _context.Chores.Add(chore);
        await _context.SaveChangesAsync();

        // Act
        _context.Chores.Remove(chore);
        await _context.SaveChangesAsync();

        // Assert
        var deletedChore = await _context.Chores.FindAsync(chore.ChoreId);
        Assert.That(deletedChore, Is.Null);
    }

    /// <summary>
    /// Tests that multiple chores can be queried successfully.
    /// </summary>
    [Test]
    public async Task QueryChores_ShouldReturnAllChores()
    {
        // Arrange
        var chores = new List<Chore>
        {
            new()
            {
                ChoreId = Guid.NewGuid(),
                UserId = _testUserId,
                Name = "Chore 1",
                Frequency = ChoreFrequency.Daily,
                Points = 10,
                CreatedAt = DateTime.UtcNow
            },
            new()
            {
                ChoreId = Guid.NewGuid(),
                UserId = _testUserId,
                Name = "Chore 2",
                Frequency = ChoreFrequency.Weekly,
                Points = 20,
                CreatedAt = DateTime.UtcNow
            }
        };
        _context.Chores.AddRange(chores);
        await _context.SaveChangesAsync();

        // Act
        var retrievedChores = await _context.Chores.ToListAsync();

        // Assert
        Assert.That(retrievedChores, Has.Count.EqualTo(2));
    }

    #endregion

    #region FamilyMember Tests

    /// <summary>
    /// Tests that a family member can be created successfully.
    /// </summary>
    [Test]
    public async Task CreateFamilyMember_ShouldAddFamilyMemberToDatabase()
    {
        // Arrange
        var familyMember = new FamilyMember
        {
            FamilyMemberId = Guid.NewGuid(),
            UserId = _testUserId,
            Name = "Emma",
            Age = 12,
            Avatar = "girl-icon",
            TotalPoints = 100,
            AvailablePoints = 50,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        // Act
        _context.FamilyMembers.Add(familyMember);
        await _context.SaveChangesAsync();

        // Assert
        var retrieved = await _context.FamilyMembers.FindAsync(familyMember.FamilyMemberId);
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved.Name, Is.EqualTo("Emma"));
        Assert.That(retrieved.Age, Is.EqualTo(12));
        Assert.That(retrieved.TotalPoints, Is.EqualTo(100));
    }

    /// <summary>
    /// Tests that a family member can be updated successfully.
    /// </summary>
    [Test]
    public async Task UpdateFamilyMember_ShouldModifyExistingFamilyMember()
    {
        // Arrange
        var familyMember = new FamilyMember
        {
            FamilyMemberId = Guid.NewGuid(),
            UserId = _testUserId,
            Name = "Original",
            TotalPoints = 50,
            AvailablePoints = 25,
            CreatedAt = DateTime.UtcNow
        };
        _context.FamilyMembers.Add(familyMember);
        await _context.SaveChangesAsync();

        // Act
        familyMember.TotalPoints = 100;
        familyMember.AvailablePoints = 75;
        await _context.SaveChangesAsync();

        // Assert
        var updated = await _context.FamilyMembers.FindAsync(familyMember.FamilyMemberId);
        Assert.That(updated, Is.Not.Null);
        Assert.That(updated.TotalPoints, Is.EqualTo(100));
        Assert.That(updated.AvailablePoints, Is.EqualTo(75));
    }

    /// <summary>
    /// Tests that a family member can be deleted successfully.
    /// </summary>
    [Test]
    public async Task DeleteFamilyMember_ShouldRemoveFamilyMemberFromDatabase()
    {
        // Arrange
        var familyMember = new FamilyMember
        {
            FamilyMemberId = Guid.NewGuid(),
            UserId = _testUserId,
            Name = "To Delete",
            CreatedAt = DateTime.UtcNow
        };
        _context.FamilyMembers.Add(familyMember);
        await _context.SaveChangesAsync();

        // Act
        _context.FamilyMembers.Remove(familyMember);
        await _context.SaveChangesAsync();

        // Assert
        var deleted = await _context.FamilyMembers.FindAsync(familyMember.FamilyMemberId);
        Assert.That(deleted, Is.Null);
    }

    #endregion

    #region Assignment Tests

    /// <summary>
    /// Tests that an assignment can be created successfully.
    /// </summary>
    [Test]
    public async Task CreateAssignment_ShouldAddAssignmentToDatabase()
    {
        // Arrange
        var chore = new Chore
        {
            ChoreId = Guid.NewGuid(),
            UserId = _testUserId,
            Name = "Test Chore",
            Frequency = ChoreFrequency.Daily,
            Points = 10,
            CreatedAt = DateTime.UtcNow
        };
        var familyMember = new FamilyMember
        {
            FamilyMemberId = Guid.NewGuid(),
            UserId = _testUserId,
            Name = "Test Member",
            CreatedAt = DateTime.UtcNow
        };
        _context.Chores.Add(chore);
        _context.FamilyMembers.Add(familyMember);
        await _context.SaveChangesAsync();

        var assignment = new Assignment
        {
            AssignmentId = Guid.NewGuid(),
            ChoreId = chore.ChoreId,
            FamilyMemberId = familyMember.FamilyMemberId,
            AssignedDate = DateTime.UtcNow,
            DueDate = DateTime.UtcNow.AddDays(1),
            IsCompleted = false,
            IsVerified = false,
            PointsEarned = 0,
            CreatedAt = DateTime.UtcNow
        };

        // Act
        _context.Assignments.Add(assignment);
        await _context.SaveChangesAsync();

        // Assert
        var retrieved = await _context.Assignments.FindAsync(assignment.AssignmentId);
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved.ChoreId, Is.EqualTo(chore.ChoreId));
        Assert.That(retrieved.FamilyMemberId, Is.EqualTo(familyMember.FamilyMemberId));
    }

    /// <summary>
    /// Tests that an assignment can be updated successfully.
    /// </summary>
    [Test]
    public async Task UpdateAssignment_ShouldModifyExistingAssignment()
    {
        // Arrange
        var chore = new Chore
        {
            ChoreId = Guid.NewGuid(),
            UserId = _testUserId,
            Name = "Test Chore",
            Frequency = ChoreFrequency.Daily,
            Points = 10,
            CreatedAt = DateTime.UtcNow
        };
        var familyMember = new FamilyMember
        {
            FamilyMemberId = Guid.NewGuid(),
            UserId = _testUserId,
            Name = "Test Member",
            CreatedAt = DateTime.UtcNow
        };
        _context.Chores.Add(chore);
        _context.FamilyMembers.Add(familyMember);
        await _context.SaveChangesAsync();

        var assignment = new Assignment
        {
            AssignmentId = Guid.NewGuid(),
            ChoreId = chore.ChoreId,
            FamilyMemberId = familyMember.FamilyMemberId,
            AssignedDate = DateTime.UtcNow,
            DueDate = DateTime.UtcNow.AddDays(1),
            IsCompleted = false,
            PointsEarned = 0,
            CreatedAt = DateTime.UtcNow
        };
        _context.Assignments.Add(assignment);
        await _context.SaveChangesAsync();

        // Act
        assignment.IsCompleted = true;
        assignment.CompletedDate = DateTime.UtcNow;
        assignment.PointsEarned = 10;
        await _context.SaveChangesAsync();

        // Assert
        var updated = await _context.Assignments.FindAsync(assignment.AssignmentId);
        Assert.That(updated, Is.Not.Null);
        Assert.That(updated.IsCompleted, Is.True);
        Assert.That(updated.PointsEarned, Is.EqualTo(10));
    }

    /// <summary>
    /// Tests that an assignment can be deleted successfully.
    /// </summary>
    [Test]
    public async Task DeleteAssignment_ShouldRemoveAssignmentFromDatabase()
    {
        // Arrange
        var chore = new Chore
        {
            ChoreId = Guid.NewGuid(),
            UserId = _testUserId,
            Name = "Test Chore",
            Frequency = ChoreFrequency.Daily,
            Points = 10,
            CreatedAt = DateTime.UtcNow
        };
        var familyMember = new FamilyMember
        {
            FamilyMemberId = Guid.NewGuid(),
            UserId = _testUserId,
            Name = "Test Member",
            CreatedAt = DateTime.UtcNow
        };
        _context.Chores.Add(chore);
        _context.FamilyMembers.Add(familyMember);
        await _context.SaveChangesAsync();

        var assignment = new Assignment
        {
            AssignmentId = Guid.NewGuid(),
            ChoreId = chore.ChoreId,
            FamilyMemberId = familyMember.FamilyMemberId,
            AssignedDate = DateTime.UtcNow,
            DueDate = DateTime.UtcNow.AddDays(1),
            CreatedAt = DateTime.UtcNow
        };
        _context.Assignments.Add(assignment);
        await _context.SaveChangesAsync();

        // Act
        _context.Assignments.Remove(assignment);
        await _context.SaveChangesAsync();

        // Assert
        var deleted = await _context.Assignments.FindAsync(assignment.AssignmentId);
        Assert.That(deleted, Is.Null);
    }

    #endregion

    #region Reward Tests

    /// <summary>
    /// Tests that a reward can be created successfully.
    /// </summary>
    [Test]
    public async Task CreateReward_ShouldAddRewardToDatabase()
    {
        // Arrange
        var reward = new Reward
        {
            RewardId = Guid.NewGuid(),
            UserId = _testUserId,
            Name = "Extra Screen Time",
            Description = "30 extra minutes",
            PointCost = 50,
            Category = "Entertainment",
            IsAvailable = true,
            CreatedAt = DateTime.UtcNow
        };

        // Act
        _context.Rewards.Add(reward);
        await _context.SaveChangesAsync();

        // Assert
        var retrieved = await _context.Rewards.FindAsync(reward.RewardId);
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved.Name, Is.EqualTo("Extra Screen Time"));
        Assert.That(retrieved.PointCost, Is.EqualTo(50));
    }

    /// <summary>
    /// Tests that a reward can be updated successfully.
    /// </summary>
    [Test]
    public async Task UpdateReward_ShouldModifyExistingReward()
    {
        // Arrange
        var reward = new Reward
        {
            RewardId = Guid.NewGuid(),
            UserId = _testUserId,
            Name = "Original Reward",
            PointCost = 50,
            IsAvailable = true,
            CreatedAt = DateTime.UtcNow
        };
        _context.Rewards.Add(reward);
        await _context.SaveChangesAsync();

        // Act
        reward.Name = "Updated Reward";
        reward.PointCost = 75;
        reward.IsAvailable = false;
        await _context.SaveChangesAsync();

        // Assert
        var updated = await _context.Rewards.FindAsync(reward.RewardId);
        Assert.That(updated, Is.Not.Null);
        Assert.That(updated.Name, Is.EqualTo("Updated Reward"));
        Assert.That(updated.PointCost, Is.EqualTo(75));
        Assert.That(updated.IsAvailable, Is.False);
    }

    /// <summary>
    /// Tests that a reward can be deleted successfully.
    /// </summary>
    [Test]
    public async Task DeleteReward_ShouldRemoveRewardFromDatabase()
    {
        // Arrange
        var reward = new Reward
        {
            RewardId = Guid.NewGuid(),
            UserId = _testUserId,
            Name = "To Delete",
            PointCost = 50,
            CreatedAt = DateTime.UtcNow
        };
        _context.Rewards.Add(reward);
        await _context.SaveChangesAsync();

        // Act
        _context.Rewards.Remove(reward);
        await _context.SaveChangesAsync();

        // Assert
        var deleted = await _context.Rewards.FindAsync(reward.RewardId);
        Assert.That(deleted, Is.Null);
    }

    #endregion

    #region Relationship Tests

    /// <summary>
    /// Tests that relationships between chores and assignments work correctly.
    /// </summary>
    [Test]
    public async Task ChoreAssignmentRelationship_ShouldLoadCorrectly()
    {
        // Arrange
        var chore = new Chore
        {
            ChoreId = Guid.NewGuid(),
            UserId = _testUserId,
            Name = "Test Chore",
            Frequency = ChoreFrequency.Daily,
            Points = 10,
            CreatedAt = DateTime.UtcNow
        };
        var familyMember = new FamilyMember
        {
            FamilyMemberId = Guid.NewGuid(),
            UserId = _testUserId,
            Name = "Test Member",
            CreatedAt = DateTime.UtcNow
        };
        _context.Chores.Add(chore);
        _context.FamilyMembers.Add(familyMember);
        await _context.SaveChangesAsync();

        var assignment = new Assignment
        {
            AssignmentId = Guid.NewGuid(),
            ChoreId = chore.ChoreId,
            FamilyMemberId = familyMember.FamilyMemberId,
            AssignedDate = DateTime.UtcNow,
            DueDate = DateTime.UtcNow.AddDays(1),
            CreatedAt = DateTime.UtcNow
        };
        _context.Assignments.Add(assignment);
        await _context.SaveChangesAsync();

        // Act
        var loadedChore = await _context.Chores
            .Include(c => c.Assignments)
            .FirstOrDefaultAsync(c => c.ChoreId == chore.ChoreId);

        // Assert
        Assert.That(loadedChore, Is.Not.Null);
        Assert.That(loadedChore.Assignments, Has.Count.EqualTo(1));
        Assert.That(loadedChore.Assignments.First().AssignmentId, Is.EqualTo(assignment.AssignmentId));
    }

    #endregion
}
