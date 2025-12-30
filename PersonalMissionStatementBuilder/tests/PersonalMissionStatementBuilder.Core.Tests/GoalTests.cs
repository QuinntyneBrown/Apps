namespace PersonalMissionStatementBuilder.Core.Tests;

public class GoalTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesGoal()
    {
        // Arrange
        var goalId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var missionStatementId = Guid.NewGuid();
        var targetDate = DateTime.UtcNow.AddMonths(6);

        // Act
        var goal = new Goal
        {
            GoalId = goalId,
            UserId = userId,
            MissionStatementId = missionStatementId,
            Title = "Learn Spanish",
            Description = "Become conversational in Spanish",
            Status = GoalStatus.InProgress,
            TargetDate = targetDate
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(goal.GoalId, Is.EqualTo(goalId));
            Assert.That(goal.UserId, Is.EqualTo(userId));
            Assert.That(goal.MissionStatementId, Is.EqualTo(missionStatementId));
            Assert.That(goal.Title, Is.EqualTo("Learn Spanish"));
            Assert.That(goal.Description, Is.EqualTo("Become conversational in Spanish"));
            Assert.That(goal.Status, Is.EqualTo(GoalStatus.InProgress));
            Assert.That(goal.TargetDate, Is.EqualTo(targetDate));
            Assert.That(goal.CompletedDate, Is.Null);
            Assert.That(goal.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(goal.UpdatedAt, Is.Null);
        });
    }

    [Test]
    public void MarkAsCompleted_SetsStatusToCompletedAndSetsDate()
    {
        // Arrange
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Goal",
            Status = GoalStatus.InProgress
        };

        // Act
        goal.MarkAsCompleted();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(goal.Status, Is.EqualTo(GoalStatus.Completed));
            Assert.That(goal.CompletedDate, Is.Not.Null);
            Assert.That(goal.UpdatedAt, Is.Not.Null);
        });
    }

    [Test]
    public void MarkAsCompleted_AlreadyCompleted_UpdatesTimestamp()
    {
        // Arrange
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Goal",
            Status = GoalStatus.Completed,
            CompletedDate = DateTime.UtcNow.AddDays(-1)
        };

        // Act
        goal.MarkAsCompleted();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(goal.Status, Is.EqualTo(GoalStatus.Completed));
            Assert.That(goal.CompletedDate, Is.Not.Null);
        });
    }

    [Test]
    public void UpdateStatus_ValidStatus_UpdatesStatusAndTimestamp()
    {
        // Arrange
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Goal",
            Status = GoalStatus.NotStarted
        };

        // Act
        goal.UpdateStatus(GoalStatus.InProgress);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(goal.Status, Is.EqualTo(GoalStatus.InProgress));
            Assert.That(goal.UpdatedAt, Is.Not.Null);
        });
    }

    [Test]
    public void UpdateStatus_ToOnHold_UpdatesCorrectly()
    {
        // Arrange
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Goal",
            Status = GoalStatus.InProgress
        };

        // Act
        goal.UpdateStatus(GoalStatus.OnHold);

        // Assert
        Assert.That(goal.Status, Is.EqualTo(GoalStatus.OnHold));
    }

    [Test]
    public void UpdateStatus_ToAbandoned_UpdatesCorrectly()
    {
        // Arrange
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Goal",
            Status = GoalStatus.InProgress
        };

        // Act
        goal.UpdateStatus(GoalStatus.Abandoned);

        // Assert
        Assert.That(goal.Status, Is.EqualTo(GoalStatus.Abandoned));
    }

    [Test]
    public void Goal_DefaultStatus_IsNotStarted()
    {
        // Arrange & Act
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Goal"
        };

        // Assert
        Assert.That(goal.Status, Is.EqualTo(GoalStatus.NotStarted));
    }

    [Test]
    public void Goal_AllStatuses_CanBeAssigned()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(() => new Goal { Status = GoalStatus.NotStarted }, Throws.Nothing);
            Assert.That(() => new Goal { Status = GoalStatus.InProgress }, Throws.Nothing);
            Assert.That(() => new Goal { Status = GoalStatus.Completed }, Throws.Nothing);
            Assert.That(() => new Goal { Status = GoalStatus.OnHold }, Throws.Nothing);
            Assert.That(() => new Goal { Status = GoalStatus.Abandoned }, Throws.Nothing);
        });
    }

    [Test]
    public void Goal_CanHaveProgresses()
    {
        // Arrange
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Goal",
            Progresses = new List<Progress>
            {
                new Progress { ProgressId = Guid.NewGuid(), CompletionPercentage = 25 },
                new Progress { ProgressId = Guid.NewGuid(), CompletionPercentage = 50 }
            }
        };

        // Assert
        Assert.That(goal.Progresses.Count, Is.EqualTo(2));
    }

    [Test]
    public void Goal_WithoutTargetDate_CanBeCreated()
    {
        // Arrange & Act
        var goal = new Goal
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Goal",
            TargetDate = null
        };

        // Assert
        Assert.That(goal.TargetDate, Is.Null);
    }
}
