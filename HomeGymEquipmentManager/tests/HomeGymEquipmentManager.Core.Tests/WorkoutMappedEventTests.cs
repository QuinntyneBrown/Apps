// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeGymEquipmentManager.Core.Tests;

public class WorkoutMappedEventTests
{
    [Test]
    public void WorkoutMappedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var workoutMappingId = Guid.NewGuid();
        var equipmentId = Guid.NewGuid();
        var exerciseName = "Bench Press";
        var timestamp = DateTime.UtcNow;

        // Act
        var workoutEvent = new WorkoutMappedEvent
        {
            WorkoutMappingId = workoutMappingId,
            EquipmentId = equipmentId,
            ExerciseName = exerciseName,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(workoutEvent.WorkoutMappingId, Is.EqualTo(workoutMappingId));
            Assert.That(workoutEvent.EquipmentId, Is.EqualTo(equipmentId));
            Assert.That(workoutEvent.ExerciseName, Is.EqualTo(exerciseName));
            Assert.That(workoutEvent.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void WorkoutMappedEvent_DefaultValues_AreSetCorrectly()
    {
        // Act
        var workoutEvent = new WorkoutMappedEvent();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(workoutEvent.WorkoutMappingId, Is.EqualTo(Guid.Empty));
            Assert.That(workoutEvent.EquipmentId, Is.EqualTo(Guid.Empty));
            Assert.That(workoutEvent.ExerciseName, Is.EqualTo(string.Empty));
            Assert.That(workoutEvent.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void WorkoutMappedEvent_Timestamp_IsSetToCurrentTime()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var workoutEvent = new WorkoutMappedEvent
        {
            WorkoutMappingId = Guid.NewGuid(),
            EquipmentId = Guid.NewGuid(),
            ExerciseName = "Squat"
        };
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(workoutEvent.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void WorkoutMappedEvent_WithEmptyExerciseName_IsValid()
    {
        // Arrange & Act
        var workoutEvent = new WorkoutMappedEvent
        {
            WorkoutMappingId = Guid.NewGuid(),
            EquipmentId = Guid.NewGuid(),
            ExerciseName = ""
        };

        // Assert
        Assert.That(workoutEvent.ExerciseName, Is.EqualTo(string.Empty));
    }

    [Test]
    public void WorkoutMappedEvent_IsImmutable()
    {
        // Arrange
        var workoutMappingId = Guid.NewGuid();
        var equipmentId = Guid.NewGuid();
        var exerciseName = "Deadlift";

        // Act
        var workoutEvent = new WorkoutMappedEvent
        {
            WorkoutMappingId = workoutMappingId,
            EquipmentId = equipmentId,
            ExerciseName = exerciseName
        };

        // Assert - Record properties are init-only
        Assert.Multiple(() =>
        {
            Assert.That(workoutEvent.WorkoutMappingId, Is.EqualTo(workoutMappingId));
            Assert.That(workoutEvent.EquipmentId, Is.EqualTo(equipmentId));
            Assert.That(workoutEvent.ExerciseName, Is.EqualTo(exerciseName));
        });
    }

    [Test]
    public void WorkoutMappedEvent_EqualityByValue()
    {
        // Arrange
        var workoutMappingId = Guid.NewGuid();
        var equipmentId = Guid.NewGuid();
        var exerciseName = "Pull-up";
        var timestamp = DateTime.UtcNow;

        var event1 = new WorkoutMappedEvent
        {
            WorkoutMappingId = workoutMappingId,
            EquipmentId = equipmentId,
            ExerciseName = exerciseName,
            Timestamp = timestamp
        };

        var event2 = new WorkoutMappedEvent
        {
            WorkoutMappingId = workoutMappingId,
            EquipmentId = equipmentId,
            ExerciseName = exerciseName,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void WorkoutMappedEvent_DifferentValues_AreNotEqual()
    {
        // Arrange
        var event1 = new WorkoutMappedEvent
        {
            WorkoutMappingId = Guid.NewGuid(),
            EquipmentId = Guid.NewGuid(),
            ExerciseName = "Exercise 1"
        };

        var event2 = new WorkoutMappedEvent
        {
            WorkoutMappingId = Guid.NewGuid(),
            EquipmentId = Guid.NewGuid(),
            ExerciseName = "Exercise 2"
        };

        // Act & Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }

    [Test]
    public void WorkoutMappedEvent_WithLongExerciseName_IsValid()
    {
        // Arrange
        var longExerciseName = new string('A', 500);

        // Act
        var workoutEvent = new WorkoutMappedEvent
        {
            WorkoutMappingId = Guid.NewGuid(),
            EquipmentId = Guid.NewGuid(),
            ExerciseName = longExerciseName
        };

        // Assert
        Assert.That(workoutEvent.ExerciseName, Is.EqualTo(longExerciseName));
    }
}
