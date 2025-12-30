using PersonalHealthDashboard.Api.Features.Vitals;
using PersonalHealthDashboard.Api.Features.WearableData;
using PersonalHealthDashboard.Api.Features.HealthTrends;

namespace PersonalHealthDashboard.Api.Tests;

[TestFixture]
public class ApiTests
{
    [Test]
    public void VitalDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var vital = new Core.Vital
        {
            VitalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            VitalType = Core.VitalType.HeartRate,
            Value = 72.5,
            Unit = "bpm",
            MeasuredAt = DateTime.UtcNow,
            Notes = "Test Notes",
            Source = "Manual",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = vital.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.VitalId, Is.EqualTo(vital.VitalId));
            Assert.That(dto.UserId, Is.EqualTo(vital.UserId));
            Assert.That(dto.VitalType, Is.EqualTo(vital.VitalType));
            Assert.That(dto.Value, Is.EqualTo(vital.Value));
            Assert.That(dto.Unit, Is.EqualTo(vital.Unit));
            Assert.That(dto.MeasuredAt, Is.EqualTo(vital.MeasuredAt));
            Assert.That(dto.Notes, Is.EqualTo(vital.Notes));
            Assert.That(dto.Source, Is.EqualTo(vital.Source));
            Assert.That(dto.CreatedAt, Is.EqualTo(vital.CreatedAt));
        });
    }

    [Test]
    public void WearableDataDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var wearableData = new Core.WearableData
        {
            WearableDataId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            DeviceName = "Apple Watch",
            DataType = "Steps",
            Value = 10000,
            Unit = "steps",
            RecordedAt = DateTime.UtcNow.AddHours(-2),
            SyncedAt = DateTime.UtcNow,
            Metadata = "{\"source\":\"fitness app\"}",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = wearableData.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.WearableDataId, Is.EqualTo(wearableData.WearableDataId));
            Assert.That(dto.UserId, Is.EqualTo(wearableData.UserId));
            Assert.That(dto.DeviceName, Is.EqualTo(wearableData.DeviceName));
            Assert.That(dto.DataType, Is.EqualTo(wearableData.DataType));
            Assert.That(dto.Value, Is.EqualTo(wearableData.Value));
            Assert.That(dto.Unit, Is.EqualTo(wearableData.Unit));
            Assert.That(dto.RecordedAt, Is.EqualTo(wearableData.RecordedAt));
            Assert.That(dto.SyncedAt, Is.EqualTo(wearableData.SyncedAt));
            Assert.That(dto.Metadata, Is.EqualTo(wearableData.Metadata));
            Assert.That(dto.CreatedAt, Is.EqualTo(wearableData.CreatedAt));
            Assert.That(dto.DataAgeInHours, Is.EqualTo(wearableData.GetDataAgeInHours()));
        });
    }

    [Test]
    public void HealthTrendDto_ToDto_MapsAllProperties()
    {
        // Arrange
        var healthTrend = new Core.HealthTrend
        {
            HealthTrendId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MetricName = "Weight",
            StartDate = DateTime.UtcNow.AddDays(-30),
            EndDate = DateTime.UtcNow,
            AverageValue = 75.5,
            MinValue = 73.0,
            MaxValue = 78.0,
            TrendDirection = "Decreasing",
            PercentageChange = -3.5,
            Insights = "Good progress on weight loss",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        var dto = healthTrend.ToDto();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(dto.HealthTrendId, Is.EqualTo(healthTrend.HealthTrendId));
            Assert.That(dto.UserId, Is.EqualTo(healthTrend.UserId));
            Assert.That(dto.MetricName, Is.EqualTo(healthTrend.MetricName));
            Assert.That(dto.StartDate, Is.EqualTo(healthTrend.StartDate));
            Assert.That(dto.EndDate, Is.EqualTo(healthTrend.EndDate));
            Assert.That(dto.AverageValue, Is.EqualTo(healthTrend.AverageValue));
            Assert.That(dto.MinValue, Is.EqualTo(healthTrend.MinValue));
            Assert.That(dto.MaxValue, Is.EqualTo(healthTrend.MaxValue));
            Assert.That(dto.TrendDirection, Is.EqualTo(healthTrend.TrendDirection));
            Assert.That(dto.PercentageChange, Is.EqualTo(healthTrend.PercentageChange));
            Assert.That(dto.Insights, Is.EqualTo(healthTrend.Insights));
            Assert.That(dto.CreatedAt, Is.EqualTo(healthTrend.CreatedAt));
            Assert.That(dto.PeriodDuration, Is.EqualTo(healthTrend.GetPeriodDuration()));
        });
    }
}
