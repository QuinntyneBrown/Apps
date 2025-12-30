// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeGymEquipmentManager.Core.Tests;

public class WorkoutMappingTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesWorkoutMapping()
    {
        // Arrange
        var workoutMappingId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var equipmentId = Guid.NewGuid();
        var exerciseName = "Bench Press";
        var muscleGroup = "Chest";
        var instructions = "Keep back flat on bench";
        var isFavorite = true;

        // Act
        var workoutMapping = new WorkoutMapping
        {
            WorkoutMappingId = workoutMappingId,
            UserId = userId,
            EquipmentId = equipmentId,
            ExerciseName = exerciseName,
            MuscleGroup = muscleGroup,
            Instructions = instructions,
            IsFavorite = isFavorite
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(workoutMapping.WorkoutMappingId, Is.EqualTo(workoutMappingId));
            Assert.That(workoutMapping.UserId, Is.EqualTo(userId));
            Assert.That(workoutMapping.EquipmentId, Is.EqualTo(equipmentId));
            Assert.That(workoutMapping.ExerciseName, Is.EqualTo(exerciseName));
            Assert.That(workoutMapping.MuscleGroup, Is.EqualTo(muscleGroup));
            Assert.That(workoutMapping.Instructions, Is.EqualTo(instructions));
            Assert.That(workoutMapping.IsFavorite, Is.True);
        });
    }

    [Test]
    public void WorkoutMapping_DefaultValues_AreSetCorrectly()
    {
        // Act
        var workoutMapping = new WorkoutMapping();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(workoutMapping.ExerciseName, Is.EqualTo(string.Empty));
            Assert.That(workoutMapping.MuscleGroup, Is.Null);
            Assert.That(workoutMapping.Instructions, Is.Null);
            Assert.That(workoutMapping.IsFavorite, Is.False);
            Assert.That(workoutMapping.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void ToggleFavorite_WhenFalse_BecomesTrue()
    {
        // Arrange
        var workoutMapping = new WorkoutMapping
        {
            WorkoutMappingId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            EquipmentId = Guid.NewGuid(),
            ExerciseName = "Squat",
            IsFavorite = false
        };

        // Act
        workoutMapping.ToggleFavorite();

        // Assert
        Assert.That(workoutMapping.IsFavorite, Is.True);
    }

    [Test]
    public void ToggleFavorite_WhenTrue_BecomesFalse()
    {
        // Arrange
        var workoutMapping = new WorkoutMapping
        {
            WorkoutMappingId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            EquipmentId = Guid.NewGuid(),
            ExerciseName = "Deadlift",
            IsFavorite = true
        };

        // Act
        workoutMapping.ToggleFavorite();

        // Assert
        Assert.That(workoutMapping.IsFavorite, Is.False);
    }

    [Test]
    public void ToggleFavorite_MultipleCalls_TogglesCorrectly()
    {
        // Arrange
        var workoutMapping = new WorkoutMapping
        {
            WorkoutMappingId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            EquipmentId = Guid.NewGuid(),
            ExerciseName = "Pull-up",
            IsFavorite = false
        };

        // Act & Assert
        Assert.That(workoutMapping.IsFavorite, Is.False);

        workoutMapping.ToggleFavorite();
        Assert.That(workoutMapping.IsFavorite, Is.True);

        workoutMapping.ToggleFavorite();
        Assert.That(workoutMapping.IsFavorite, Is.False);

        workoutMapping.ToggleFavorite();
        Assert.That(workoutMapping.IsFavorite, Is.True);
    }

    [Test]
    public void WorkoutMapping_WithoutMuscleGroup_IsValid()
    {
        // Arrange & Act
        var workoutMapping = new WorkoutMapping
        {
            WorkoutMappingId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            EquipmentId = Guid.NewGuid(),
            ExerciseName = "Full Body Workout"
        };

        // Assert
        Assert.That(workoutMapping.MuscleGroup, Is.Null);
    }

    [Test]
    public void WorkoutMapping_WithoutInstructions_IsValid()
    {
        // Arrange & Act
        var workoutMapping = new WorkoutMapping
        {
            WorkoutMappingId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            EquipmentId = Guid.NewGuid(),
            ExerciseName = "Simple Exercise"
        };

        // Assert
        Assert.That(workoutMapping.Instructions, Is.Null);
    }

    [Test]
    public void WorkoutMapping_IsFavorite_DefaultsToFalse()
    {
        // Arrange & Act
        var workoutMapping = new WorkoutMapping
        {
            WorkoutMappingId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            EquipmentId = Guid.NewGuid(),
            ExerciseName = "New Exercise"
        };

        // Assert
        Assert.That(workoutMapping.IsFavorite, Is.False);
    }

    [Test]
    public void WorkoutMapping_CreatedAt_IsSetToCurrentTime()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var workoutMapping = new WorkoutMapping
        {
            WorkoutMappingId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            EquipmentId = Guid.NewGuid(),
            ExerciseName = "Test Exercise"
        };
        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.That(workoutMapping.CreatedAt, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
    }

    [Test]
    public void WorkoutMapping_WithLongInstructions_IsValid()
    {
        // Arrange
        var longInstructions = new string('A', 1000);

        // Act
        var workoutMapping = new WorkoutMapping
        {
            WorkoutMappingId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            EquipmentId = Guid.NewGuid(),
            ExerciseName = "Complex Exercise",
            Instructions = longInstructions
        };

        // Assert
        Assert.That(workoutMapping.Instructions, Is.EqualTo(longInstructions));
    }

    [Test]
    public void WorkoutMapping_AllProperties_CanBeSet()
    {
        // Arrange
        var workoutMappingId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var equipmentId = Guid.NewGuid();
        var exerciseName = "Leg Press";
        var muscleGroup = "Legs";
        var instructions = "Keep knees aligned with toes";
        var isFavorite = true;
        var createdAt = DateTime.UtcNow.AddDays(-5);

        // Act
        var workoutMapping = new WorkoutMapping
        {
            WorkoutMappingId = workoutMappingId,
            UserId = userId,
            EquipmentId = equipmentId,
            ExerciseName = exerciseName,
            MuscleGroup = muscleGroup,
            Instructions = instructions,
            IsFavorite = isFavorite,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(workoutMapping.WorkoutMappingId, Is.EqualTo(workoutMappingId));
            Assert.That(workoutMapping.UserId, Is.EqualTo(userId));
            Assert.That(workoutMapping.EquipmentId, Is.EqualTo(equipmentId));
            Assert.That(workoutMapping.ExerciseName, Is.EqualTo(exerciseName));
            Assert.That(workoutMapping.MuscleGroup, Is.EqualTo(muscleGroup));
            Assert.That(workoutMapping.Instructions, Is.EqualTo(instructions));
            Assert.That(workoutMapping.IsFavorite, Is.EqualTo(isFavorite));
            Assert.That(workoutMapping.CreatedAt, Is.EqualTo(createdAt));
        });
    }
}
