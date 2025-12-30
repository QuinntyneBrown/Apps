// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FocusSessionTracker.Core;

namespace FocusSessionTracker.Core.Tests;

public class SessionAnalyticsTests
{
    [Test]
    public void SessionAnalytics_CanBeCreated_WithValidProperties()
    {
        // Arrange
        var analyticsId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var periodStart = new DateTime(2024, 6, 1);
        var periodEnd = new DateTime(2024, 6, 30);

        // Act
        var analytics = new SessionAnalytics
        {
            SessionAnalyticsId = analyticsId,
            UserId = userId,
            PeriodStartDate = periodStart,
            PeriodEndDate = periodEnd,
            TotalSessions = 25,
            TotalFocusMinutes = 1500.0,
            AverageFocusScore = 7.5,
            TotalDistractions = 15,
            CompletionRate = 92.5,
            MostProductiveSessionType = SessionType.DeepWork
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(analytics.SessionAnalyticsId, Is.EqualTo(analyticsId));
            Assert.That(analytics.UserId, Is.EqualTo(userId));
            Assert.That(analytics.PeriodStartDate, Is.EqualTo(periodStart));
            Assert.That(analytics.PeriodEndDate, Is.EqualTo(periodEnd));
            Assert.That(analytics.TotalSessions, Is.EqualTo(25));
            Assert.That(analytics.TotalFocusMinutes, Is.EqualTo(1500.0));
            Assert.That(analytics.AverageFocusScore, Is.EqualTo(7.5));
            Assert.That(analytics.TotalDistractions, Is.EqualTo(15));
            Assert.That(analytics.CompletionRate, Is.EqualTo(92.5));
            Assert.That(analytics.MostProductiveSessionType, Is.EqualTo(SessionType.DeepWork));
            Assert.That(analytics.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void SessionAnalytics_DefaultValues_AreSetCorrectly()
    {
        // Act
        var analytics = new SessionAnalytics();

        // Assert
        Assert.That(analytics.CreatedAt, Is.Not.EqualTo(default(DateTime)));
    }

    [Test]
    public void GetAverageSessionDuration_WithSessions_ReturnsCorrectAverage()
    {
        // Arrange
        var analytics = new SessionAnalytics
        {
            TotalSessions = 10,
            TotalFocusMinutes = 600.0
        };

        // Act
        var average = analytics.GetAverageSessionDuration();

        // Assert
        Assert.That(average, Is.EqualTo(60.0));
    }

    [Test]
    public void GetAverageSessionDuration_WithZeroSessions_ReturnsZero()
    {
        // Arrange
        var analytics = new SessionAnalytics
        {
            TotalSessions = 0,
            TotalFocusMinutes = 0
        };

        // Act
        var average = analytics.GetAverageSessionDuration();

        // Assert
        Assert.That(average, Is.EqualTo(0));
    }

    [Test]
    public void GetAverageSessionDuration_WithOnlyOneSession_ReturnsTotal()
    {
        // Arrange
        var analytics = new SessionAnalytics
        {
            TotalSessions = 1,
            TotalFocusMinutes = 90.0
        };

        // Act
        var average = analytics.GetAverageSessionDuration();

        // Assert
        Assert.That(average, Is.EqualTo(90.0));
    }

    [Test]
    public void GetAverageDistractions_WithSessions_ReturnsCorrectAverage()
    {
        // Arrange
        var analytics = new SessionAnalytics
        {
            TotalSessions = 10,
            TotalDistractions = 25
        };

        // Act
        var average = analytics.GetAverageDistractions();

        // Assert
        Assert.That(average, Is.EqualTo(2.5));
    }

    [Test]
    public void GetAverageDistractions_WithZeroSessions_ReturnsZero()
    {
        // Arrange
        var analytics = new SessionAnalytics
        {
            TotalSessions = 0,
            TotalDistractions = 0
        };

        // Act
        var average = analytics.GetAverageDistractions();

        // Assert
        Assert.That(average, Is.EqualTo(0));
    }

    [Test]
    public void GetAverageDistractions_WithNoDistractions_ReturnsZero()
    {
        // Arrange
        var analytics = new SessionAnalytics
        {
            TotalSessions = 10,
            TotalDistractions = 0
        };

        // Act
        var average = analytics.GetAverageDistractions();

        // Assert
        Assert.That(average, Is.EqualTo(0));
    }

    [Test]
    public void SessionAnalytics_CanHaveNullableProperties_SetToNull()
    {
        // Arrange & Act
        var analytics = new SessionAnalytics
        {
            AverageFocusScore = null,
            MostProductiveSessionType = null
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(analytics.AverageFocusScore, Is.Null);
            Assert.That(analytics.MostProductiveSessionType, Is.Null);
        });
    }

    [Test]
    public void SessionAnalytics_CompletionRate_CanBeZero()
    {
        // Arrange & Act
        var analytics = new SessionAnalytics { CompletionRate = 0 };

        // Assert
        Assert.That(analytics.CompletionRate, Is.EqualTo(0));
    }

    [Test]
    public void SessionAnalytics_CompletionRate_CanBeOneHundred()
    {
        // Arrange & Act
        var analytics = new SessionAnalytics { CompletionRate = 100.0 };

        // Assert
        Assert.That(analytics.CompletionRate, Is.EqualTo(100.0));
    }

    [Test]
    public void SessionAnalytics_TotalSessions_CanBeZero()
    {
        // Arrange & Act
        var analytics = new SessionAnalytics { TotalSessions = 0 };

        // Assert
        Assert.That(analytics.TotalSessions, Is.EqualTo(0));
    }

    [Test]
    public void SessionAnalytics_TotalFocusMinutes_CanBeZero()
    {
        // Arrange & Act
        var analytics = new SessionAnalytics { TotalFocusMinutes = 0 };

        // Assert
        Assert.That(analytics.TotalFocusMinutes, Is.EqualTo(0));
    }

    [Test]
    public void SessionAnalytics_TotalDistractions_CanBeZero()
    {
        // Arrange & Act
        var analytics = new SessionAnalytics { TotalDistractions = 0 };

        // Assert
        Assert.That(analytics.TotalDistractions, Is.EqualTo(0));
    }

    [Test]
    public void GetAverageSessionDuration_WithLargeTotals_CalculatesCorrectly()
    {
        // Arrange
        var analytics = new SessionAnalytics
        {
            TotalSessions = 100,
            TotalFocusMinutes = 5000.0
        };

        // Act
        var average = analytics.GetAverageSessionDuration();

        // Assert
        Assert.That(average, Is.EqualTo(50.0));
    }

    [Test]
    public void GetAverageDistractions_WithFractionalResult_ReturnsDecimal()
    {
        // Arrange
        var analytics = new SessionAnalytics
        {
            TotalSessions = 7,
            TotalDistractions = 10
        };

        // Act
        var average = analytics.GetAverageDistractions();

        // Assert
        Assert.That(average, Is.EqualTo(10.0 / 7.0).Within(0.001));
    }
}
