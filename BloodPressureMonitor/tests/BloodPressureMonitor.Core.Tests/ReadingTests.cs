// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BloodPressureMonitor.Core.Tests;

public class ReadingTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesReading()
    {
        // Arrange
        var readingId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var systolic = 120;
        var diastolic = 80;
        var pulse = 72;
        var category = BloodPressureCategory.Normal;
        var measuredAt = DateTime.UtcNow.AddHours(-1);
        var position = "Sitting";
        var arm = "Left";
        var notes = "Morning reading";

        // Act
        var reading = new Reading
        {
            ReadingId = readingId,
            UserId = userId,
            Systolic = systolic,
            Diastolic = diastolic,
            Pulse = pulse,
            Category = category,
            MeasuredAt = measuredAt,
            Position = position,
            Arm = arm,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(reading.ReadingId, Is.EqualTo(readingId));
            Assert.That(reading.UserId, Is.EqualTo(userId));
            Assert.That(reading.Systolic, Is.EqualTo(systolic));
            Assert.That(reading.Diastolic, Is.EqualTo(diastolic));
            Assert.That(reading.Pulse, Is.EqualTo(pulse));
            Assert.That(reading.Category, Is.EqualTo(category));
            Assert.That(reading.MeasuredAt, Is.EqualTo(measuredAt));
            Assert.That(reading.Position, Is.EqualTo(position));
            Assert.That(reading.Arm, Is.EqualTo(arm));
            Assert.That(reading.Notes, Is.EqualTo(notes));
            Assert.That(reading.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Constructor_DefaultValues_SetsCorrectDefaults()
    {
        // Act
        var reading = new Reading();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(reading.Pulse, Is.Null);
            Assert.That(reading.Position, Is.Null);
            Assert.That(reading.Arm, Is.Null);
            Assert.That(reading.Notes, Is.Null);
            Assert.That(reading.MeasuredAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
            Assert.That(reading.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void DetermineCategory_NormalBloodPressure_ReturnsNormal()
    {
        // Arrange
        var reading = new Reading { Systolic = 110, Diastolic = 70 };

        // Act
        var category = reading.DetermineCategory();

        // Assert
        Assert.That(category, Is.EqualTo(BloodPressureCategory.Normal));
    }

    [Test]
    public void DetermineCategory_ElevatedBloodPressure_ReturnsElevated()
    {
        // Arrange
        var reading = new Reading { Systolic = 125, Diastolic = 75 };

        // Act
        var category = reading.DetermineCategory();

        // Assert
        Assert.That(category, Is.EqualTo(BloodPressureCategory.Elevated));
    }

    [Test]
    public void DetermineCategory_HypertensionStage1_ReturnsHypertensionStage1()
    {
        // Arrange
        var reading = new Reading { Systolic = 135, Diastolic = 85 };

        // Act
        var category = reading.DetermineCategory();

        // Assert
        Assert.That(category, Is.EqualTo(BloodPressureCategory.HypertensionStage1));
    }

    [Test]
    public void DetermineCategory_HypertensionStage2_ReturnsHypertensionStage2()
    {
        // Arrange
        var reading = new Reading { Systolic = 145, Diastolic = 95 };

        // Act
        var category = reading.DetermineCategory();

        // Assert
        Assert.That(category, Is.EqualTo(BloodPressureCategory.HypertensionStage2));
    }

    [Test]
    public void DetermineCategory_HypertensiveCrisis_ReturnsHypertensiveCrisis()
    {
        // Arrange
        var reading = new Reading { Systolic = 185, Diastolic = 125 };

        // Act
        var category = reading.DetermineCategory();

        // Assert
        Assert.That(category, Is.EqualTo(BloodPressureCategory.HypertensiveCrisis));
    }

    [Test]
    public void DetermineCategory_HighSystolicOnly_ReturnsCorrectCategory()
    {
        // Arrange
        var reading = new Reading { Systolic = 190, Diastolic = 75 };

        // Act
        var category = reading.DetermineCategory();

        // Assert
        Assert.That(category, Is.EqualTo(BloodPressureCategory.HypertensiveCrisis));
    }

    [Test]
    public void DetermineCategory_HighDiastolicOnly_ReturnsCorrectCategory()
    {
        // Arrange
        var reading = new Reading { Systolic = 110, Diastolic = 95 };

        // Act
        var category = reading.DetermineCategory();

        // Assert
        Assert.That(category, Is.EqualTo(BloodPressureCategory.HypertensionStage2));
    }

    [Test]
    public void IsCritical_WhenHypertensiveCrisis_ReturnsTrue()
    {
        // Arrange
        var reading = new Reading { Category = BloodPressureCategory.HypertensiveCrisis };

        // Act
        var result = reading.IsCritical();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsCritical_WhenNormal_ReturnsFalse()
    {
        // Arrange
        var reading = new Reading { Category = BloodPressureCategory.Normal };

        // Act
        var result = reading.IsCritical();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsCritical_WhenElevated_ReturnsFalse()
    {
        // Arrange
        var reading = new Reading { Category = BloodPressureCategory.Elevated };

        // Act
        var result = reading.IsCritical();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void Pulse_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var reading = new Reading();

        // Act
        reading.Pulse = 80;

        // Assert
        Assert.That(reading.Pulse, Is.EqualTo(80));
    }

    [Test]
    public void Position_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var reading = new Reading();
        var position = "Standing";

        // Act
        reading.Position = position;

        // Assert
        Assert.That(reading.Position, Is.EqualTo(position));
    }

    [Test]
    public void Arm_CanBeSet_SetsCorrectly()
    {
        // Arrange
        var reading = new Reading();
        var arm = "Right";

        // Act
        reading.Arm = arm;

        // Assert
        Assert.That(reading.Arm, Is.EqualTo(arm));
    }

    [Test]
    public void Notes_CanBeSetToNull_SetsCorrectly()
    {
        // Arrange
        var reading = new Reading { Notes = "Some notes" };

        // Act
        reading.Notes = null;

        // Assert
        Assert.That(reading.Notes, Is.Null);
    }
}
