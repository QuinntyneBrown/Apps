// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace InjuryPreventionRecoveryTracker.Core.Tests;

public class ExerciseCompletedEventTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesExerciseCompletedEvent()
    {
        // Arrange
        var exerciseId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var injuryId = Guid.NewGuid();
        var name = "Stretching";
        var completedAt = new DateTime(2025, 1, 15, 10, 0, 0);
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new ExerciseCompletedEvent
        {
            RecoveryExerciseId = exerciseId,
            UserId = userId,
            InjuryId = injuryId,
            Name = name,
            CompletedAt = completedAt,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.RecoveryExerciseId, Is.EqualTo(exerciseId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.InjuryId, Is.EqualTo(injuryId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.CompletedAt, Is.EqualTo(completedAt));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void DefaultConstructor_SetsDefaultTimestamp()
    {
        // Act
        var evt = new ExerciseCompletedEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.RecoveryExerciseId, Is.EqualTo(Guid.Empty));
            Assert.That(evt.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(evt.InjuryId, Is.EqualTo(Guid.Empty));
            Assert.That(evt.Name, Is.EqualTo(string.Empty));
            Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Record_Equality_WorksCorrectly()
    {
        // Arrange
        var exerciseId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var injuryId = Guid.NewGuid();
        var completedAt = new DateTime(2025, 1, 1, 10, 0, 0);
        var timestamp = new DateTime(2025, 1, 1, 10, 5, 0);

        var evt1 = new ExerciseCompletedEvent
        {
            RecoveryExerciseId = exerciseId,
            UserId = userId,
            InjuryId = injuryId,
            Name = "Test Exercise",
            CompletedAt = completedAt,
            Timestamp = timestamp
        };

        var evt2 = new ExerciseCompletedEvent
        {
            RecoveryExerciseId = exerciseId,
            UserId = userId,
            InjuryId = injuryId,
            Name = "Test Exercise",
            CompletedAt = completedAt,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(evt1, Is.EqualTo(evt2));
    }

    [Test]
    public void With_Expression_CreatesNewInstance()
    {
        // Arrange
        var original = new ExerciseCompletedEvent
        {
            RecoveryExerciseId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            InjuryId = Guid.NewGuid(),
            Name = "Original Name",
            CompletedAt = DateTime.UtcNow,
            Timestamp = DateTime.UtcNow
        };

        // Act
        var modified = original with { Name = "Modified Name" };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(modified.RecoveryExerciseId, Is.EqualTo(original.RecoveryExerciseId));
            Assert.That(modified.Name, Is.EqualTo("Modified Name"));
            Assert.That(modified, Is.Not.SameAs(original));
        });
    }
}
