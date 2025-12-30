// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BloodPressureMonitor.Core.Tests;

public class EventsTests
{
    [Test]
    public void ReadingRecordedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var readingId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var systolic = 120;
        var diastolic = 80;
        var category = BloodPressureCategory.Normal;

        // Act
        var evt = new ReadingRecordedEvent
        {
            ReadingId = readingId,
            UserId = userId,
            Systolic = systolic,
            Diastolic = diastolic,
            Category = category
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.ReadingId, Is.EqualTo(readingId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.Systolic, Is.EqualTo(systolic));
            Assert.That(evt.Diastolic, Is.EqualTo(diastolic));
            Assert.That(evt.Category, Is.EqualTo(category));
            Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void ReadingRecordedEvent_WithTimestamp_CreatesEvent()
    {
        // Arrange
        var readingId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var systolic = 145;
        var diastolic = 95;
        var category = BloodPressureCategory.HypertensionStage2;
        var timestamp = DateTime.UtcNow.AddMinutes(-5);

        // Act
        var evt = new ReadingRecordedEvent
        {
            ReadingId = readingId,
            UserId = userId,
            Systolic = systolic,
            Diastolic = diastolic,
            Category = category,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
    }

    [Test]
    public void ReadingRecordedEvent_HighBloodPressure_CreatesEvent()
    {
        // Arrange
        var readingId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var systolic = 185;
        var diastolic = 125;
        var category = BloodPressureCategory.HypertensiveCrisis;

        // Act
        var evt = new ReadingRecordedEvent
        {
            ReadingId = readingId,
            UserId = userId,
            Systolic = systolic,
            Diastolic = diastolic,
            Category = category
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.Systolic, Is.EqualTo(185));
            Assert.That(evt.Diastolic, Is.EqualTo(125));
            Assert.That(evt.Category, Is.EqualTo(BloodPressureCategory.HypertensiveCrisis));
        });
    }

    [Test]
    public void TrendCalculatedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var trendId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var avgSystolic = 125.5m;
        var avgDiastolic = 82.3m;
        var trendDirection = "Improving";

        // Act
        var evt = new TrendCalculatedEvent
        {
            TrendId = trendId,
            UserId = userId,
            AverageSystolic = avgSystolic,
            AverageDiastolic = avgDiastolic,
            TrendDirection = trendDirection
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.TrendId, Is.EqualTo(trendId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.AverageSystolic, Is.EqualTo(avgSystolic));
            Assert.That(evt.AverageDiastolic, Is.EqualTo(avgDiastolic));
            Assert.That(evt.TrendDirection, Is.EqualTo(trendDirection));
            Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void TrendCalculatedEvent_WithTimestamp_CreatesEvent()
    {
        // Arrange
        var trendId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var avgSystolic = 130.2m;
        var avgDiastolic = 85.7m;
        var trendDirection = "Stable";
        var timestamp = DateTime.UtcNow.AddMinutes(-10);

        // Act
        var evt = new TrendCalculatedEvent
        {
            TrendId = trendId,
            UserId = userId,
            AverageSystolic = avgSystolic,
            AverageDiastolic = avgDiastolic,
            TrendDirection = trendDirection,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
    }

    [Test]
    public void TrendCalculatedEvent_EmptyTrendDirection_CreatesEvent()
    {
        // Arrange
        var trendId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        // Act
        var evt = new TrendCalculatedEvent
        {
            TrendId = trendId,
            UserId = userId,
            AverageSystolic = 120m,
            AverageDiastolic = 80m,
            TrendDirection = string.Empty
        };

        // Assert
        Assert.That(evt.TrendDirection, Is.EqualTo(string.Empty));
    }

    [Test]
    public void ReadingRecordedEvent_IsRecord_SupportsWithExpression()
    {
        // Arrange
        var evt = new ReadingRecordedEvent
        {
            ReadingId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Systolic = 120,
            Diastolic = 80,
            Category = BloodPressureCategory.Normal
        };
        var newSystolic = 130;

        // Act
        var newEvt = evt with { Systolic = newSystolic };

        // Assert
        Assert.That(newEvt.Systolic, Is.EqualTo(newSystolic));
        Assert.That(newEvt.ReadingId, Is.EqualTo(evt.ReadingId));
    }

    [Test]
    public void TrendCalculatedEvent_IsRecord_SupportsWithExpression()
    {
        // Arrange
        var evt = new TrendCalculatedEvent
        {
            TrendId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            AverageSystolic = 120m,
            AverageDiastolic = 80m,
            TrendDirection = "Improving"
        };
        var newDirection = "Worsening";

        // Act
        var newEvt = evt with { TrendDirection = newDirection };

        // Assert
        Assert.That(newEvt.TrendDirection, Is.EqualTo(newDirection));
        Assert.That(newEvt.TrendId, Is.EqualTo(evt.TrendId));
    }
}
