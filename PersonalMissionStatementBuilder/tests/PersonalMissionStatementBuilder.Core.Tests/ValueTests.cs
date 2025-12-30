namespace PersonalMissionStatementBuilder.Core.Tests;

public class ValueTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesValue()
    {
        // Arrange
        var valueId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var missionStatementId = Guid.NewGuid();

        // Act
        var value = new Value
        {
            ValueId = valueId,
            UserId = userId,
            MissionStatementId = missionStatementId,
            Name = "Integrity",
            Description = "Always act with honesty and strong moral principles",
            Priority = 1
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(value.ValueId, Is.EqualTo(valueId));
            Assert.That(value.UserId, Is.EqualTo(userId));
            Assert.That(value.MissionStatementId, Is.EqualTo(missionStatementId));
            Assert.That(value.Name, Is.EqualTo("Integrity"));
            Assert.That(value.Description, Is.EqualTo("Always act with honesty and strong moral principles"));
            Assert.That(value.Priority, Is.EqualTo(1));
            Assert.That(value.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(value.UpdatedAt, Is.Null);
        });
    }

    [Test]
    public void UpdatePriority_ValidPriority_UpdatesPriorityAndTimestamp()
    {
        // Arrange
        var value = new Value
        {
            ValueId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Excellence",
            Priority = 1
        };

        // Act
        value.UpdatePriority(2);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(value.Priority, Is.EqualTo(2));
            Assert.That(value.UpdatedAt, Is.Not.Null);
        });
    }

    [Test]
    public void UpdatePriority_ToHigherPriority_Updates()
    {
        // Arrange
        var value = new Value
        {
            ValueId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Excellence",
            Priority = 5
        };

        // Act
        value.UpdatePriority(1);

        // Assert
        Assert.That(value.Priority, Is.EqualTo(1));
    }

    [Test]
    public void UpdatePriority_ToLowerPriority_Updates()
    {
        // Arrange
        var value = new Value
        {
            ValueId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Excellence",
            Priority = 1
        };

        // Act
        value.UpdatePriority(10);

        // Assert
        Assert.That(value.Priority, Is.EqualTo(10));
    }

    [Test]
    public void UpdatePriority_SamePriority_UpdatesTimestamp()
    {
        // Arrange
        var value = new Value
        {
            ValueId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Excellence",
            Priority = 1,
            UpdatedAt = null
        };

        // Act
        value.UpdatePriority(1);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(value.Priority, Is.EqualTo(1));
            Assert.That(value.UpdatedAt, Is.Not.Null);
        });
    }

    [Test]
    public void Value_DifferentPriorities_CanBeAssigned()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            var value1 = new Value { Priority = 1 };
            Assert.That(value1.Priority, Is.EqualTo(1));

            var value2 = new Value { Priority = 5 };
            Assert.That(value2.Priority, Is.EqualTo(5));

            var value3 = new Value { Priority = 10 };
            Assert.That(value3.Priority, Is.EqualTo(10));
        });
    }

    [Test]
    public void Value_CommonValues_CanBeCreated()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(() => new Value { Name = "Integrity" }, Throws.Nothing);
            Assert.That(() => new Value { Name = "Excellence" }, Throws.Nothing);
            Assert.That(() => new Value { Name = "Compassion" }, Throws.Nothing);
            Assert.That(() => new Value { Name = "Innovation" }, Throws.Nothing);
            Assert.That(() => new Value { Name = "Courage" }, Throws.Nothing);
            Assert.That(() => new Value { Name = "Respect" }, Throws.Nothing);
        });
    }

    [Test]
    public void Value_WithoutDescription_CanBeCreated()
    {
        // Arrange & Act
        var value = new Value
        {
            ValueId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Honesty",
            Description = null
        };

        // Assert
        Assert.That(value.Description, Is.Null);
    }

    [Test]
    public void Value_WithoutMissionStatement_CanBeCreated()
    {
        // Arrange & Act
        var value = new Value
        {
            ValueId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Integrity",
            MissionStatementId = null
        };

        // Assert
        Assert.That(value.MissionStatementId, Is.Null);
    }
}
