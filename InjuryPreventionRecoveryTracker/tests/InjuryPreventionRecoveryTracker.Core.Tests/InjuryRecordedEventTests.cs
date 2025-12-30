// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace InjuryPreventionRecoveryTracker.Core.Tests;

public class InjuryRecordedEventTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesInjuryRecordedEvent()
    {
        // Arrange
        var injuryId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var injuryType = InjuryType.Sprain;
        var severity = InjurySeverity.Moderate;
        var bodyPart = "Ankle";
        var injuryDate = new DateTime(2025, 1, 15);
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new InjuryRecordedEvent
        {
            InjuryId = injuryId,
            UserId = userId,
            InjuryType = injuryType,
            Severity = severity,
            BodyPart = bodyPart,
            InjuryDate = injuryDate,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.InjuryId, Is.EqualTo(injuryId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.InjuryType, Is.EqualTo(injuryType));
            Assert.That(evt.Severity, Is.EqualTo(severity));
            Assert.That(evt.BodyPart, Is.EqualTo(bodyPart));
            Assert.That(evt.InjuryDate, Is.EqualTo(injuryDate));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void DefaultConstructor_SetsDefaultTimestamp()
    {
        // Act
        var evt = new InjuryRecordedEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.InjuryId, Is.EqualTo(Guid.Empty));
            Assert.That(evt.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(evt.BodyPart, Is.EqualTo(string.Empty));
            Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Record_Equality_WorksCorrectly()
    {
        // Arrange
        var injuryId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = new DateTime(2025, 1, 1);

        var evt1 = new InjuryRecordedEvent
        {
            InjuryId = injuryId,
            UserId = userId,
            InjuryType = InjuryType.Strain,
            Severity = InjurySeverity.Minor,
            BodyPart = "Knee",
            InjuryDate = new DateTime(2025, 1, 1),
            Timestamp = timestamp
        };

        var evt2 = new InjuryRecordedEvent
        {
            InjuryId = injuryId,
            UserId = userId,
            InjuryType = InjuryType.Strain,
            Severity = InjurySeverity.Minor,
            BodyPart = "Knee",
            InjuryDate = new DateTime(2025, 1, 1),
            Timestamp = timestamp
        };

        // Assert
        Assert.That(evt1, Is.EqualTo(evt2));
    }

    [Test]
    public void With_Expression_CreatesNewInstance()
    {
        // Arrange
        var original = new InjuryRecordedEvent
        {
            InjuryId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            InjuryType = InjuryType.Strain,
            Severity = InjurySeverity.Minor,
            BodyPart = "Wrist",
            InjuryDate = DateTime.UtcNow,
            Timestamp = DateTime.UtcNow
        };

        // Act
        var modified = original with { Severity = InjurySeverity.Severe };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(modified.InjuryId, Is.EqualTo(original.InjuryId));
            Assert.That(modified.Severity, Is.EqualTo(InjurySeverity.Severe));
            Assert.That(modified, Is.Not.SameAs(original));
        });
    }

    [Test]
    public void InjuryType_AllTypes_CanBeSet()
    {
        Assert.DoesNotThrow(() => new InjuryRecordedEvent { InjuryType = InjuryType.Strain });
        Assert.DoesNotThrow(() => new InjuryRecordedEvent { InjuryType = InjuryType.Sprain });
        Assert.DoesNotThrow(() => new InjuryRecordedEvent { InjuryType = InjuryType.Fracture });
        Assert.DoesNotThrow(() => new InjuryRecordedEvent { InjuryType = InjuryType.Tendonitis });
    }

    [Test]
    public void Severity_AllLevels_CanBeSet()
    {
        Assert.DoesNotThrow(() => new InjuryRecordedEvent { Severity = InjurySeverity.Minor });
        Assert.DoesNotThrow(() => new InjuryRecordedEvent { Severity = InjurySeverity.Moderate });
        Assert.DoesNotThrow(() => new InjuryRecordedEvent { Severity = InjurySeverity.Severe });
        Assert.DoesNotThrow(() => new InjuryRecordedEvent { Severity = InjurySeverity.Critical });
    }
}
