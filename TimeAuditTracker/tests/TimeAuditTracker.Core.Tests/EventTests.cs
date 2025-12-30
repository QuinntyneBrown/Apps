// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace TimeAuditTracker.Core.Tests;

public class EventTests
{
    [Test]
    public void TimeBlockStartedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var timeBlockId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var category = ActivityCategory.Work;
        var description = "Working";
        var startTime = DateTime.UtcNow;
        var timestamp = DateTime.UtcNow;

        // Act
        var eventData = new TimeBlockStartedEvent
        {
            TimeBlockId = timeBlockId,
            UserId = userId,
            Category = category,
            Description = description,
            StartTime = startTime,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.TimeBlockId, Is.EqualTo(timeBlockId));
            Assert.That(eventData.UserId, Is.EqualTo(userId));
            Assert.That(eventData.Category, Is.EqualTo(category));
            Assert.That(eventData.Description, Is.EqualTo(description));
            Assert.That(eventData.StartTime, Is.EqualTo(startTime));
            Assert.That(eventData.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void TimeBlockEndedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var timeBlockId = Guid.NewGuid();
        var endTime = DateTime.UtcNow;
        var durationInMinutes = 90.0;
        var timestamp = DateTime.UtcNow;

        // Act
        var eventData = new TimeBlockEndedEvent
        {
            TimeBlockId = timeBlockId,
            EndTime = endTime,
            DurationInMinutes = durationInMinutes,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.TimeBlockId, Is.EqualTo(timeBlockId));
            Assert.That(eventData.EndTime, Is.EqualTo(endTime));
            Assert.That(eventData.DurationInMinutes, Is.EqualTo(durationInMinutes));
            Assert.That(eventData.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void GoalCreatedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var goalId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var category = ActivityCategory.Exercise;
        var targetHoursPerWeek = 10.0;
        var description = "Exercise goal";
        var timestamp = DateTime.UtcNow;

        // Act
        var eventData = new GoalCreatedEvent
        {
            GoalId = goalId,
            UserId = userId,
            Category = category,
            TargetHoursPerWeek = targetHoursPerWeek,
            Description = description,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.GoalId, Is.EqualTo(goalId));
            Assert.That(eventData.UserId, Is.EqualTo(userId));
            Assert.That(eventData.Category, Is.EqualTo(category));
            Assert.That(eventData.TargetHoursPerWeek, Is.EqualTo(targetHoursPerWeek));
            Assert.That(eventData.Description, Is.EqualTo(description));
            Assert.That(eventData.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void GoalAchievedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var goalId = Guid.NewGuid();
        var actualHours = 12.0;
        var targetHours = 10.0;
        var timestamp = DateTime.UtcNow;

        // Act
        var eventData = new GoalAchievedEvent
        {
            GoalId = goalId,
            ActualHours = actualHours,
            TargetHours = targetHours,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.GoalId, Is.EqualTo(goalId));
            Assert.That(eventData.ActualHours, Is.EqualTo(actualHours));
            Assert.That(eventData.TargetHours, Is.EqualTo(targetHours));
            Assert.That(eventData.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void AuditReportGeneratedEvent_ValidParameters_CreatesEvent()
    {
        // Arrange
        var auditReportId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var title = "Weekly Report";
        var startDate = DateTime.UtcNow.AddDays(-7);
        var endDate = DateTime.UtcNow;
        var totalTrackedHours = 50.0;
        var timestamp = DateTime.UtcNow;

        // Act
        var eventData = new AuditReportGeneratedEvent
        {
            AuditReportId = auditReportId,
            UserId = userId,
            Title = title,
            StartDate = startDate,
            EndDate = endDate,
            TotalTrackedHours = totalTrackedHours,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.AuditReportId, Is.EqualTo(auditReportId));
            Assert.That(eventData.UserId, Is.EqualTo(userId));
            Assert.That(eventData.Title, Is.EqualTo(title));
            Assert.That(eventData.StartDate, Is.EqualTo(startDate));
            Assert.That(eventData.EndDate, Is.EqualTo(endDate));
            Assert.That(eventData.TotalTrackedHours, Is.EqualTo(totalTrackedHours));
            Assert.That(eventData.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void TimeBlockStartedEvent_Timestamp_DefaultsToUtcNow()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var eventData = new TimeBlockStartedEvent
        {
            TimeBlockId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Category = ActivityCategory.Work,
            Description = "Test",
            StartTime = DateTime.UtcNow
        };

        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation));
            Assert.That(eventData.Timestamp, Is.LessThanOrEqualTo(afterCreation));
        });
    }

    [Test]
    public void TimeBlockEndedEvent_Timestamp_DefaultsToUtcNow()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var eventData = new TimeBlockEndedEvent
        {
            TimeBlockId = Guid.NewGuid(),
            EndTime = DateTime.UtcNow,
            DurationInMinutes = 60.0
        };

        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation));
            Assert.That(eventData.Timestamp, Is.LessThanOrEqualTo(afterCreation));
        });
    }

    [Test]
    public void GoalCreatedEvent_Timestamp_DefaultsToUtcNow()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var eventData = new GoalCreatedEvent
        {
            GoalId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Category = ActivityCategory.Exercise,
            TargetHoursPerWeek = 10.0,
            Description = "Test"
        };

        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation));
            Assert.That(eventData.Timestamp, Is.LessThanOrEqualTo(afterCreation));
        });
    }

    [Test]
    public void GoalAchievedEvent_Timestamp_DefaultsToUtcNow()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var eventData = new GoalAchievedEvent
        {
            GoalId = Guid.NewGuid(),
            ActualHours = 12.0,
            TargetHours = 10.0
        };

        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation));
            Assert.That(eventData.Timestamp, Is.LessThanOrEqualTo(afterCreation));
        });
    }

    [Test]
    public void AuditReportGeneratedEvent_Timestamp_DefaultsToUtcNow()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var eventData = new AuditReportGeneratedEvent
        {
            AuditReportId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test",
            StartDate = DateTime.UtcNow.AddDays(-7),
            EndDate = DateTime.UtcNow,
            TotalTrackedHours = 50.0
        };

        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventData.Timestamp, Is.GreaterThanOrEqualTo(beforeCreation));
            Assert.That(eventData.Timestamp, Is.LessThanOrEqualTo(afterCreation));
        });
    }

    [Test]
    public void Events_AreRecords_SupportValueEquality()
    {
        // Arrange
        var timeBlockId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new TimeBlockStartedEvent
        {
            TimeBlockId = timeBlockId,
            UserId = userId,
            Category = ActivityCategory.Work,
            Description = "Test",
            StartTime = timestamp,
            Timestamp = timestamp
        };

        var event2 = new TimeBlockStartedEvent
        {
            TimeBlockId = timeBlockId,
            UserId = userId,
            Category = ActivityCategory.Work,
            Description = "Test",
            StartTime = timestamp,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(event1, Is.EqualTo(event2));
    }
}
