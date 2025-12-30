namespace PersonalHealthDashboard.Core.Tests;

public class WearableDataTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesWearableData()
    {
        // Arrange
        var wearableDataId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var recordedAt = DateTime.UtcNow.AddHours(-2);
        var syncedAt = DateTime.UtcNow;

        // Act
        var wearableData = new WearableData
        {
            WearableDataId = wearableDataId,
            UserId = userId,
            DeviceName = "Fitbit Charge 5",
            DataType = "Steps",
            Value = 8500,
            Unit = "steps",
            RecordedAt = recordedAt,
            SyncedAt = syncedAt,
            Metadata = "{\"source\":\"automatic\"}"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(wearableData.WearableDataId, Is.EqualTo(wearableDataId));
            Assert.That(wearableData.UserId, Is.EqualTo(userId));
            Assert.That(wearableData.DeviceName, Is.EqualTo("Fitbit Charge 5"));
            Assert.That(wearableData.DataType, Is.EqualTo("Steps"));
            Assert.That(wearableData.Value, Is.EqualTo(8500));
            Assert.That(wearableData.Unit, Is.EqualTo("steps"));
            Assert.That(wearableData.RecordedAt, Is.EqualTo(recordedAt));
            Assert.That(wearableData.SyncedAt, Is.EqualTo(syncedAt));
            Assert.That(wearableData.Metadata, Is.EqualTo("{\"source\":\"automatic\"}"));
            Assert.That(wearableData.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void IsFromToday_RecordedToday_ReturnsTrue()
    {
        // Arrange
        var wearableData = new WearableData
        {
            WearableDataId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            DeviceName = "Apple Watch",
            DataType = "Steps",
            Value = 5000,
            Unit = "steps",
            RecordedAt = DateTime.UtcNow
        };

        // Act
        var result = wearableData.IsFromToday();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsFromToday_RecordedYesterday_ReturnsFalse()
    {
        // Arrange
        var wearableData = new WearableData
        {
            WearableDataId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            DeviceName = "Apple Watch",
            DataType = "Steps",
            Value = 5000,
            Unit = "steps",
            RecordedAt = DateTime.UtcNow.AddDays(-1)
        };

        // Act
        var result = wearableData.IsFromToday();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsFromToday_RecordedTomorrow_ReturnsFalse()
    {
        // Arrange
        var wearableData = new WearableData
        {
            WearableDataId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            DeviceName = "Apple Watch",
            DataType = "Steps",
            Value = 5000,
            Unit = "steps",
            RecordedAt = DateTime.UtcNow.AddDays(1)
        };

        // Act
        var result = wearableData.IsFromToday();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void GetDataAgeInHours_Recorded2HoursAgo_Returns2()
    {
        // Arrange
        var wearableData = new WearableData
        {
            WearableDataId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            DeviceName = "Apple Watch",
            DataType = "Steps",
            Value = 5000,
            Unit = "steps",
            RecordedAt = DateTime.UtcNow.AddHours(-2)
        };

        // Act
        var age = wearableData.GetDataAgeInHours();

        // Assert
        Assert.That(age, Is.GreaterThanOrEqualTo(2.0).And.LessThan(2.1));
    }

    [Test]
    public void GetDataAgeInHours_Recorded24HoursAgo_Returns24()
    {
        // Arrange
        var wearableData = new WearableData
        {
            WearableDataId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            DeviceName = "Apple Watch",
            DataType = "Steps",
            Value = 5000,
            Unit = "steps",
            RecordedAt = DateTime.UtcNow.AddHours(-24)
        };

        // Act
        var age = wearableData.GetDataAgeInHours();

        // Assert
        Assert.That(age, Is.GreaterThanOrEqualTo(24.0).And.LessThan(24.1));
    }

    [Test]
    public void GetDataAgeInHours_RecordedJustNow_ReturnsNearZero()
    {
        // Arrange
        var wearableData = new WearableData
        {
            WearableDataId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            DeviceName = "Apple Watch",
            DataType = "Steps",
            Value = 5000,
            Unit = "steps",
            RecordedAt = DateTime.UtcNow
        };

        // Act
        var age = wearableData.GetDataAgeInHours();

        // Assert
        Assert.That(age, Is.LessThan(0.1));
    }

    [Test]
    public void WearableData_DifferentDataTypes_CanBeCreated()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(() => new WearableData { DataType = "Steps" }, Throws.Nothing);
            Assert.That(() => new WearableData { DataType = "Calories" }, Throws.Nothing);
            Assert.That(() => new WearableData { DataType = "Distance" }, Throws.Nothing);
            Assert.That(() => new WearableData { DataType = "HeartRate" }, Throws.Nothing);
            Assert.That(() => new WearableData { DataType = "Sleep" }, Throws.Nothing);
        });
    }

    [Test]
    public void WearableData_DifferentDevices_CanBeCreated()
    {
        // Arrange & Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(() => new WearableData { DeviceName = "Fitbit" }, Throws.Nothing);
            Assert.That(() => new WearableData { DeviceName = "Apple Watch" }, Throws.Nothing);
            Assert.That(() => new WearableData { DeviceName = "Garmin" }, Throws.Nothing);
            Assert.That(() => new WearableData { DeviceName = "Samsung Galaxy Watch" }, Throws.Nothing);
        });
    }
}
