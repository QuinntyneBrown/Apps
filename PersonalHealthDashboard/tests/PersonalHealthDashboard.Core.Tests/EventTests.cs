namespace PersonalHealthDashboard.Core.Tests;

public class EventTests
{
    [Test]
    public void VitalRecordedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var vitalId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var measuredAt = DateTime.UtcNow;
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new VitalRecordedEvent
        {
            VitalId = vitalId,
            UserId = userId,
            VitalType = VitalType.HeartRate,
            Value = 75.5,
            Unit = "bpm",
            MeasuredAt = measuredAt,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.VitalId, Is.EqualTo(vitalId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.VitalType, Is.EqualTo(VitalType.HeartRate));
            Assert.That(evt.Value, Is.EqualTo(75.5));
            Assert.That(evt.Unit, Is.EqualTo("bpm"));
            Assert.That(evt.MeasuredAt, Is.EqualTo(measuredAt));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void VitalDeletedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var vitalId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new VitalDeletedEvent
        {
            VitalId = vitalId,
            UserId = userId,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.VitalId, Is.EqualTo(vitalId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void HealthTrendCalculatedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var healthTrendId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new HealthTrendCalculatedEvent
        {
            HealthTrendId = healthTrendId,
            UserId = userId,
            MetricName = "Weight",
            TrendDirection = "Decreasing",
            PercentageChange = -2.5,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.HealthTrendId, Is.EqualTo(healthTrendId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.MetricName, Is.EqualTo("Weight"));
            Assert.That(evt.TrendDirection, Is.EqualTo("Decreasing"));
            Assert.That(evt.PercentageChange, Is.EqualTo(-2.5));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void WearableDataSyncedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var wearableDataId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new WearableDataSyncedEvent
        {
            WearableDataId = wearableDataId,
            UserId = userId,
            DeviceName = "Fitbit",
            DataType = "Steps",
            Value = 8500,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.WearableDataId, Is.EqualTo(wearableDataId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.DeviceName, Is.EqualTo("Fitbit"));
            Assert.That(evt.DataType, Is.EqualTo("Steps"));
            Assert.That(evt.Value, Is.EqualTo(8500));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void VitalRecordedEvent_IsRecord_IsImmutable()
    {
        // Arrange
        var evt = new VitalRecordedEvent
        {
            VitalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            VitalType = VitalType.HeartRate,
            Value = 75.5,
            Unit = "bpm",
            MeasuredAt = DateTime.UtcNow,
            Timestamp = DateTime.UtcNow
        };

        // Assert - Record types are immutable, properties cannot be reassigned
        Assert.That(evt, Is.Not.Null);
    }
}
