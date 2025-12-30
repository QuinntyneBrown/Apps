namespace PersonalHealthDashboard.Core.Tests;

public class VitalTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesVital()
    {
        // Arrange
        var vitalId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var measuredAt = DateTime.UtcNow;

        // Act
        var vital = new Vital
        {
            VitalId = vitalId,
            UserId = userId,
            VitalType = VitalType.HeartRate,
            Value = 75.5,
            Unit = "bpm",
            MeasuredAt = measuredAt,
            Notes = "Morning measurement",
            Source = "Manual"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(vital.VitalId, Is.EqualTo(vitalId));
            Assert.That(vital.UserId, Is.EqualTo(userId));
            Assert.That(vital.VitalType, Is.EqualTo(VitalType.HeartRate));
            Assert.That(vital.Value, Is.EqualTo(75.5));
            Assert.That(vital.Unit, Is.EqualTo("bpm"));
            Assert.That(vital.MeasuredAt, Is.EqualTo(measuredAt));
            Assert.That(vital.Notes, Is.EqualTo("Morning measurement"));
            Assert.That(vital.Source, Is.EqualTo("Manual"));
            Assert.That(vital.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void IsWithinNormalRange_ValueWithinRange_ReturnsTrue()
    {
        // Arrange
        var vital = new Vital
        {
            VitalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            VitalType = VitalType.HeartRate,
            Value = 75,
            Unit = "bpm"
        };

        // Act
        var result = vital.IsWithinNormalRange(60, 100);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsWithinNormalRange_ValueBelowRange_ReturnsFalse()
    {
        // Arrange
        var vital = new Vital
        {
            VitalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            VitalType = VitalType.HeartRate,
            Value = 50,
            Unit = "bpm"
        };

        // Act
        var result = vital.IsWithinNormalRange(60, 100);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsWithinNormalRange_ValueAboveRange_ReturnsFalse()
    {
        // Arrange
        var vital = new Vital
        {
            VitalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            VitalType = VitalType.HeartRate,
            Value = 120,
            Unit = "bpm"
        };

        // Act
        var result = vital.IsWithinNormalRange(60, 100);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsWithinNormalRange_ValueAtMinimum_ReturnsTrue()
    {
        // Arrange
        var vital = new Vital
        {
            VitalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            VitalType = VitalType.HeartRate,
            Value = 60,
            Unit = "bpm"
        };

        // Act
        var result = vital.IsWithinNormalRange(60, 100);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsWithinNormalRange_ValueAtMaximum_ReturnsTrue()
    {
        // Arrange
        var vital = new Vital
        {
            VitalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            VitalType = VitalType.HeartRate,
            Value = 100,
            Unit = "bpm"
        };

        // Act
        var result = vital.IsWithinNormalRange(60, 100);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsRecent_MeasuredWithinLast24Hours_ReturnsTrue()
    {
        // Arrange
        var vital = new Vital
        {
            VitalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            VitalType = VitalType.HeartRate,
            Value = 75,
            Unit = "bpm",
            MeasuredAt = DateTime.UtcNow.AddHours(-12)
        };

        // Act
        var result = vital.IsRecent();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsRecent_MeasuredMoreThan24HoursAgo_ReturnsFalse()
    {
        // Arrange
        var vital = new Vital
        {
            VitalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            VitalType = VitalType.HeartRate,
            Value = 75,
            Unit = "bpm",
            MeasuredAt = DateTime.UtcNow.AddHours(-25)
        };

        // Act
        var result = vital.IsRecent();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsRecent_MeasuredExactly24HoursAgo_ReturnsTrue()
    {
        // Arrange
        var vital = new Vital
        {
            VitalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            VitalType = VitalType.HeartRate,
            Value = 75,
            Unit = "bpm",
            MeasuredAt = DateTime.UtcNow.AddHours(-24)
        };

        // Act
        var result = vital.IsRecent();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void Vital_AllVitalTypes_CanBeAssigned()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(() => new Vital { VitalType = VitalType.BloodPressure }, Throws.Nothing);
            Assert.That(() => new Vital { VitalType = VitalType.HeartRate }, Throws.Nothing);
            Assert.That(() => new Vital { VitalType = VitalType.Temperature }, Throws.Nothing);
            Assert.That(() => new Vital { VitalType = VitalType.BloodGlucose }, Throws.Nothing);
            Assert.That(() => new Vital { VitalType = VitalType.OxygenSaturation }, Throws.Nothing);
            Assert.That(() => new Vital { VitalType = VitalType.Weight }, Throws.Nothing);
            Assert.That(() => new Vital { VitalType = VitalType.RespiratoryRate }, Throws.Nothing);
        });
    }
}
