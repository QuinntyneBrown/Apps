namespace CouplesGoalTracker.Core.Tests;

public class GoalTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesGoal()
    {
        // Arrange
        var goalId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var title = "Our Financial Goal";
        var description = "Save for house";
        var category = GoalCategory.Financial;
        var status = GoalStatus.NotStarted;
        var priority = 4;

        // Act
        var goal = new Goal
        {
            GoalId = goalId,
            UserId = userId,
            Title = title,
            Description = description,
            Category = category,
            Status = status,
            Priority = priority
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(goal.GoalId, Is.EqualTo(goalId));
            Assert.That(goal.UserId, Is.EqualTo(userId));
            Assert.That(goal.Title, Is.EqualTo(title));
            Assert.That(goal.Description, Is.EqualTo(description));
            Assert.That(goal.Category, Is.EqualTo(category));
            Assert.That(goal.Status, Is.EqualTo(status));
            Assert.That(goal.Priority, Is.EqualTo(priority));
            Assert.That(goal.IsShared, Is.True);
            Assert.That(goal.Milestones, Is.Not.Null);
            Assert.That(goal.Progresses, Is.Not.Null);
        });
    }

    [Test]
    public void DefaultValues_AreSetCorrectly()
    {
        // Arrange & Act
        var goal = new Goal();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(goal.Title, Is.EqualTo(string.Empty));
            Assert.That(goal.Description, Is.EqualTo(string.Empty));
            Assert.That(goal.Priority, Is.EqualTo(3));
            Assert.That(goal.IsShared, Is.True);
            Assert.That(goal.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(goal.Milestones, Is.Not.Null.And.Empty);
            Assert.That(goal.Progresses, Is.Not.Null.And.Empty);
        });
    }

    [Test]
    public void MarkAsCompleted_UpdatesStatusAndDates()
    {
        // Arrange
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            Status = GoalStatus.InProgress
        };
        var beforeUpdate = DateTime.UtcNow.AddSeconds(-1);

        // Act
        goal.MarkAsCompleted();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(goal.Status, Is.EqualTo(GoalStatus.Completed));
            Assert.That(goal.CompletedDate, Is.Not.Null);
            Assert.That(goal.CompletedDate!.Value, Is.GreaterThan(beforeUpdate));
            Assert.That(goal.UpdatedAt, Is.Not.Null);
            Assert.That(goal.UpdatedAt!.Value, Is.GreaterThan(beforeUpdate));
        });
    }

    [Test]
    public void MarkAsInProgress_UpdatesStatusAndDate()
    {
        // Arrange
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            Status = GoalStatus.NotStarted
        };
        var beforeUpdate = DateTime.UtcNow.AddSeconds(-1);

        // Act
        goal.MarkAsInProgress();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(goal.Status, Is.EqualTo(GoalStatus.InProgress));
            Assert.That(goal.UpdatedAt, Is.Not.Null);
            Assert.That(goal.UpdatedAt!.Value, Is.GreaterThan(beforeUpdate));
        });
    }

    [Test]
    public void CalculateCompletionPercentage_NoMilestones_ReturnsZero()
    {
        // Arrange
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            Milestones = new List<Milestone>()
        };

        // Act
        var percentage = goal.CalculateCompletionPercentage();

        // Assert
        Assert.That(percentage, Is.EqualTo(0));
    }

    [Test]
    public void CalculateCompletionPercentage_NullMilestones_ReturnsZero()
    {
        // Arrange
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            Milestones = null!
        };

        // Act
        var percentage = goal.CalculateCompletionPercentage();

        // Assert
        Assert.That(percentage, Is.EqualTo(0));
    }

    [Test]
    public void CalculateCompletionPercentage_AllMilestonesCompleted_Returns100()
    {
        // Arrange
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            Milestones = new List<Milestone>
            {
                new Milestone { MilestoneId = Guid.NewGuid(), IsCompleted = true },
                new Milestone { MilestoneId = Guid.NewGuid(), IsCompleted = true },
                new Milestone { MilestoneId = Guid.NewGuid(), IsCompleted = true }
            }
        };

        // Act
        var percentage = goal.CalculateCompletionPercentage();

        // Assert
        Assert.That(percentage, Is.EqualTo(100));
    }

    [Test]
    public void CalculateCompletionPercentage_SomeMilestonesCompleted_ReturnsCorrectPercentage()
    {
        // Arrange
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            Milestones = new List<Milestone>
            {
                new Milestone { MilestoneId = Guid.NewGuid(), IsCompleted = true },
                new Milestone { MilestoneId = Guid.NewGuid(), IsCompleted = false },
                new Milestone { MilestoneId = Guid.NewGuid(), IsCompleted = true },
                new Milestone { MilestoneId = Guid.NewGuid(), IsCompleted = false }
            }
        };

        // Act
        var percentage = goal.CalculateCompletionPercentage();

        // Assert
        Assert.That(percentage, Is.EqualTo(50));
    }

    [Test]
    public void CalculateCompletionPercentage_OneOfThreeCompleted_ReturnsCorrectPercentage()
    {
        // Arrange
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            Milestones = new List<Milestone>
            {
                new Milestone { MilestoneId = Guid.NewGuid(), IsCompleted = true },
                new Milestone { MilestoneId = Guid.NewGuid(), IsCompleted = false },
                new Milestone { MilestoneId = Guid.NewGuid(), IsCompleted = false }
            }
        };

        // Act
        var percentage = goal.CalculateCompletionPercentage();

        // Assert
        Assert.That(percentage, Is.EqualTo(33.333333333333336).Within(0.0001));
    }

    [Test]
    public void TargetDate_CanBeSetAndRetrieved()
    {
        // Arrange
        var goal = new Goal();
        var targetDate = DateTime.UtcNow.AddMonths(6);

        // Act
        goal.TargetDate = targetDate;

        // Assert
        Assert.That(goal.TargetDate, Is.EqualTo(targetDate));
    }

    [Test]
    public void IsShared_CanBeSetToFalse()
    {
        // Arrange
        var goal = new Goal
        {
            IsShared = false
        };

        // Assert
        Assert.That(goal.IsShared, Is.False);
    }

    [Test]
    public void Priority_CanBeSetToCustomValue()
    {
        // Arrange & Act
        var goal = new Goal
        {
            Priority = 5
        };

        // Assert
        Assert.That(goal.Priority, Is.EqualTo(5));
    }
}
