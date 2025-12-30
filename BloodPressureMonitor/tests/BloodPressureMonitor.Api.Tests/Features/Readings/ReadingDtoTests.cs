// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using BloodPressureMonitor.Core;

namespace BloodPressureMonitor.Api.Tests;

/// <summary>
/// Unit tests for ReadingDto mapping.
/// </summary>
[TestFixture]
public class ReadingDtoTests
{
    /// <summary>
    /// Tests that ToDto maps all properties correctly.
    /// </summary>
    [Test]
    public void ToDto_MapsAllPropertiesCorrectly()
    {
        // Arrange
        var reading = new Reading
        {
            ReadingId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Systolic = 120,
            Diastolic = 80,
            Pulse = 70,
            Category = BloodPressureCategory.Normal,
            MeasuredAt = new DateTime(2024, 1, 15, 10, 30, 0, DateTimeKind.Utc),
            Position = "Sitting",
            Arm = "Left",
            Notes = "Morning reading",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = reading.ToDto();

        // Assert
        Assert.That(dto.ReadingId, Is.EqualTo(reading.ReadingId));
        Assert.That(dto.UserId, Is.EqualTo(reading.UserId));
        Assert.That(dto.Systolic, Is.EqualTo(120));
        Assert.That(dto.Diastolic, Is.EqualTo(80));
        Assert.That(dto.Pulse, Is.EqualTo(70));
        Assert.That(dto.Category, Is.EqualTo(BloodPressureCategory.Normal));
        Assert.That(dto.Position, Is.EqualTo("Sitting"));
        Assert.That(dto.Arm, Is.EqualTo("Left"));
        Assert.That(dto.Notes, Is.EqualTo("Morning reading"));
        Assert.That(dto.IsCritical, Is.False);
    }

    /// <summary>
    /// Tests that ToDto correctly identifies critical readings.
    /// </summary>
    [Test]
    public void ToDto_IdentifiesCriticalReadings()
    {
        // Arrange
        var reading = new Reading
        {
            ReadingId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Systolic = 185,
            Diastolic = 125,
            Category = BloodPressureCategory.HypertensiveCrisis,
            MeasuredAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = reading.ToDto();

        // Assert
        Assert.That(dto.IsCritical, Is.True);
        Assert.That(dto.Category, Is.EqualTo(BloodPressureCategory.HypertensiveCrisis));
    }

    /// <summary>
    /// Tests that ToDto handles null optional properties.
    /// </summary>
    [Test]
    public void ToDto_HandlesNullOptionalProperties()
    {
        // Arrange
        var reading = new Reading
        {
            ReadingId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Systolic = 130,
            Diastolic = 85,
            Pulse = null,
            Category = BloodPressureCategory.HypertensionStage1,
            MeasuredAt = DateTime.UtcNow,
            Position = null,
            Arm = null,
            Notes = null,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = reading.ToDto();

        // Assert
        Assert.That(dto.Pulse, Is.Null);
        Assert.That(dto.Position, Is.Null);
        Assert.That(dto.Arm, Is.Null);
        Assert.That(dto.Notes, Is.Null);
    }
}
