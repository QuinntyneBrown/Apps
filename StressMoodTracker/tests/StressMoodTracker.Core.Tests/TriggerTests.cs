// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace StressMoodTracker.Core.Tests;

public class TriggerTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesTrigger()
    {
        // Arrange
        var triggerId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Work Deadlines";
        var description = "Tight deadlines at work cause anxiety";
        var triggerType = "Work";
        var impactLevel = 4;

        // Act
        var trigger = new Trigger
        {
            TriggerId = triggerId,
            UserId = userId,
            Name = name,
            Description = description,
            TriggerType = triggerType,
            ImpactLevel = impactLevel
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(trigger.TriggerId, Is.EqualTo(triggerId));
            Assert.That(trigger.UserId, Is.EqualTo(userId));
            Assert.That(trigger.Name, Is.EqualTo(name));
            Assert.That(trigger.Description, Is.EqualTo(description));
            Assert.That(trigger.TriggerType, Is.EqualTo(triggerType));
            Assert.That(trigger.ImpactLevel, Is.EqualTo(impactLevel));
        });
    }

    [Test]
    public void DefaultValues_NewTrigger_HasExpectedDefaults()
    {
        // Act
        var trigger = new Trigger();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(trigger.Name, Is.EqualTo(string.Empty));
            Assert.That(trigger.TriggerType, Is.EqualTo(string.Empty));
            Assert.That(trigger.ImpactLevel, Is.EqualTo(0));
            Assert.That(trigger.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void IsHighImpact_ImpactLevel4_ReturnsTrue()
    {
        // Arrange
        var trigger = new Trigger
        {
            ImpactLevel = 4
        };

        // Act
        var result = trigger.IsHighImpact();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsHighImpact_ImpactLevel5_ReturnsTrue()
    {
        // Arrange
        var trigger = new Trigger
        {
            ImpactLevel = 5
        };

        // Act
        var result = trigger.IsHighImpact();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsHighImpact_ImpactLevel3_ReturnsFalse()
    {
        // Arrange
        var trigger = new Trigger
        {
            ImpactLevel = 3
        };

        // Act
        var result = trigger.IsHighImpact();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsHighImpact_ImpactLevel1_ReturnsFalse()
    {
        // Arrange
        var trigger = new Trigger
        {
            ImpactLevel = 1
        };

        // Act
        var result = trigger.IsHighImpact();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsHighImpact_ImpactLevel0_ReturnsFalse()
    {
        // Arrange
        var trigger = new Trigger
        {
            ImpactLevel = 0
        };

        // Act
        var result = trigger.IsHighImpact();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void Description_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var trigger = new Trigger
        {
            Description = null
        };

        // Assert
        Assert.That(trigger.Description, Is.Null);
    }

    [Test]
    public void ImpactLevel_ValidRange_CanBeStored()
    {
        // Arrange & Act
        var trigger1 = new Trigger { ImpactLevel = 1 };
        var trigger2 = new Trigger { ImpactLevel = 3 };
        var trigger3 = new Trigger { ImpactLevel = 5 };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(trigger1.ImpactLevel, Is.EqualTo(1));
            Assert.That(trigger2.ImpactLevel, Is.EqualTo(3));
            Assert.That(trigger3.ImpactLevel, Is.EqualTo(5));
        });
    }

    [Test]
    public void TriggerType_DifferentTypes_CanBeStored()
    {
        // Arrange & Act
        var trigger1 = new Trigger { TriggerType = "Work" };
        var trigger2 = new Trigger { TriggerType = "Social" };
        var trigger3 = new Trigger { TriggerType = "Financial" };
        var trigger4 = new Trigger { TriggerType = "Health" };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(trigger1.TriggerType, Is.EqualTo("Work"));
            Assert.That(trigger2.TriggerType, Is.EqualTo("Social"));
            Assert.That(trigger3.TriggerType, Is.EqualTo("Financial"));
            Assert.That(trigger4.TriggerType, Is.EqualTo("Health"));
        });
    }

    [Test]
    public void Name_CanStoreValue()
    {
        // Arrange
        var name = "Public Speaking";

        // Act
        var trigger = new Trigger
        {
            Name = name
        };

        // Assert
        Assert.That(trigger.Name, Is.EqualTo(name));
    }

    [Test]
    public void Description_CanStoreDetailedText()
    {
        // Arrange
        var description = "Speaking in front of large audiences causes significant anxiety and stress";

        // Act
        var trigger = new Trigger
        {
            Description = description
        };

        // Assert
        Assert.That(trigger.Description, Is.EqualTo(description));
    }

    [Test]
    public void UserId_CanBeAssociated()
    {
        // Arrange
        var userId = Guid.NewGuid();

        // Act
        var trigger = new Trigger
        {
            UserId = userId
        };

        // Assert
        Assert.That(trigger.UserId, Is.EqualTo(userId));
    }

    [Test]
    public void CreatedAt_AutomaticallySet_OnCreation()
    {
        // Arrange & Act
        var trigger = new Trigger();

        // Assert
        Assert.That(trigger.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void ImpactLevel_BoundaryValue_AtThreshold()
    {
        // Arrange
        var trigger = new Trigger
        {
            ImpactLevel = 4
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(trigger.ImpactLevel, Is.EqualTo(4));
            Assert.That(trigger.IsHighImpact(), Is.True);
        });
    }

    [Test]
    public void ImpactLevel_JustBelowThreshold_NotHighImpact()
    {
        // Arrange
        var trigger = new Trigger
        {
            ImpactLevel = 3
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(trigger.ImpactLevel, Is.EqualTo(3));
            Assert.That(trigger.IsHighImpact(), Is.False);
        });
    }
}
