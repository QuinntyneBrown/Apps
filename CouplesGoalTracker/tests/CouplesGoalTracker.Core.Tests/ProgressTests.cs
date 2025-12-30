namespace CouplesGoalTracker.Core.Tests;

public class ProgressTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesProgress()
    {
        // Arrange
        var progressId = Guid.NewGuid();
        var goalId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var notes = "Made good progress today";
        var completionPercentage = 45.5;
        var effortHours = 2.5m;

        // Act
        var progress = new Progress
        {
            ProgressId = progressId,
            GoalId = goalId,
            UserId = userId,
            Notes = notes,
            CompletionPercentage = completionPercentage,
            EffortHours = effortHours
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(progress.ProgressId, Is.EqualTo(progressId));
            Assert.That(progress.GoalId, Is.EqualTo(goalId));
            Assert.That(progress.UserId, Is.EqualTo(userId));
            Assert.That(progress.Notes, Is.EqualTo(notes));
            Assert.That(progress.CompletionPercentage, Is.EqualTo(completionPercentage));
            Assert.That(progress.EffortHours, Is.EqualTo(effortHours));
            Assert.That(progress.IsSignificant, Is.False);
        });
    }

    [Test]
    public void DefaultValues_AreSetCorrectly()
    {
        // Arrange & Act
        var progress = new Progress();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(progress.Notes, Is.EqualTo(string.Empty));
            Assert.That(progress.CompletionPercentage, Is.EqualTo(0));
            Assert.That(progress.IsSignificant, Is.False);
            Assert.That(progress.ProgressDate, Is.Not.EqualTo(default(DateTime)));
            Assert.That(progress.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void MarkAsSignificant_UpdatesPropertiesCorrectly()
    {
        // Arrange
        var progress = new Progress
        {
            ProgressId = Guid.NewGuid(),
            IsSignificant = false
        };
        var beforeUpdate = DateTime.UtcNow.AddSeconds(-1);

        // Act
        progress.MarkAsSignificant();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(progress.IsSignificant, Is.True);
            Assert.That(progress.UpdatedAt, Is.Not.Null);
            Assert.That(progress.UpdatedAt!.Value, Is.GreaterThan(beforeUpdate));
        });
    }

    [Test]
    public void UpdatePercentage_ValidPercentage_UpdatesValue()
    {
        // Arrange
        var progress = new Progress
        {
            ProgressId = Guid.NewGuid(),
            CompletionPercentage = 25
        };
        var beforeUpdate = DateTime.UtcNow.AddSeconds(-1);

        // Act
        progress.UpdatePercentage(75);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(progress.CompletionPercentage, Is.EqualTo(75));
            Assert.That(progress.UpdatedAt, Is.Not.Null);
            Assert.That(progress.UpdatedAt!.Value, Is.GreaterThan(beforeUpdate));
        });
    }

    [Test]
    public void UpdatePercentage_ZeroPercentage_UpdatesValue()
    {
        // Arrange
        var progress = new Progress
        {
            ProgressId = Guid.NewGuid(),
            CompletionPercentage = 50
        };

        // Act
        progress.UpdatePercentage(0);

        // Assert
        Assert.That(progress.CompletionPercentage, Is.EqualTo(0));
    }

    [Test]
    public void UpdatePercentage_HundredPercentage_UpdatesValue()
    {
        // Arrange
        var progress = new Progress
        {
            ProgressId = Guid.NewGuid(),
            CompletionPercentage = 50
        };

        // Act
        progress.UpdatePercentage(100);

        // Assert
        Assert.That(progress.CompletionPercentage, Is.EqualTo(100));
    }

    [Test]
    public void UpdatePercentage_NegativePercentage_DoesNotUpdate()
    {
        // Arrange
        var progress = new Progress
        {
            ProgressId = Guid.NewGuid(),
            CompletionPercentage = 50
        };

        // Act
        progress.UpdatePercentage(-10);

        // Assert
        Assert.That(progress.CompletionPercentage, Is.EqualTo(50));
    }

    [Test]
    public void UpdatePercentage_OverHundredPercentage_DoesNotUpdate()
    {
        // Arrange
        var progress = new Progress
        {
            ProgressId = Guid.NewGuid(),
            CompletionPercentage = 50
        };

        // Act
        progress.UpdatePercentage(150);

        // Assert
        Assert.That(progress.CompletionPercentage, Is.EqualTo(50));
    }

    [Test]
    public void UpdatePercentage_InvalidValue_DoesNotUpdateTimestamp()
    {
        // Arrange
        var progress = new Progress
        {
            ProgressId = Guid.NewGuid(),
            CompletionPercentage = 50
        };

        // Act
        progress.UpdatePercentage(-10);

        // Assert
        Assert.That(progress.UpdatedAt, Is.Null);
    }

    [Test]
    public void EffortHours_CanBeSetToDecimal()
    {
        // Arrange & Act
        var progress = new Progress
        {
            EffortHours = 3.75m
        };

        // Assert
        Assert.That(progress.EffortHours, Is.EqualTo(3.75m));
    }

    [Test]
    public void EffortHours_CanBeNull()
    {
        // Arrange & Act
        var progress = new Progress
        {
            EffortHours = null
        };

        // Assert
        Assert.That(progress.EffortHours, Is.Null);
    }

    [Test]
    public void ProgressDate_CanBeSetToCustomDate()
    {
        // Arrange
        var customDate = new DateTime(2024, 1, 15, 10, 30, 0, DateTimeKind.Utc);

        // Act
        var progress = new Progress
        {
            ProgressDate = customDate
        };

        // Assert
        Assert.That(progress.ProgressDate, Is.EqualTo(customDate));
    }

    [Test]
    public void Goal_NavigationProperty_CanBeSet()
    {
        // Arrange
        var goal = new Goal { GoalId = Guid.NewGuid() };
        var progress = new Progress
        {
            ProgressId = Guid.NewGuid(),
            GoalId = goal.GoalId
        };

        // Act
        progress.Goal = goal;

        // Assert
        Assert.That(progress.Goal, Is.EqualTo(goal));
    }

    [Test]
    public void CompletionPercentage_CanBeDecimal()
    {
        // Arrange & Act
        var progress = new Progress
        {
            CompletionPercentage = 33.33
        };

        // Assert
        Assert.That(progress.CompletionPercentage, Is.EqualTo(33.33));
    }
}
