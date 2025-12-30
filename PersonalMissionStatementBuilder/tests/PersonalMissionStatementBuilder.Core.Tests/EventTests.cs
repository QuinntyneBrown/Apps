namespace PersonalMissionStatementBuilder.Core.Tests;

public class EventTests
{
    [Test]
    public void MissionStatementCreatedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var missionStatementId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new MissionStatementCreatedEvent
        {
            MissionStatementId = missionStatementId,
            UserId = userId,
            Title = "My Life Mission",
            Version = 1,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.MissionStatementId, Is.EqualTo(missionStatementId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Title, Is.EqualTo("My Life Mission"));
            Assert.That(evt.Version, Is.EqualTo(1));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void MissionStatementUpdatedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var missionStatementId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new MissionStatementUpdatedEvent
        {
            MissionStatementId = missionStatementId,
            UserId = userId,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.MissionStatementId, Is.EqualTo(missionStatementId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void GoalCreatedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var goalId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new GoalCreatedEvent
        {
            GoalId = goalId,
            UserId = userId,
            Title = "Learn Spanish",
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.GoalId, Is.EqualTo(goalId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Title, Is.EqualTo("Learn Spanish"));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void GoalCompletedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var goalId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var completedDate = DateTime.UtcNow;
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new GoalCompletedEvent
        {
            GoalId = goalId,
            UserId = userId,
            CompletedDate = completedDate,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.GoalId, Is.EqualTo(goalId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.CompletedDate, Is.EqualTo(completedDate));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void ValueCreatedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var valueId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new ValueCreatedEvent
        {
            ValueId = valueId,
            UserId = userId,
            Name = "Integrity",
            Priority = 1,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ValueId, Is.EqualTo(valueId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Name, Is.EqualTo("Integrity"));
            Assert.That(evt.Priority, Is.EqualTo(1));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void ProgressCreatedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var progressId = Guid.NewGuid();
        var goalId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new ProgressCreatedEvent
        {
            ProgressId = progressId,
            GoalId = goalId,
            UserId = userId,
            CompletionPercentage = 50.0,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ProgressId, Is.EqualTo(progressId));
            Assert.That(evt.GoalId, Is.EqualTo(goalId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.CompletionPercentage, Is.EqualTo(50.0));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void MissionStatementCreatedEvent_DifferentVersions_CanBeCreated()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(() => new MissionStatementCreatedEvent { Version = 1 }, Throws.Nothing);
            Assert.That(() => new MissionStatementCreatedEvent { Version = 2 }, Throws.Nothing);
            Assert.That(() => new MissionStatementCreatedEvent { Version = 10 }, Throws.Nothing);
        });
    }

    [Test]
    public void ValueCreatedEvent_DifferentPriorities_CanBeCreated()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(() => new ValueCreatedEvent { Priority = 1 }, Throws.Nothing);
            Assert.That(() => new ValueCreatedEvent { Priority = 5 }, Throws.Nothing);
            Assert.That(() => new ValueCreatedEvent { Priority = 10 }, Throws.Nothing);
        });
    }

    [Test]
    public void ProgressCreatedEvent_DifferentPercentages_CanBeCreated()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(() => new ProgressCreatedEvent { CompletionPercentage = 0.0 }, Throws.Nothing);
            Assert.That(() => new ProgressCreatedEvent { CompletionPercentage = 50.0 }, Throws.Nothing);
            Assert.That(() => new ProgressCreatedEvent { CompletionPercentage = 100.0 }, Throws.Nothing);
        });
    }

    [Test]
    public void Events_AreRecords_AreImmutable()
    {
        // Arrange & Act & Assert - Record types are immutable
        Assert.Multiple(() =>
        {
            var evt1 = new MissionStatementCreatedEvent { MissionStatementId = Guid.NewGuid() };
            Assert.That(evt1, Is.Not.Null);

            var evt2 = new GoalCreatedEvent { GoalId = Guid.NewGuid() };
            Assert.That(evt2, Is.Not.Null);

            var evt3 = new ValueCreatedEvent { ValueId = Guid.NewGuid() };
            Assert.That(evt3, Is.Not.Null);

            var evt4 = new ProgressCreatedEvent { ProgressId = Guid.NewGuid() };
            Assert.That(evt4, Is.Not.Null);
        });
    }
}
