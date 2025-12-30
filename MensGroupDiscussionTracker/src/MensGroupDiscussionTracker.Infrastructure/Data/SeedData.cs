// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MensGroupDiscussionTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MensGroupDiscussionTracker.Infrastructure;

/// <summary>
/// Provides seed data for the MensGroupDiscussionTracker database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(MensGroupDiscussionTrackerContext context, ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Groups.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedGroupsAsync(context);
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

    private static async Task SeedGroupsAsync(MensGroupDiscussionTrackerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        // Create groups
        var groups = new List<Group>
        {
            new Group
            {
                GroupId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                CreatedByUserId = sampleUserId,
                Name = "Leadership & Growth",
                Description = "A group focused on personal development and leadership skills",
                MeetingSchedule = "Weekly on Thursdays at 7:00 PM",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Group
            {
                GroupId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                CreatedByUserId = sampleUserId,
                Name = "Faith & Purpose",
                Description = "Exploring faith, purpose, and spiritual growth",
                MeetingSchedule = "Bi-weekly on Sundays at 6:00 PM",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Groups.AddRange(groups);

        // Create meetings
        var meetings = new List<Meeting>
        {
            new Meeting
            {
                MeetingId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                GroupId = groups[0].GroupId,
                Title = "Building Authentic Relationships",
                MeetingDateTime = DateTime.UtcNow.AddDays(-7),
                Location = "Community Center",
                Notes = "Great discussion on the importance of vulnerability and trust",
                AttendeeCount = 12,
                CreatedAt = DateTime.UtcNow.AddDays(-7),
            },
            new Meeting
            {
                MeetingId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                GroupId = groups[0].GroupId,
                Title = "Work-Life Balance",
                MeetingDateTime = DateTime.UtcNow.AddDays(7),
                Location = "Online - Zoom",
                AttendeeCount = 0,
                CreatedAt = DateTime.UtcNow,
            },
            new Meeting
            {
                MeetingId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                GroupId = groups[1].GroupId,
                Title = "Finding Your Purpose",
                MeetingDateTime = DateTime.UtcNow.AddDays(-14),
                Location = "Church Hall",
                Notes = "Powerful testimonies shared by members",
                AttendeeCount = 15,
                CreatedAt = DateTime.UtcNow.AddDays(-14),
            },
        };

        context.Meetings.AddRange(meetings);

        // Create topics
        var topics = new List<Topic>
        {
            new Topic
            {
                TopicId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                MeetingId = meetings[0].MeetingId,
                UserId = sampleUserId,
                Title = "Overcoming Fear in Relationships",
                Description = "How to be vulnerable and authentic with others",
                Category = TopicCategory.RelationshipsAndFamily,
                DiscussionNotes = "Key insight: Trust is built through consistent small actions",
                CreatedAt = DateTime.UtcNow.AddDays(-7),
            },
            new Topic
            {
                TopicId = Guid.Parse("55555555-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                MeetingId = meetings[2].MeetingId,
                UserId = sampleUserId,
                Title = "Serving Others as Purpose",
                Description = "How serving others can help us discover our purpose",
                Category = TopicCategory.FaithAndSpirituality,
                DiscussionNotes = "Many shared stories of finding fulfillment through service",
                CreatedAt = DateTime.UtcNow.AddDays(-14),
            },
            new Topic
            {
                TopicId = Guid.Parse("66666666-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Title = "Mental Health Awareness",
                Description = "Breaking the stigma around men's mental health",
                Category = TopicCategory.MentalHealth,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Topics.AddRange(topics);

        // Create resources
        var resources = new List<Resource>
        {
            new Resource
            {
                ResourceId = Guid.Parse("77777777-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                GroupId = groups[0].GroupId,
                SharedByUserId = sampleUserId,
                Title = "The Way of the Superior Man",
                Description = "Book on masculine spirituality and purpose",
                Url = "https://example.com/book",
                ResourceType = "Book",
                CreatedAt = DateTime.UtcNow,
            },
            new Resource
            {
                ResourceId = Guid.Parse("88888888-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                GroupId = groups[1].GroupId,
                SharedByUserId = sampleUserId,
                Title = "Purpose-Driven Life Podcast",
                Description = "Weekly podcast on finding purpose",
                Url = "https://example.com/podcast",
                ResourceType = "Podcast",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Resources.AddRange(resources);

        await context.SaveChangesAsync();
    }
}
