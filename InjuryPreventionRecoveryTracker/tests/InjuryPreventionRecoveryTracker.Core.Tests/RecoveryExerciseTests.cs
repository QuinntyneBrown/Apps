// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace InjuryPreventionRecoveryTracker.Core.Tests;

public class RecoveryExerciseTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesRecoveryExercise()
    {
        // Arrange
        var exerciseId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var injuryId = Guid.NewGuid();
        var name = "Ankle Rotations";
        var description = "Gentle rotation movements";
        var frequency = "3 times daily";
        var setsAndReps = "3 sets of 10";
        var durationMinutes = 5;
        var instructions = "Stop if pain occurs";

        // Act
        var exercise = new RecoveryExercise
        {
            RecoveryExerciseId = exerciseId,
            UserId = userId,
            InjuryId = injuryId,
            Name = name,
            Description = description,
            Frequency = frequency,
            SetsAndReps = setsAndReps,
            DurationMinutes = durationMinutes,
            Instructions = instructions
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(exercise.RecoveryExerciseId, Is.EqualTo(exerciseId));
            Assert.That(exercise.UserId, Is.EqualTo(userId));
            Assert.That(exercise.InjuryId, Is.EqualTo(injuryId));
            Assert.That(exercise.Name, Is.EqualTo(name));
            Assert.That(exercise.Description, Is.EqualTo(description));
            Assert.That(exercise.Frequency, Is.EqualTo(frequency));
            Assert.That(exercise.SetsAndReps, Is.EqualTo(setsAndReps));
            Assert.That(exercise.DurationMinutes, Is.EqualTo(durationMinutes));
            Assert.That(exercise.Instructions, Is.EqualTo(instructions));
            Assert.That(exercise.IsActive, Is.True);
            Assert.That(exercise.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void DefaultConstructor_SetsDefaultValues()
    {
        // Act
        var exercise = new RecoveryExercise();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(exercise.RecoveryExerciseId, Is.EqualTo(Guid.Empty));
            Assert.That(exercise.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(exercise.InjuryId, Is.EqualTo(Guid.Empty));
            Assert.That(exercise.Name, Is.EqualTo(string.Empty));
            Assert.That(exercise.Frequency, Is.EqualTo(string.Empty));
            Assert.That(exercise.IsActive, Is.True);
            Assert.That(exercise.LastCompleted, Is.Null);
        });
    }

    [Test]
    public void MarkCompleted_SetsLastCompletedToNow()
    {
        // Arrange
        var exercise = new RecoveryExercise { LastCompleted = null };

        // Act
        exercise.MarkCompleted();

        // Assert
        Assert.That(exercise.LastCompleted, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void MarkCompleted_UpdatesExistingLastCompleted()
    {
        // Arrange
        var oldDate = new DateTime(2025, 1, 1);
        var exercise = new RecoveryExercise { LastCompleted = oldDate };

        // Act
        exercise.MarkCompleted();

        // Assert
        Assert.That(exercise.LastCompleted, Is.GreaterThan(oldDate));
    }

    [Test]
    public void WasCompletedToday_CompletedToday_ReturnsTrue()
    {
        // Arrange
        var exercise = new RecoveryExercise();
        exercise.MarkCompleted();

        // Act
        var result = exercise.WasCompletedToday();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void WasCompletedToday_CompletedYesterday_ReturnsFalse()
    {
        // Arrange
        var exercise = new RecoveryExercise
        {
            LastCompleted = DateTime.UtcNow.AddDays(-1)
        };

        // Act
        var result = exercise.WasCompletedToday();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void WasCompletedToday_NeverCompleted_ReturnsFalse()
    {
        // Arrange
        var exercise = new RecoveryExercise { LastCompleted = null };

        // Act
        var result = exercise.WasCompletedToday();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsActive_DefaultValue_IsTrue()
    {
        // Act
        var exercise = new RecoveryExercise();

        // Assert
        Assert.That(exercise.IsActive, Is.True);
    }

    [Test]
    public void IsActive_CanBeSetToFalse()
    {
        // Arrange
        var exercise = new RecoveryExercise { IsActive = true };

        // Act
        exercise.IsActive = false;

        // Assert
        Assert.That(exercise.IsActive, Is.False);
    }

    [Test]
    public void DurationMinutes_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var exercise = new RecoveryExercise { DurationMinutes = null };

        // Assert
        Assert.That(exercise.DurationMinutes, Is.Null);
    }

    [Test]
    public void SetsAndReps_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var exercise = new RecoveryExercise { SetsAndReps = null };

        // Assert
        Assert.That(exercise.SetsAndReps, Is.Null);
    }

    [Test]
    public void Instructions_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var exercise = new RecoveryExercise { Instructions = null };

        // Assert
        Assert.That(exercise.Instructions, Is.Null);
    }

    [Test]
    public void Injury_NavigationProperty_CanBeSet()
    {
        // Arrange
        var injury = new Injury { InjuryId = Guid.NewGuid() };
        var exercise = new RecoveryExercise();

        // Act
        exercise.Injury = injury;

        // Assert
        Assert.That(exercise.Injury, Is.EqualTo(injury));
    }
}
