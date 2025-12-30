// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalHealthDashboard.Infrastructure.Tests;

/// <summary>
/// Unit tests for the PersonalHealthDashboardContext.
/// </summary>
[TestFixture]
public class PersonalHealthDashboardContextTests
{
    private PersonalHealthDashboardContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<PersonalHealthDashboardContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new PersonalHealthDashboardContext(options);
    }

    /// <summary>
    /// Tears down the test context.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    /// <summary>
    /// Tests that Vitals can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Vitals_CanAddAndRetrieve()
    {
        // Arrange
        var vital = new Vital
        {
            VitalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            VitalType = VitalType.BloodPressure,
            Value = 120,
            Unit = "mmHg",
            MeasuredAt = DateTime.UtcNow,
            Notes = "Morning measurement",
            Source = "Manual",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Vitals.Add(vital);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Vitals.FindAsync(vital.VitalId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.VitalType, Is.EqualTo(VitalType.BloodPressure));
        Assert.That(retrieved.Value, Is.EqualTo(120));
        Assert.That(retrieved.Unit, Is.EqualTo("mmHg"));
    }

    /// <summary>
    /// Tests that WearableData can be added and retrieved.
    /// </summary>
    [Test]
    public async Task WearableData_CanAddAndRetrieve()
    {
        // Arrange
        var wearableData = new WearableData
        {
            WearableDataId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            DeviceName = "Apple Watch",
            DataType = "Steps",
            Value = 10000,
            Unit = "steps",
            RecordedAt = DateTime.UtcNow,
            SyncedAt = DateTime.UtcNow,
            Metadata = "{\"goal\": 10000}",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.WearableData.Add(wearableData);
        await _context.SaveChangesAsync();

        var retrieved = await _context.WearableData.FindAsync(wearableData.WearableDataId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.DeviceName, Is.EqualTo("Apple Watch"));
        Assert.That(retrieved.DataType, Is.EqualTo("Steps"));
        Assert.That(retrieved.Value, Is.EqualTo(10000));
    }

    /// <summary>
    /// Tests that HealthTrends can be added and retrieved.
    /// </summary>
    [Test]
    public async Task HealthTrends_CanAddAndRetrieve()
    {
        // Arrange
        var healthTrend = new HealthTrend
        {
            HealthTrendId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            MetricName = "Weight",
            StartDate = DateTime.UtcNow.AddDays(-30),
            EndDate = DateTime.UtcNow,
            AverageValue = 170.5,
            MinValue = 168.0,
            MaxValue = 173.0,
            TrendDirection = "Decreasing",
            PercentageChange = -2.5,
            Insights = "Good progress on weight loss",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.HealthTrends.Add(healthTrend);
        await _context.SaveChangesAsync();

        var retrieved = await _context.HealthTrends.FindAsync(healthTrend.HealthTrendId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.MetricName, Is.EqualTo("Weight"));
        Assert.That(retrieved.TrendDirection, Is.EqualTo("Decreasing"));
        Assert.That(retrieved.PercentageChange, Is.EqualTo(-2.5));
    }

    /// <summary>
    /// Tests that multiple vitals can be queried by user.
    /// </summary>
    [Test]
    public async Task Vitals_CanQueryByUser()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var vitals = new List<Vital>
        {
            new Vital
            {
                VitalId = Guid.NewGuid(),
                UserId = userId,
                VitalType = VitalType.HeartRate,
                Value = 72,
                Unit = "bpm",
                MeasuredAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
            },
            new Vital
            {
                VitalId = Guid.NewGuid(),
                UserId = userId,
                VitalType = VitalType.Weight,
                Value = 170,
                Unit = "lbs",
                MeasuredAt = DateTime.UtcNow,
                CreatedAt = DateTime.UtcNow,
            },
        };

        // Act
        _context.Vitals.AddRange(vitals);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Vitals
            .Where(v => v.UserId == userId)
            .ToListAsync();

        // Assert
        Assert.That(retrieved, Has.Count.EqualTo(2));
        Assert.That(retrieved.All(v => v.UserId == userId), Is.True);
    }
}
