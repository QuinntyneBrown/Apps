// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace KidsActivitySportsTracker.Api.Tests;

/// <summary>
/// Tests for DTO mapping extensions.
/// </summary>
[TestFixture]
public class DtoMappingTests
{
    [Test]
    public void ActivityToDto_MapsAllProperties()
    {
        // Arrange
        var activity = new Core.Activity
        {
            ActivityId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ChildName = "John Doe",
            Name = "Soccer",
            ActivityType = ActivityType.TeamSports,
            Organization = "Youth Soccer League",
            CoachName = "Coach Smith",
            CoachContact = "coach@example.com",
            Season = "Spring 2024",
            StartDate = new DateTime(2024, 3, 1),
            EndDate = new DateTime(2024, 6, 1),
            Notes = "Practice every Tuesday",
            CreatedAt = DateTime.UtcNow
        };

        // Act
        var dto = activity.ToDto();

        // Assert
        Assert.That(dto.ActivityId, Is.EqualTo(activity.ActivityId));
        Assert.That(dto.UserId, Is.EqualTo(activity.UserId));
        Assert.That(dto.ChildName, Is.EqualTo(activity.ChildName));
        Assert.That(dto.Name, Is.EqualTo(activity.Name));
        Assert.That(dto.ActivityType, Is.EqualTo(activity.ActivityType));
        Assert.That(dto.Organization, Is.EqualTo(activity.Organization));
        Assert.That(dto.CoachName, Is.EqualTo(activity.CoachName));
        Assert.That(dto.CoachContact, Is.EqualTo(activity.CoachContact));
        Assert.That(dto.Season, Is.EqualTo(activity.Season));
        Assert.That(dto.StartDate, Is.EqualTo(activity.StartDate));
        Assert.That(dto.EndDate, Is.EqualTo(activity.EndDate));
        Assert.That(dto.Notes, Is.EqualTo(activity.Notes));
        Assert.That(dto.CreatedAt, Is.EqualTo(activity.CreatedAt));
    }

    [Test]
    public void ScheduleToDto_MapsAllProperties()
    {
        // Arrange
        var schedule = new Core.Schedule
        {
            ScheduleId = Guid.NewGuid(),
            ActivityId = Guid.NewGuid(),
            EventType = "Practice",
            DateTime = new DateTime(2024, 3, 15, 14, 0, 0),
            Location = "City Park Field 3",
            DurationMinutes = 90,
            Notes = "Bring water",
            IsConfirmed = true,
            CreatedAt = DateTime.UtcNow
        };

        // Act
        var dto = schedule.ToDto();

        // Assert
        Assert.That(dto.ScheduleId, Is.EqualTo(schedule.ScheduleId));
        Assert.That(dto.ActivityId, Is.EqualTo(schedule.ActivityId));
        Assert.That(dto.EventType, Is.EqualTo(schedule.EventType));
        Assert.That(dto.DateTime, Is.EqualTo(schedule.DateTime));
        Assert.That(dto.Location, Is.EqualTo(schedule.Location));
        Assert.That(dto.DurationMinutes, Is.EqualTo(schedule.DurationMinutes));
        Assert.That(dto.Notes, Is.EqualTo(schedule.Notes));
        Assert.That(dto.IsConfirmed, Is.EqualTo(schedule.IsConfirmed));
        Assert.That(dto.CreatedAt, Is.EqualTo(schedule.CreatedAt));
    }

    [Test]
    public void CarpoolToDto_MapsAllProperties()
    {
        // Arrange
        var carpool = new Core.Carpool
        {
            CarpoolId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Tuesday Practice Carpool",
            DriverName = "Jane Smith",
            DriverContact = "555-1234",
            PickupTime = new DateTime(2024, 3, 15, 13, 30, 0),
            PickupLocation = "123 Main St",
            DropoffTime = new DateTime(2024, 3, 15, 16, 0, 0),
            DropoffLocation = "City Park",
            Participants = "John, Sarah, Mike",
            Notes = "Call before pickup",
            CreatedAt = DateTime.UtcNow
        };

        // Act
        var dto = carpool.ToDto();

        // Assert
        Assert.That(dto.CarpoolId, Is.EqualTo(carpool.CarpoolId));
        Assert.That(dto.UserId, Is.EqualTo(carpool.UserId));
        Assert.That(dto.Name, Is.EqualTo(carpool.Name));
        Assert.That(dto.DriverName, Is.EqualTo(carpool.DriverName));
        Assert.That(dto.DriverContact, Is.EqualTo(carpool.DriverContact));
        Assert.That(dto.PickupTime, Is.EqualTo(carpool.PickupTime));
        Assert.That(dto.PickupLocation, Is.EqualTo(carpool.PickupLocation));
        Assert.That(dto.DropoffTime, Is.EqualTo(carpool.DropoffTime));
        Assert.That(dto.DropoffLocation, Is.EqualTo(carpool.DropoffLocation));
        Assert.That(dto.Participants, Is.EqualTo(carpool.Participants));
        Assert.That(dto.Notes, Is.EqualTo(carpool.Notes));
        Assert.That(dto.CreatedAt, Is.EqualTo(carpool.CreatedAt));
    }
}
