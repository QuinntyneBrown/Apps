// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TimeAuditTracker.Core.Tests;

public class AuditReportTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesAuditReport()
    {
        // Arrange
        var auditReportId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var title = "Weekly Audit Report";
        var startDate = new DateTime(2024, 3, 11);
        var endDate = new DateTime(2024, 3, 17);
        var totalTrackedHours = 100.0;
        var productiveHours = 75.0;
        var summary = "Good week";
        var insights = "More productive in mornings";
        var recommendations = "Continue morning routine";

        // Act
        var report = new AuditReport
        {
            AuditReportId = auditReportId,
            UserId = userId,
            Title = title,
            StartDate = startDate,
            EndDate = endDate,
            TotalTrackedHours = totalTrackedHours,
            ProductiveHours = productiveHours,
            Summary = summary,
            Insights = insights,
            Recommendations = recommendations
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(report.AuditReportId, Is.EqualTo(auditReportId));
            Assert.That(report.UserId, Is.EqualTo(userId));
            Assert.That(report.Title, Is.EqualTo(title));
            Assert.That(report.StartDate, Is.EqualTo(startDate));
            Assert.That(report.EndDate, Is.EqualTo(endDate));
            Assert.That(report.TotalTrackedHours, Is.EqualTo(totalTrackedHours));
            Assert.That(report.ProductiveHours, Is.EqualTo(productiveHours));
            Assert.That(report.Summary, Is.EqualTo(summary));
            Assert.That(report.Insights, Is.EqualTo(insights));
            Assert.That(report.Recommendations, Is.EqualTo(recommendations));
        });
    }

    [Test]
    public void GetProductivityPercentage_ReturnsCorrectPercentage()
    {
        // Arrange
        var report = new AuditReport
        {
            AuditReportId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Report",
            StartDate = DateTime.UtcNow.AddDays(-7),
            EndDate = DateTime.UtcNow,
            TotalTrackedHours = 100.0,
            ProductiveHours = 75.0
        };

        // Act
        var percentage = report.GetProductivityPercentage();

        // Assert
        Assert.That(percentage, Is.EqualTo(75.0));
    }

    [Test]
    public void GetProductivityPercentage_WhenTotalIsZero_ReturnsZero()
    {
        // Arrange
        var report = new AuditReport
        {
            AuditReportId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Report",
            StartDate = DateTime.UtcNow.AddDays(-7),
            EndDate = DateTime.UtcNow,
            TotalTrackedHours = 0.0,
            ProductiveHours = 0.0
        };

        // Act
        var percentage = report.GetProductivityPercentage();

        // Assert
        Assert.That(percentage, Is.EqualTo(0.0));
    }

    [Test]
    public void GetProductivityPercentage_With100PercentProductivity_Returns100()
    {
        // Arrange
        var report = new AuditReport
        {
            AuditReportId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Perfect Week",
            StartDate = DateTime.UtcNow.AddDays(-7),
            EndDate = DateTime.UtcNow,
            TotalTrackedHours = 50.0,
            ProductiveHours = 50.0
        };

        // Act
        var percentage = report.GetProductivityPercentage();

        // Assert
        Assert.That(percentage, Is.EqualTo(100.0));
    }

    [Test]
    public void IsCurrentWeek_WhenCurrentDateWithinRange_ReturnsTrue()
    {
        // Arrange
        var now = DateTime.UtcNow;
        var report = new AuditReport
        {
            AuditReportId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Current Week",
            StartDate = now.AddDays(-3),
            EndDate = now.AddDays(3),
            TotalTrackedHours = 50.0,
            ProductiveHours = 40.0
        };

        // Act
        var isCurrent = report.IsCurrentWeek();

        // Assert
        Assert.That(isCurrent, Is.True);
    }

    [Test]
    public void IsCurrentWeek_WhenCurrentDateBeforeRange_ReturnsFalse()
    {
        // Arrange
        var now = DateTime.UtcNow;
        var report = new AuditReport
        {
            AuditReportId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Future Week",
            StartDate = now.AddDays(10),
            EndDate = now.AddDays(17),
            TotalTrackedHours = 50.0,
            ProductiveHours = 40.0
        };

        // Act
        var isCurrent = report.IsCurrentWeek();

        // Assert
        Assert.That(isCurrent, Is.False);
    }

    [Test]
    public void IsCurrentWeek_WhenCurrentDateAfterRange_ReturnsFalse()
    {
        // Arrange
        var now = DateTime.UtcNow;
        var report = new AuditReport
        {
            AuditReportId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Past Week",
            StartDate = now.AddDays(-17),
            EndDate = now.AddDays(-10),
            TotalTrackedHours = 50.0,
            ProductiveHours = 40.0
        };

        // Act
        var isCurrent = report.IsCurrentWeek();

        // Assert
        Assert.That(isCurrent, Is.False);
    }

    [Test]
    public void GetPeriodDays_ReturnsCorrectNumberOfDays()
    {
        // Arrange
        var startDate = new DateTime(2024, 3, 11);
        var endDate = new DateTime(2024, 3, 17);

        var report = new AuditReport
        {
            AuditReportId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Week Report",
            StartDate = startDate,
            EndDate = endDate,
            TotalTrackedHours = 50.0,
            ProductiveHours = 40.0
        };

        // Act
        var days = report.GetPeriodDays();

        // Assert
        Assert.That(days, Is.EqualTo(7));
    }

    [Test]
    public void GetPeriodDays_ForSingleDay_Returns1()
    {
        // Arrange
        var date = new DateTime(2024, 3, 15);

        var report = new AuditReport
        {
            AuditReportId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Daily Report",
            StartDate = date,
            EndDate = date,
            TotalTrackedHours = 8.0,
            ProductiveHours = 6.0
        };

        // Act
        var days = report.GetPeriodDays();

        // Assert
        Assert.That(days, Is.EqualTo(1));
    }

    [Test]
    public void GetPeriodDays_ForMonthlyReport_ReturnsCorrectDays()
    {
        // Arrange
        var startDate = new DateTime(2024, 3, 1);
        var endDate = new DateTime(2024, 3, 31);

        var report = new AuditReport
        {
            AuditReportId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Monthly Report",
            StartDate = startDate,
            EndDate = endDate,
            TotalTrackedHours = 200.0,
            ProductiveHours = 150.0
        };

        // Act
        var days = report.GetPeriodDays();

        // Assert
        Assert.That(days, Is.EqualTo(31));
    }

    [Test]
    public void CreatedAt_DefaultsToUtcNow()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var report = new AuditReport
        {
            AuditReportId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Report",
            StartDate = DateTime.UtcNow.AddDays(-7),
            EndDate = DateTime.UtcNow,
            TotalTrackedHours = 50.0,
            ProductiveHours = 40.0
        };

        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(report.CreatedAt, Is.GreaterThanOrEqualTo(beforeCreation));
            Assert.That(report.CreatedAt, Is.LessThanOrEqualTo(afterCreation));
        });
    }

    [Test]
    public void AuditReport_WithOptionalFieldsNull_IsValid()
    {
        // Arrange & Act
        var report = new AuditReport
        {
            AuditReportId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Minimal Report",
            StartDate = DateTime.UtcNow.AddDays(-7),
            EndDate = DateTime.UtcNow,
            TotalTrackedHours = 50.0,
            ProductiveHours = 40.0
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(report.Summary, Is.Null);
            Assert.That(report.Insights, Is.Null);
            Assert.That(report.Recommendations, Is.Null);
        });
    }

    [Test]
    public void GetProductivityPercentage_WithPartialProductivity_ReturnsCorrectValue()
    {
        // Arrange
        var report = new AuditReport
        {
            AuditReportId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Report",
            StartDate = DateTime.UtcNow.AddDays(-7),
            EndDate = DateTime.UtcNow,
            TotalTrackedHours = 80.0,
            ProductiveHours = 20.0
        };

        // Act
        var percentage = report.GetProductivityPercentage();

        // Assert
        Assert.That(percentage, Is.EqualTo(25.0));
    }

    [Test]
    public void IsCurrentWeek_WhenTodayIsStartDate_ReturnsTrue()
    {
        // Arrange
        var now = DateTime.UtcNow;
        var report = new AuditReport
        {
            AuditReportId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Week Starting Today",
            StartDate = now,
            EndDate = now.AddDays(6),
            TotalTrackedHours = 50.0,
            ProductiveHours = 40.0
        };

        // Act
        var isCurrent = report.IsCurrentWeek();

        // Assert
        Assert.That(isCurrent, Is.True);
    }
}
