// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace KidsActivitySportsTracker.Core.Tests;

public class ActivityTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesActivity()
    {
        // Arrange
        var activityId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var childName = "John Doe";
        var name = "Soccer Practice";
        var activityType = ActivityType.TeamSports;

        // Act
        var activity = new Activity
        {
            ActivityId = activityId,
            UserId = userId,
            ChildName = childName,
            Name = name,
            ActivityType = activityType
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(activity.ActivityId, Is.EqualTo(activityId));
            Assert.That(activity.UserId, Is.EqualTo(userId));
            Assert.That(activity.ChildName, Is.EqualTo(childName));
            Assert.That(activity.Name, Is.EqualTo(name));
            Assert.That(activity.ActivityType, Is.EqualTo(activityType));
            Assert.That(activity.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void ActivityId_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var activity = new Activity();
        var expectedId = Guid.NewGuid();

        // Act
        activity.ActivityId = expectedId;

        // Assert
        Assert.That(activity.ActivityId, Is.EqualTo(expectedId));
    }

    [Test]
    public void UserId_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var activity = new Activity();
        var expectedUserId = Guid.NewGuid();

        // Act
        activity.UserId = expectedUserId;

        // Assert
        Assert.That(activity.UserId, Is.EqualTo(expectedUserId));
    }

    [Test]
    public void ChildName_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var activity = new Activity();
        var expectedChildName = "Jane Smith";

        // Act
        activity.ChildName = expectedChildName;

        // Assert
        Assert.That(activity.ChildName, Is.EqualTo(expectedChildName));
    }

    [Test]
    public void Name_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var activity = new Activity();
        var expectedName = "Basketball Team";

        // Act
        activity.Name = expectedName;

        // Assert
        Assert.That(activity.Name, Is.EqualTo(expectedName));
    }

    [Test]
    public void ActivityType_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var activity = new Activity();
        var expectedType = ActivityType.Music;

        // Act
        activity.ActivityType = expectedType;

        // Assert
        Assert.That(activity.ActivityType, Is.EqualTo(expectedType));
    }

    [Test]
    public void Organization_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var activity = new Activity();
        var expectedOrganization = "Youth Soccer League";

        // Act
        activity.Organization = expectedOrganization;

        // Assert
        Assert.That(activity.Organization, Is.EqualTo(expectedOrganization));
    }

    [Test]
    public void CoachName_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var activity = new Activity();
        var expectedCoachName = "Coach Johnson";

        // Act
        activity.CoachName = expectedCoachName;

        // Assert
        Assert.That(activity.CoachName, Is.EqualTo(expectedCoachName));
    }

    [Test]
    public void CoachContact_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var activity = new Activity();
        var expectedContact = "coach@example.com";

        // Act
        activity.CoachContact = expectedContact;

        // Assert
        Assert.That(activity.CoachContact, Is.EqualTo(expectedContact));
    }

    [Test]
    public void Season_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var activity = new Activity();
        var expectedSeason = "Fall 2024";

        // Act
        activity.Season = expectedSeason;

        // Assert
        Assert.That(activity.Season, Is.EqualTo(expectedSeason));
    }

    [Test]
    public void StartDate_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var activity = new Activity();
        var expectedStartDate = DateTime.UtcNow.AddDays(7);

        // Act
        activity.StartDate = expectedStartDate;

        // Assert
        Assert.That(activity.StartDate, Is.EqualTo(expectedStartDate));
    }

    [Test]
    public void EndDate_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var activity = new Activity();
        var expectedEndDate = DateTime.UtcNow.AddMonths(3);

        // Act
        activity.EndDate = expectedEndDate;

        // Assert
        Assert.That(activity.EndDate, Is.EqualTo(expectedEndDate));
    }

    [Test]
    public void Notes_CanBeSet_ReturnsCorrectValue()
    {
        // Arrange
        var activity = new Activity();
        var expectedNotes = "Bring water bottle and cleats";

        // Act
        activity.Notes = expectedNotes;

        // Assert
        Assert.That(activity.Notes, Is.EqualTo(expectedNotes));
    }

    [Test]
    public void Schedules_DefaultsToEmptyCollection()
    {
        // Arrange & Act
        var activity = new Activity();

        // Assert
        Assert.That(activity.Schedules, Is.Not.Null);
        Assert.That(activity.Schedules, Is.Empty);
    }

    [Test]
    public void Schedules_CanAddSchedules_ReturnsCorrectCount()
    {
        // Arrange
        var activity = new Activity();
        var schedule = new Schedule { ScheduleId = Guid.NewGuid() };

        // Act
        activity.Schedules.Add(schedule);

        // Assert
        Assert.That(activity.Schedules.Count, Is.EqualTo(1));
        Assert.That(activity.Schedules.First(), Is.EqualTo(schedule));
    }
}
