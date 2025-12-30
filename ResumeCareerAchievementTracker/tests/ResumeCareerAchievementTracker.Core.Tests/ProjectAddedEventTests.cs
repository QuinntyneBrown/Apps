// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ResumeCareerAchievementTracker.Core.Tests;

public class ProjectAddedEventTests
{
    [Test]
    public void Constructor_WithValidParameters_CreatesEvent()
    {
        var projectId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var name = "Test Project";
        var startDate = new DateTime(2024, 1, 1);

        var eventData = new ProjectAddedEvent
        {
            ProjectId = projectId,
            UserId = userId,
            Name = name,
            StartDate = startDate
        };

        Assert.Multiple(() =>
        {
            Assert.That(eventData.ProjectId, Is.EqualTo(projectId));
            Assert.That(eventData.UserId, Is.EqualTo(userId));
            Assert.That(eventData.Name, Is.EqualTo(name));
            Assert.That(eventData.StartDate, Is.EqualTo(startDate));
            Assert.That(eventData.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Record_Equality_WorksCorrectly()
    {
        var projectId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new ProjectAddedEvent { ProjectId = projectId, UserId = userId, Name = "Test", StartDate = DateTime.Today, Timestamp = timestamp };
        var event2 = new ProjectAddedEvent { ProjectId = projectId, UserId = userId, Name = "Test", StartDate = DateTime.Today, Timestamp = timestamp };

        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Record_Inequality_WorksCorrectly()
    {
        var event1 = new ProjectAddedEvent { ProjectId = Guid.NewGuid(), UserId = Guid.NewGuid(), Name = "Project 1", StartDate = DateTime.Today };
        var event2 = new ProjectAddedEvent { ProjectId = Guid.NewGuid(), UserId = Guid.NewGuid(), Name = "Project 2", StartDate = DateTime.Today.AddDays(1) };

        Assert.That(event1, Is.Not.EqualTo(event2));
    }
}
