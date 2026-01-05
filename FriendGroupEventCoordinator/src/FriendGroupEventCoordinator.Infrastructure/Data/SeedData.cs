// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FriendGroupEventCoordinator.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using FriendGroupEventCoordinator.Core.Models.UserAggregate;
using FriendGroupEventCoordinator.Core.Models.UserAggregate.Entities;
using FriendGroupEventCoordinator.Core.Services;
namespace FriendGroupEventCoordinator.Infrastructure;

/// <summary>
/// Provides seed data for the FriendGroupEventCoordinator database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(FriendGroupEventCoordinatorContext context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Groups.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedGroupsAndEventsAsync(context);
                logger.LogInformation("Initial data seeded successfully.");
            }
            else
            {
                logger.LogInformation("Database already contains data. Skipping seed.");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private static async Task SeedGroupsAndEventsAsync(FriendGroupEventCoordinatorContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var user2Id = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var user3Id = Guid.Parse("33333333-3333-3333-3333-333333333333");

        var groups = new List<Group>
        {
            new Group
            {
                GroupId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                CreatedByUserId = sampleUserId,
                Name = "Weekend Warriors",
                Description = "Friends who love weekend adventures",
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-90),
            },
            new Group
            {
                GroupId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                CreatedByUserId = sampleUserId,
                Name = "Book Club",
                Description = "Monthly book discussion group",
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-60),
            },
        };

        context.Groups.AddRange(groups);

        var members = new List<Member>
        {
            new Member
            {
                MemberId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                GroupId = groups[0].GroupId,
                UserId = sampleUserId,
                Name = "John Smith",
                Email = "john@example.com",
                IsAdmin = true,
                IsActive = true,
                JoinedAt = DateTime.UtcNow.AddDays(-90),
                CreatedAt = DateTime.UtcNow.AddDays(-90),
            },
            new Member
            {
                MemberId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                GroupId = groups[0].GroupId,
                UserId = user2Id,
                Name = "Jane Doe",
                Email = "jane@example.com",
                IsAdmin = false,
                IsActive = true,
                JoinedAt = DateTime.UtcNow.AddDays(-80),
                CreatedAt = DateTime.UtcNow.AddDays(-80),
            },
            new Member
            {
                MemberId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                GroupId = groups[0].GroupId,
                UserId = user3Id,
                Name = "Bob Johnson",
                Email = "bob@example.com",
                IsAdmin = false,
                IsActive = true,
                JoinedAt = DateTime.UtcNow.AddDays(-70),
                CreatedAt = DateTime.UtcNow.AddDays(-70),
            },
            new Member
            {
                MemberId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                GroupId = groups[1].GroupId,
                UserId = sampleUserId,
                Name = "John Smith",
                Email = "john@example.com",
                IsAdmin = true,
                IsActive = true,
                JoinedAt = DateTime.UtcNow.AddDays(-60),
                CreatedAt = DateTime.UtcNow.AddDays(-60),
            },
        };

        context.Members.AddRange(members);

        var events = new List<Event>
        {
            new Event
            {
                EventId = Guid.Parse("55555555-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                GroupId = groups[0].GroupId,
                CreatedByUserId = sampleUserId,
                Title = "Hiking at Mountain Trail",
                Description = "Group hike on the scenic mountain trail",
                EventType = EventType.Outdoor,
                StartDateTime = DateTime.UtcNow.AddDays(14).AddHours(8),
                EndDateTime = DateTime.UtcNow.AddDays(14).AddHours(16),
                Location = "Mountain Trail Parking Lot, Trailhead Rd",
                MaxAttendees = 10,
                IsCancelled = false,
                CreatedAt = DateTime.UtcNow.AddDays(-7),
            },
            new Event
            {
                EventId = Guid.Parse("66666666-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                GroupId = groups[1].GroupId,
                CreatedByUserId = sampleUserId,
                Title = "Monthly Book Discussion",
                Description = "Discussing 'The Great Gatsby'",
                EventType = EventType.Social,
                StartDateTime = DateTime.UtcNow.AddDays(7).AddHours(19),
                EndDateTime = DateTime.UtcNow.AddDays(7).AddHours(21),
                Location = "Coffee House Downtown",
                MaxAttendees = null,
                IsCancelled = false,
                CreatedAt = DateTime.UtcNow.AddDays(-14),
            },
        };

        context.Events.AddRange(events);

        var rsvps = new List<RSVP>
        {
            new RSVP
            {
                RSVPId = Guid.Parse("77777777-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                EventId = events[0].EventId,
                MemberId = members[0].MemberId,
                UserId = sampleUserId,
                Response = RSVPResponse.Yes,
                AdditionalGuests = 0,
                Notes = "Looking forward to it!",
                CreatedAt = DateTime.UtcNow.AddDays(-6),
            },
            new RSVP
            {
                RSVPId = Guid.Parse("88888888-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                EventId = events[0].EventId,
                MemberId = members[1].MemberId,
                UserId = user2Id,
                Response = RSVPResponse.Yes,
                AdditionalGuests = 1,
                Notes = "Bringing my partner",
                CreatedAt = DateTime.UtcNow.AddDays(-5),
            },
            new RSVP
            {
                RSVPId = Guid.Parse("99999999-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                EventId = events[0].EventId,
                MemberId = members[2].MemberId,
                UserId = user3Id,
                Response = RSVPResponse.Maybe,
                AdditionalGuests = 0,
                Notes = "Need to check my schedule",
                CreatedAt = DateTime.UtcNow.AddDays(-4),
            },
        };

        context.RSVPs.AddRange(rsvps);

        await context.SaveChangesAsync();
    }
}
