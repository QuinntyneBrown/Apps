namespace CouplesGoalTracker.Core.Tests;

public class EventTests
{
    [Test]
    public void GoalCreatedEvent_CanBeCreated()
    {
        // Arrange
        var goalId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var title = "New Goal";
        var category = GoalCategory.Financial;

        // Act
        var evt = new GoalCreatedEvent
        {
            GoalId = goalId,
            UserId = userId,
            Title = title,
            Category = category
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.GoalId, Is.EqualTo(goalId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Title, Is.EqualTo(title));
            Assert.That(evt.Category, Is.EqualTo(category));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void GoalStatusChangedEvent_CanBeCreated()
    {
        // Arrange
        var goalId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var oldStatus = GoalStatus.NotStarted;
        var newStatus = GoalStatus.InProgress;

        // Act
        var evt = new GoalStatusChangedEvent
        {
            GoalId = goalId,
            UserId = userId,
            OldStatus = oldStatus,
            NewStatus = newStatus
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.GoalId, Is.EqualTo(goalId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.OldStatus, Is.EqualTo(oldStatus));
            Assert.That(evt.NewStatus, Is.EqualTo(newStatus));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void GoalCompletedEvent_CanBeCreated()
    {
        // Arrange
        var goalId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var completedDate = DateTime.UtcNow;

        // Act
        var evt = new GoalCompletedEvent
        {
            GoalId = goalId,
            UserId = userId,
            CompletedDate = completedDate
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.GoalId, Is.EqualTo(goalId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.CompletedDate, Is.EqualTo(completedDate));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void MilestoneCreatedEvent_CanBeCreated()
    {
        // Arrange
        var milestoneId = Guid.NewGuid();
        var goalId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var title = "New Milestone";

        // Act
        var evt = new MilestoneCreatedEvent
        {
            MilestoneId = milestoneId,
            GoalId = goalId,
            UserId = userId,
            Title = title
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.MilestoneId, Is.EqualTo(milestoneId));
            Assert.That(evt.GoalId, Is.EqualTo(goalId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Title, Is.EqualTo(title));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void MilestoneCompletedEvent_CanBeCreated()
    {
        // Arrange
        var milestoneId = Guid.NewGuid();
        var goalId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var completedDate = DateTime.UtcNow;

        // Act
        var evt = new MilestoneCompletedEvent
        {
            MilestoneId = milestoneId,
            GoalId = goalId,
            UserId = userId,
            CompletedDate = completedDate
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.MilestoneId, Is.EqualTo(milestoneId));
            Assert.That(evt.GoalId, Is.EqualTo(goalId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.CompletedDate, Is.EqualTo(completedDate));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void ProgressCreatedEvent_CanBeCreated()
    {
        // Arrange
        var progressId = Guid.NewGuid();
        var goalId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var completionPercentage = 45.5;

        // Act
        var evt = new ProgressCreatedEvent
        {
            ProgressId = progressId,
            GoalId = goalId,
            UserId = userId,
            CompletionPercentage = completionPercentage
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ProgressId, Is.EqualTo(progressId));
            Assert.That(evt.GoalId, Is.EqualTo(goalId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.CompletionPercentage, Is.EqualTo(completionPercentage));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void ProgressUpdatedEvent_CanBeCreated()
    {
        // Arrange
        var progressId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var newCompletionPercentage = 75.0;

        // Act
        var evt = new ProgressUpdatedEvent
        {
            ProgressId = progressId,
            UserId = userId,
            NewCompletionPercentage = newCompletionPercentage
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ProgressId, Is.EqualTo(progressId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.NewCompletionPercentage, Is.EqualTo(newCompletionPercentage));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void Events_AreRecords()
    {
        // This ensures events are immutable and have value-based equality
        var goalId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var event1 = new GoalCreatedEvent
        {
            GoalId = goalId,
            UserId = userId,
            Title = "Test",
            Category = GoalCategory.Financial,
            Timestamp = new DateTime(2024, 1, 1)
        };

        var event2 = new GoalCreatedEvent
        {
            GoalId = goalId,
            UserId = userId,
            Title = "Test",
            Category = GoalCategory.Financial,
            Timestamp = new DateTime(2024, 1, 1)
        };

        // Assert
        Assert.That(event1, Is.EqualTo(event2));
    }
}
