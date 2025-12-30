// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace SleepQualityTracker.Core.Tests;

public class SleepSessionTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesSleepSession()
    {
        // Arrange
        var sessionId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var bedtime = new DateTime(2024, 1, 15, 22, 30, 0);
        var wakeTime = new DateTime(2024, 1, 16, 6, 30, 0);
        var totalSleepMinutes = 480; // 8 hours
        var sleepQuality = SleepQuality.Good;
        var timesAwakened = 2;
        var deepSleepMinutes = 120;
        var remSleepMinutes = 90;
        var sleepEfficiency = 95.5m;
        var notes = "Slept well after evening exercise";

        // Act
        var session = new SleepSession
        {
            SleepSessionId = sessionId,
            UserId = userId,
            Bedtime = bedtime,
            WakeTime = wakeTime,
            TotalSleepMinutes = totalSleepMinutes,
            SleepQuality = sleepQuality,
            TimesAwakened = timesAwakened,
            DeepSleepMinutes = deepSleepMinutes,
            RemSleepMinutes = remSleepMinutes,
            SleepEfficiency = sleepEfficiency,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(session.SleepSessionId, Is.EqualTo(sessionId));
            Assert.That(session.UserId, Is.EqualTo(userId));
            Assert.That(session.Bedtime, Is.EqualTo(bedtime));
            Assert.That(session.WakeTime, Is.EqualTo(wakeTime));
            Assert.That(session.TotalSleepMinutes, Is.EqualTo(totalSleepMinutes));
            Assert.That(session.SleepQuality, Is.EqualTo(sleepQuality));
            Assert.That(session.TimesAwakened, Is.EqualTo(timesAwakened));
            Assert.That(session.DeepSleepMinutes, Is.EqualTo(deepSleepMinutes));
            Assert.That(session.RemSleepMinutes, Is.EqualTo(remSleepMinutes));
            Assert.That(session.SleepEfficiency, Is.EqualTo(sleepEfficiency));
            Assert.That(session.Notes, Is.EqualTo(notes));
        });
    }

    [Test]
    public void DefaultValues_NewSleepSession_HasExpectedDefaults()
    {
        // Act
        var session = new SleepSession();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(session.TotalSleepMinutes, Is.EqualTo(0));
            Assert.That(session.SleepQuality, Is.EqualTo(SleepQuality.VeryPoor));
            Assert.That(session.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void GetTimeInBed_8HourSession_Returns480Minutes()
    {
        // Arrange
        var session = new SleepSession
        {
            Bedtime = new DateTime(2024, 1, 15, 22, 0, 0),
            WakeTime = new DateTime(2024, 1, 16, 6, 0, 0)
        };

        // Act
        var timeInBed = session.GetTimeInBed();

        // Assert
        Assert.That(timeInBed, Is.EqualTo(480));
    }

    [Test]
    public void GetTimeInBed_7HourSession_Returns420Minutes()
    {
        // Arrange
        var session = new SleepSession
        {
            Bedtime = new DateTime(2024, 1, 15, 23, 0, 0),
            WakeTime = new DateTime(2024, 1, 16, 6, 0, 0)
        };

        // Act
        var timeInBed = session.GetTimeInBed();

        // Assert
        Assert.That(timeInBed, Is.EqualTo(420));
    }

    [Test]
    public void GetTimeInBed_ShortNap_Returns90Minutes()
    {
        // Arrange
        var session = new SleepSession
        {
            Bedtime = new DateTime(2024, 1, 15, 14, 0, 0),
            WakeTime = new DateTime(2024, 1, 15, 15, 30, 0)
        };

        // Act
        var timeInBed = session.GetTimeInBed();

        // Assert
        Assert.That(timeInBed, Is.EqualTo(90));
    }

    [Test]
    public void MeetsRecommendedDuration_7Hours_ReturnsTrue()
    {
        // Arrange
        var session = new SleepSession
        {
            TotalSleepMinutes = 420 // 7 hours
        };

        // Act
        var result = session.MeetsRecommendedDuration();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void MeetsRecommendedDuration_8Hours_ReturnsTrue()
    {
        // Arrange
        var session = new SleepSession
        {
            TotalSleepMinutes = 480 // 8 hours
        };

        // Act
        var result = session.MeetsRecommendedDuration();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void MeetsRecommendedDuration_9Hours_ReturnsTrue()
    {
        // Arrange
        var session = new SleepSession
        {
            TotalSleepMinutes = 540 // 9 hours
        };

        // Act
        var result = session.MeetsRecommendedDuration();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void MeetsRecommendedDuration_6Hours_ReturnsFalse()
    {
        // Arrange
        var session = new SleepSession
        {
            TotalSleepMinutes = 360 // 6 hours
        };

        // Act
        var result = session.MeetsRecommendedDuration();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void MeetsRecommendedDuration_10Hours_ReturnsFalse()
    {
        // Arrange
        var session = new SleepSession
        {
            TotalSleepMinutes = 600 // 10 hours
        };

        // Act
        var result = session.MeetsRecommendedDuration();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsGoodQuality_GoodSleepQuality_ReturnsTrue()
    {
        // Arrange
        var session = new SleepSession
        {
            SleepQuality = SleepQuality.Good
        };

        // Act
        var result = session.IsGoodQuality();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsGoodQuality_ExcellentSleepQuality_ReturnsTrue()
    {
        // Arrange
        var session = new SleepSession
        {
            SleepQuality = SleepQuality.Excellent
        };

        // Act
        var result = session.IsGoodQuality();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void IsGoodQuality_FairSleepQuality_ReturnsFalse()
    {
        // Arrange
        var session = new SleepSession
        {
            SleepQuality = SleepQuality.Fair
        };

        // Act
        var result = session.IsGoodQuality();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsGoodQuality_PoorSleepQuality_ReturnsFalse()
    {
        // Arrange
        var session = new SleepSession
        {
            SleepQuality = SleepQuality.Poor
        };

        // Act
        var result = session.IsGoodQuality();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void IsGoodQuality_VeryPoorSleepQuality_ReturnsFalse()
    {
        // Arrange
        var session = new SleepSession
        {
            SleepQuality = SleepQuality.VeryPoor
        };

        // Act
        var result = session.IsGoodQuality();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void SleepQuality_AllEnumValues_CanBeAssigned()
    {
        // Arrange
        var session = new SleepSession();

        // Act & Assert
        session.SleepQuality = SleepQuality.VeryPoor;
        Assert.That(session.SleepQuality, Is.EqualTo(SleepQuality.VeryPoor));

        session.SleepQuality = SleepQuality.Poor;
        Assert.That(session.SleepQuality, Is.EqualTo(SleepQuality.Poor));

        session.SleepQuality = SleepQuality.Fair;
        Assert.That(session.SleepQuality, Is.EqualTo(SleepQuality.Fair));

        session.SleepQuality = SleepQuality.Good;
        Assert.That(session.SleepQuality, Is.EqualTo(SleepQuality.Good));

        session.SleepQuality = SleepQuality.Excellent;
        Assert.That(session.SleepQuality, Is.EqualTo(SleepQuality.Excellent));
    }

    [Test]
    public void TimesAwakened_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var session = new SleepSession
        {
            TimesAwakened = null
        };

        // Assert
        Assert.That(session.TimesAwakened, Is.Null);
    }

    [Test]
    public void DeepSleepMinutes_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var session = new SleepSession
        {
            DeepSleepMinutes = null
        };

        // Assert
        Assert.That(session.DeepSleepMinutes, Is.Null);
    }

    [Test]
    public void RemSleepMinutes_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var session = new SleepSession
        {
            RemSleepMinutes = null
        };

        // Assert
        Assert.That(session.RemSleepMinutes, Is.Null);
    }

    [Test]
    public void SleepEfficiency_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var session = new SleepSession
        {
            SleepEfficiency = null
        };

        // Assert
        Assert.That(session.SleepEfficiency, Is.Null);
    }

    [Test]
    public void Notes_OptionalField_CanBeNull()
    {
        // Arrange & Act
        var session = new SleepSession
        {
            Notes = null
        };

        // Assert
        Assert.That(session.Notes, Is.Null);
    }
}
