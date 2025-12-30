// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace KidsActivitySportsTracker.Core.Tests;

public class ActivityTypeTests
{
    [Test]
    public void ActivityType_TeamSports_HasCorrectValue()
    {
        // Arrange & Act
        var activityType = ActivityType.TeamSports;

        // Assert
        Assert.That((int)activityType, Is.EqualTo(0));
    }

    [Test]
    public void ActivityType_IndividualSports_HasCorrectValue()
    {
        // Arrange & Act
        var activityType = ActivityType.IndividualSports;

        // Assert
        Assert.That((int)activityType, Is.EqualTo(1));
    }

    [Test]
    public void ActivityType_Music_HasCorrectValue()
    {
        // Arrange & Act
        var activityType = ActivityType.Music;

        // Assert
        Assert.That((int)activityType, Is.EqualTo(2));
    }

    [Test]
    public void ActivityType_Art_HasCorrectValue()
    {
        // Arrange & Act
        var activityType = ActivityType.Art;

        // Assert
        Assert.That((int)activityType, Is.EqualTo(3));
    }

    [Test]
    public void ActivityType_Academic_HasCorrectValue()
    {
        // Arrange & Act
        var activityType = ActivityType.Academic;

        // Assert
        Assert.That((int)activityType, Is.EqualTo(4));
    }

    [Test]
    public void ActivityType_Dance_HasCorrectValue()
    {
        // Arrange & Act
        var activityType = ActivityType.Dance;

        // Assert
        Assert.That((int)activityType, Is.EqualTo(5));
    }

    [Test]
    public void ActivityType_Other_HasCorrectValue()
    {
        // Arrange & Act
        var activityType = ActivityType.Other;

        // Assert
        Assert.That((int)activityType, Is.EqualTo(6));
    }

    [Test]
    public void ActivityType_CanBeAssignedToActivity()
    {
        // Arrange
        var activity = new Activity();

        // Act
        activity.ActivityType = ActivityType.Music;

        // Assert
        Assert.That(activity.ActivityType, Is.EqualTo(ActivityType.Music));
    }

    [Test]
    public void ActivityType_AllValuesCanBeAssigned()
    {
        // Arrange
        var activity = new Activity();
        var allTypes = new[]
        {
            ActivityType.TeamSports,
            ActivityType.IndividualSports,
            ActivityType.Music,
            ActivityType.Art,
            ActivityType.Academic,
            ActivityType.Dance,
            ActivityType.Other
        };

        // Act & Assert
        foreach (var type in allTypes)
        {
            activity.ActivityType = type;
            Assert.That(activity.ActivityType, Is.EqualTo(type));
        }
    }
}
