// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ResumeCareerAchievementTracker.Core.Tests;

public class ProjectCompletedEventTests
{
    [Test]
    public void Constructor_WithValidParameters_CreatesEvent()
    {
        var projectId = Guid.NewGuid();
        var endDate = new DateTime(2024, 6, 30);

        var eventData = new ProjectCompletedEvent
        {
            ProjectId = projectId,
            EndDate = endDate
        };

        Assert.Multiple(() =>
        {
            Assert.That(eventData.ProjectId, Is.EqualTo(projectId));
            Assert.That(eventData.EndDate, Is.EqualTo(endDate));
            Assert.That(eventData.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Record_Equality_WorksCorrectly()
    {
        var projectId = Guid.NewGuid();
        var endDate = DateTime.Today;
        var timestamp = DateTime.UtcNow;

        var event1 = new ProjectCompletedEvent { ProjectId = projectId, EndDate = endDate, Timestamp = timestamp };
        var event2 = new ProjectCompletedEvent { ProjectId = projectId, EndDate = endDate, Timestamp = timestamp };

        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Record_Inequality_WorksCorrectly()
    {
        var event1 = new ProjectCompletedEvent { ProjectId = Guid.NewGuid(), EndDate = DateTime.Today };
        var event2 = new ProjectCompletedEvent { ProjectId = Guid.NewGuid(), EndDate = DateTime.Today.AddDays(1) };

        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
