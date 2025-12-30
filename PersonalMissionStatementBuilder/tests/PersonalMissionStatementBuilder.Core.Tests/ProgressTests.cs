namespace PersonalMissionStatementBuilder.Core.Tests;

public class ProgressTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesProgress()
    {
        // Arrange
        var progressId = Guid.NewGuid();
        var goalId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var progressDate = DateTime.UtcNow;

        // Act
        var progress = new Progress
        {
            ProgressId = progressId,
            GoalId = goalId,
            UserId = userId,
            ProgressDate = progressDate,
            Notes = "Completed first module",
            CompletionPercentage = 25.0
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(progress.ProgressId, Is.EqualTo(progressId));
            Assert.That(progress.GoalId, Is.EqualTo(goalId));
            Assert.That(progress.UserId, Is.EqualTo(userId));
            Assert.That(progress.ProgressDate, Is.EqualTo(progressDate));
            Assert.That(progress.Notes, Is.EqualTo("Completed first module"));
            Assert.That(progress.CompletionPercentage, Is.EqualTo(25.0));
            Assert.That(progress.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(progress.UpdatedAt, Is.Null);
        });
    }

    [Test]
    public void UpdatePercentage_ValidPercentage_UpdatesPercentageAndTimestamp()
    {
        // Arrange
        var progress = new Progress
        {
            ProgressId = Guid.NewGuid(),
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            CompletionPercentage = 25.0
        };

        // Act
        progress.UpdatePercentage(50.0);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(progress.CompletionPercentage, Is.EqualTo(50.0));
            Assert.That(progress.UpdatedAt, Is.Not.Null);
        });
    }

    [Test]
    public void UpdatePercentage_ZeroPercent_UpdatesCorrectly()
    {
        // Arrange
        var progress = new Progress
        {
            ProgressId = Guid.NewGuid(),
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            CompletionPercentage = 50.0
        };

        // Act
        progress.UpdatePercentage(0.0);

        // Assert
        Assert.That(progress.CompletionPercentage, Is.EqualTo(0.0));
    }

    [Test]
    public void UpdatePercentage_HundredPercent_UpdatesCorrectly()
    {
        // Arrange
        var progress = new Progress
        {
            ProgressId = Guid.NewGuid(),
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            CompletionPercentage = 75.0
        };

        // Act
        progress.UpdatePercentage(100.0);

        // Assert
        Assert.That(progress.CompletionPercentage, Is.EqualTo(100.0));
    }

    [Test]
    public void UpdatePercentage_NegativePercentage_DoesNotUpdate()
    {
        // Arrange
        var progress = new Progress
        {
            ProgressId = Guid.NewGuid(),
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            CompletionPercentage = 50.0,
            UpdatedAt = null
        };

        // Act
        progress.UpdatePercentage(-10.0);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(progress.CompletionPercentage, Is.EqualTo(50.0));
            Assert.That(progress.UpdatedAt, Is.Null);
        });
    }

    [Test]
    public void UpdatePercentage_OverHundred_DoesNotUpdate()
    {
        // Arrange
        var progress = new Progress
        {
            ProgressId = Guid.NewGuid(),
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            CompletionPercentage = 50.0,
            UpdatedAt = null
        };

        // Act
        progress.UpdatePercentage(150.0);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(progress.CompletionPercentage, Is.EqualTo(50.0));
            Assert.That(progress.UpdatedAt, Is.Null);
        });
    }

    [Test]
    public void UpdatePercentage_EdgeCaseZero_UpdatesCorrectly()
    {
        // Arrange
        var progress = new Progress
        {
            ProgressId = Guid.NewGuid(),
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            CompletionPercentage = 50.0
        };

        // Act
        progress.UpdatePercentage(0);

        // Assert
        Assert.That(progress.CompletionPercentage, Is.EqualTo(0.0));
    }

    [Test]
    public void UpdatePercentage_EdgeCaseHundred_UpdatesCorrectly()
    {
        // Arrange
        var progress = new Progress
        {
            ProgressId = Guid.NewGuid(),
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            CompletionPercentage = 50.0
        };

        // Act
        progress.UpdatePercentage(100);

        // Assert
        Assert.That(progress.CompletionPercentage, Is.EqualTo(100.0));
    }

    [Test]
    public void Progress_DecimalPercentages_CanBeUsed()
    {
        // Arrange & Act
        var progress = new Progress
        {
            ProgressId = Guid.NewGuid(),
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            CompletionPercentage = 33.33
        };

        // Assert
        Assert.That(progress.CompletionPercentage, Is.EqualTo(33.33));
    }

    [Test]
    public void UpdatePercentage_DecimalValue_UpdatesCorrectly()
    {
        // Arrange
        var progress = new Progress
        {
            ProgressId = Guid.NewGuid(),
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            CompletionPercentage = 0.0
        };

        // Act
        progress.UpdatePercentage(66.67);

        // Assert
        Assert.That(progress.CompletionPercentage, Is.EqualTo(66.67));
    }
}
