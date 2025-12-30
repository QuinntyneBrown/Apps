// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace InjuryPreventionRecoveryTracker.Core.Tests;

public class InjuryTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesInjury()
    {
        // Arrange
        var injuryId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var injuryType = InjuryType.Sprain;
        var severity = InjurySeverity.Moderate;
        var bodyPart = "Ankle";
        var injuryDate = new DateTime(2025, 1, 15);
        var description = "Twisted ankle during run";
        var diagnosis = "Grade II ankle sprain";
        var expectedRecoveryDays = 21;
        var status = "Recovering";
        var progressPercentage = 40;
        var notes = "Ice and elevation";

        // Act
        var injury = new Injury
        {
            InjuryId = injuryId,
            UserId = userId,
            InjuryType = injuryType,
            Severity = severity,
            BodyPart = bodyPart,
            InjuryDate = injuryDate,
            Description = description,
            Diagnosis = diagnosis,
            ExpectedRecoveryDays = expectedRecoveryDays,
            Status = status,
            ProgressPercentage = progressPercentage,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(injury.InjuryId, Is.EqualTo(injuryId));
            Assert.That(injury.UserId, Is.EqualTo(userId));
            Assert.That(injury.InjuryType, Is.EqualTo(injuryType));
            Assert.That(injury.Severity, Is.EqualTo(severity));
            Assert.That(injury.BodyPart, Is.EqualTo(bodyPart));
            Assert.That(injury.InjuryDate, Is.EqualTo(injuryDate));
            Assert.That(injury.Description, Is.EqualTo(description));
            Assert.That(injury.Diagnosis, Is.EqualTo(diagnosis));
            Assert.That(injury.ExpectedRecoveryDays, Is.EqualTo(expectedRecoveryDays));
            Assert.That(injury.Status, Is.EqualTo(status));
            Assert.That(injury.ProgressPercentage, Is.EqualTo(progressPercentage));
            Assert.That(injury.Notes, Is.EqualTo(notes));
            Assert.That(injury.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void DefaultConstructor_SetsDefaultValues()
    {
        // Act
        var injury = new Injury();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(injury.InjuryId, Is.EqualTo(Guid.Empty));
            Assert.That(injury.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(injury.InjuryType, Is.EqualTo(InjuryType.Strain));
            Assert.That(injury.Severity, Is.EqualTo(InjurySeverity.Minor));
            Assert.That(injury.BodyPart, Is.EqualTo(string.Empty));
            Assert.That(injury.Status, Is.EqualTo("Active"));
            Assert.That(injury.ProgressPercentage, Is.EqualTo(0));
            Assert.That(injury.RecoveryExercises, Is.Not.Null);
            Assert.That(injury.Milestones, Is.Not.Null);
        });
    }

    [Test]
    public void IsHealed_StatusHealed_ReturnsTrue()
    {
        // Arrange
        var injury = new Injury { Status = "Healed", ProgressPercentage = 80 };

        // Act
        var result = injury.IsHealed();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsHealed_ProgressPercentage100_ReturnsTrue()
    {
        // Arrange
        var injury = new Injury { Status = "Recovering", ProgressPercentage = 100 };

        // Act
        var result = injury.IsHealed();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsHealed_StatusActive_ReturnsFalse()
    {
        // Arrange
        var injury = new Injury { Status = "Active", ProgressPercentage = 0 };

        // Act
        var result = injury.IsHealed();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void UpdateProgress_ValidPercentage_UpdatesProgress()
    {
        // Arrange
        var injury = new Injury { ProgressPercentage = 0, Status = "Active" };

        // Act
        injury.UpdateProgress(50);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(injury.ProgressPercentage, Is.EqualTo(50));
            Assert.That(injury.Status, Is.EqualTo("Active"));
        });
    }

    [Test]
    public void UpdateProgress_100Percent_SetsStatusToHealed()
    {
        // Arrange
        var injury = new Injury { ProgressPercentage = 90, Status = "Recovering" };

        // Act
        injury.UpdateProgress(100);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(injury.ProgressPercentage, Is.EqualTo(100));
            Assert.That(injury.Status, Is.EqualTo("Healed"));
        });
    }

    [Test]
    public void UpdateProgress_Over100_ClampsTo100()
    {
        // Arrange
        var injury = new Injury { ProgressPercentage = 50 };

        // Act
        injury.UpdateProgress(150);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(injury.ProgressPercentage, Is.EqualTo(100));
            Assert.That(injury.Status, Is.EqualTo("Healed"));
        });
    }

    [Test]
    public void UpdateProgress_Negative_ClampsToZero()
    {
        // Arrange
        var injury = new Injury { ProgressPercentage = 50 };

        // Act
        injury.UpdateProgress(-10);

        // Assert
        Assert.That(injury.ProgressPercentage, Is.EqualTo(0));
    }

    [Test]
    public void InjuryType_AllTypes_CanBeAssigned()
    {
        // Arrange
        var injury = new Injury();

        // Act & Assert
        Assert.DoesNotThrow(() => injury.InjuryType = InjuryType.Strain);
        Assert.DoesNotThrow(() => injury.InjuryType = InjuryType.Sprain);
        Assert.DoesNotThrow(() => injury.InjuryType = InjuryType.Fracture);
        Assert.DoesNotThrow(() => injury.InjuryType = InjuryType.Tendonitis);
        Assert.DoesNotThrow(() => injury.InjuryType = InjuryType.CartilageDamage);
        Assert.DoesNotThrow(() => injury.InjuryType = InjuryType.Overuse);
        Assert.DoesNotThrow(() => injury.InjuryType = InjuryType.Other);
    }

    [Test]
    public void Severity_AllLevels_CanBeAssigned()
    {
        // Arrange
        var injury = new Injury();

        // Act & Assert
        Assert.DoesNotThrow(() => injury.Severity = InjurySeverity.Minor);
        Assert.DoesNotThrow(() => injury.Severity = InjurySeverity.Moderate);
        Assert.DoesNotThrow(() => injury.Severity = InjurySeverity.Severe);
        Assert.DoesNotThrow(() => injury.Severity = InjurySeverity.Critical);
    }

    [Test]
    public void RecoveryExercises_DefaultValue_IsEmptyList()
    {
        // Act
        var injury = new Injury();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(injury.RecoveryExercises, Is.Not.Null);
            Assert.That(injury.RecoveryExercises, Is.Empty);
        });
    }

    [Test]
    public void Milestones_DefaultValue_IsEmptyList()
    {
        // Act
        var injury = new Injury();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(injury.Milestones, Is.Not.Null);
            Assert.That(injury.Milestones, Is.Empty);
        });
    }

    [Test]
    public void ExpectedRecoveryDays_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var injury = new Injury { ExpectedRecoveryDays = null };

        // Assert
        Assert.That(injury.ExpectedRecoveryDays, Is.Null);
    }
}
