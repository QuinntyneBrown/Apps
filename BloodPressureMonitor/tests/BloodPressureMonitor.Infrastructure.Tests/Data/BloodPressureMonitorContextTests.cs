// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BloodPressureMonitor.Infrastructure.Tests;

/// <summary>
/// Unit tests for the BloodPressureMonitorContext.
/// </summary>
[TestFixture]
public class BloodPressureMonitorContextTests
{
    private BloodPressureMonitorContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<BloodPressureMonitorContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new BloodPressureMonitorContext(options);
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
    /// Tests that Readings can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Readings_CanAddAndRetrieve()
    {
        // Arrange
        var reading = new Reading
        {
            ReadingId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Systolic = 120,
            Diastolic = 80,
            Pulse = 72,
            Category = BloodPressureCategory.Elevated,
            MeasuredAt = DateTime.UtcNow,
            Position = "Sitting",
            Arm = "Left",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Readings.Add(reading);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Readings.FindAsync(reading.ReadingId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Systolic, Is.EqualTo(120));
        Assert.That(retrieved.Diastolic, Is.EqualTo(80));
        Assert.That(retrieved.Category, Is.EqualTo(BloodPressureCategory.Elevated));
    }

    /// <summary>
    /// Tests that Trends can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Trends_CanAddAndRetrieve()
    {
        // Arrange
        var trend = new Trend
        {
            TrendId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            StartDate = DateTime.UtcNow.AddDays(-7),
            EndDate = DateTime.UtcNow,
            AverageSystolic = 122.5m,
            AverageDiastolic = 78.3m,
            HighestSystolic = 135,
            HighestDiastolic = 85,
            LowestSystolic = 110,
            LowestDiastolic = 70,
            ReadingCount = 14,
            TrendDirection = "Improving",
            Insights = "Good progress",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Trends.Add(trend);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Trends.FindAsync(trend.TrendId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.AverageSystolic, Is.EqualTo(122.5m));
        Assert.That(retrieved.TrendDirection, Is.EqualTo("Improving"));
        Assert.That(retrieved.ReadingCount, Is.EqualTo(14));
    }

    /// <summary>
    /// Tests that multiple readings can be stored for a user.
    /// </summary>
    [Test]
    public async Task MultipleReadings_CanBeStored()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var reading1 = new Reading
        {
            ReadingId = Guid.NewGuid(),
            UserId = userId,
            Systolic = 118,
            Diastolic = 76,
            Category = BloodPressureCategory.Normal,
            MeasuredAt = DateTime.UtcNow.AddDays(-1),
            CreatedAt = DateTime.UtcNow,
        };

        var reading2 = new Reading
        {
            ReadingId = Guid.NewGuid(),
            UserId = userId,
            Systolic = 132,
            Diastolic = 84,
            Category = BloodPressureCategory.HypertensionStage1,
            MeasuredAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Readings.Add(reading1);
        _context.Readings.Add(reading2);
        await _context.SaveChangesAsync();

        var userReadings = await _context.Readings
            .Where(r => r.UserId == userId)
            .ToListAsync();

        // Assert
        Assert.That(userReadings, Has.Count.EqualTo(2));
        Assert.That(userReadings.Any(r => r.Systolic == 118), Is.True);
        Assert.That(userReadings.Any(r => r.Systolic == 132), Is.True);
    }

    /// <summary>
    /// Tests that readings can be queried by category.
    /// </summary>
    [Test]
    public async Task Readings_CanBeQueriedByCategory()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var normalReading = new Reading
        {
            ReadingId = Guid.NewGuid(),
            UserId = userId,
            Systolic = 115,
            Diastolic = 75,
            Category = BloodPressureCategory.Normal,
            MeasuredAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        var elevatedReading = new Reading
        {
            ReadingId = Guid.NewGuid(),
            UserId = userId,
            Systolic = 125,
            Diastolic = 78,
            Category = BloodPressureCategory.Elevated,
            MeasuredAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Readings.Add(normalReading);
        _context.Readings.Add(elevatedReading);
        await _context.SaveChangesAsync();

        // Act
        var normalReadings = await _context.Readings
            .Where(r => r.Category == BloodPressureCategory.Normal)
            .ToListAsync();

        // Assert
        Assert.That(normalReadings, Has.Count.EqualTo(1));
        Assert.That(normalReadings[0].Systolic, Is.EqualTo(115));
    }
}
