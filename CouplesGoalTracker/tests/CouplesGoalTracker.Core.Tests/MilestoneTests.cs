namespace CouplesGoalTracker.Core.Tests;

public class MilestoneTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesMilestone()
    {
        // Arrange
        var milestoneId = Guid.NewGuid();
        var goalId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var title = "First Milestone";
        var description = "Complete initial step";
        var sortOrder = 1;

        // Act
        var milestone = new Milestone
        {
            MilestoneId = milestoneId,
            GoalId = goalId,
            UserId = userId,
            Title = title,
            Description = description,
            SortOrder = sortOrder
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(milestone.MilestoneId, Is.EqualTo(milestoneId));
            Assert.That(milestone.GoalId, Is.EqualTo(goalId));
            Assert.That(milestone.UserId, Is.EqualTo(userId));
            Assert.That(milestone.Title, Is.EqualTo(title));
            Assert.That(milestone.Description, Is.EqualTo(description));
            Assert.That(milestone.SortOrder, Is.EqualTo(sortOrder));
            Assert.That(milestone.IsCompleted, Is.False);
        });
    }

    [Test]
    public void DefaultValues_AreSetCorrectly()
    {
        // Arrange & Act
        var milestone = new Milestone();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(milestone.Title, Is.EqualTo(string.Empty));
            Assert.That(milestone.IsCompleted, Is.False);
            Assert.That(milestone.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(milestone.CompletedDate, Is.Null);
            Assert.That(milestone.UpdatedAt, Is.Null);
        });
    }

    [Test]
    public void MarkAsCompleted_UpdatesPropertiesCorrectly()
    {
        // Arrange
        var milestone = new Milestone
        {
            MilestoneId = Guid.NewGuid(),
            IsCompleted = false
        };
        var beforeUpdate = DateTime.UtcNow.AddSeconds(-1);

        // Act
        milestone.MarkAsCompleted();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(milestone.IsCompleted, Is.True);
            Assert.That(milestone.CompletedDate, Is.Not.Null);
            Assert.That(milestone.CompletedDate!.Value, Is.GreaterThan(beforeUpdate));
            Assert.That(milestone.UpdatedAt, Is.Not.Null);
            Assert.That(milestone.UpdatedAt!.Value, Is.GreaterThan(beforeUpdate));
        });
    }

    [Test]
    public void MarkAsCompleted_CalledMultipleTimes_UpdatesCompletedDate()
    {
        // Arrange
        var milestone = new Milestone
        {
            MilestoneId = Guid.NewGuid()
        };

        milestone.MarkAsCompleted();
        var firstCompletedDate = milestone.CompletedDate;

        System.Threading.Thread.Sleep(10); // Small delay to ensure different timestamp

        // Act
        milestone.MarkAsCompleted();

        // Assert
        Assert.That(milestone.CompletedDate, Is.GreaterThanOrEqualTo(firstCompletedDate));
    }

    [Test]
    public void IsOverdue_NoTargetDate_ReturnsFalse()
    {
        // Arrange
        var milestone = new Milestone
        {
            MilestoneId = Guid.NewGuid(),
            TargetDate = null,
            IsCompleted = false
        };

        // Act
        var isOverdue = milestone.IsOverdue();

        // Assert
        Assert.That(isOverdue, Is.False);
    }

    [Test]
    public void IsOverdue_CompletedMilestone_ReturnsFalse()
    {
        // Arrange
        var milestone = new Milestone
        {
            MilestoneId = Guid.NewGuid(),
            TargetDate = DateTime.UtcNow.AddDays(-5),
            IsCompleted = true
        };

        // Act
        var isOverdue = milestone.IsOverdue();

        // Assert
        Assert.That(isOverdue, Is.False);
    }

    [Test]
    public void IsOverdue_FutureTargetDate_ReturnsFalse()
    {
        // Arrange
        var milestone = new Milestone
        {
            MilestoneId = Guid.NewGuid(),
            TargetDate = DateTime.UtcNow.AddDays(5),
            IsCompleted = false
        };

        // Act
        var isOverdue = milestone.IsOverdue();

        // Assert
        Assert.That(isOverdue, Is.False);
    }

    [Test]
    public void IsOverdue_PastTargetDateAndNotCompleted_ReturnsTrue()
    {
        // Arrange
        var milestone = new Milestone
        {
            MilestoneId = Guid.NewGuid(),
            TargetDate = DateTime.UtcNow.AddDays(-5),
            IsCompleted = false
        };

        // Act
        var isOverdue = milestone.IsOverdue();

        // Assert
        Assert.That(isOverdue, Is.True);
    }

    [Test]
    public void IsOverdue_TargetDateToday_EdgeCase()
    {
        // Arrange
        var milestone = new Milestone
        {
            MilestoneId = Guid.NewGuid(),
            TargetDate = DateTime.UtcNow.AddMinutes(-1), // Just passed
            IsCompleted = false
        };

        // Act
        var isOverdue = milestone.IsOverdue();

        // Assert
        Assert.That(isOverdue, Is.True);
    }

    [Test]
    public void TargetDate_CanBeSetAndRetrieved()
    {
        // Arrange
        var milestone = new Milestone();
        var targetDate = DateTime.UtcNow.AddDays(30);

        // Act
        milestone.TargetDate = targetDate;

        // Assert
        Assert.That(milestone.TargetDate, Is.EqualTo(targetDate));
    }

    [Test]
    public void SortOrder_CanBeSetToZero()
    {
        // Arrange & Act
        var milestone = new Milestone
        {
            SortOrder = 0
        };

        // Assert
        Assert.That(milestone.SortOrder, Is.EqualTo(0));
    }

    [Test]
    public void Goal_NavigationProperty_CanBeSet()
    {
        // Arrange
        var goal = new Goal { GoalId = Guid.NewGuid() };
        var milestone = new Milestone
        {
            MilestoneId = Guid.NewGuid(),
            GoalId = goal.GoalId
        };

        // Act
        milestone.Goal = goal;

        // Assert
        Assert.That(milestone.Goal, Is.EqualTo(goal));
    }
}
