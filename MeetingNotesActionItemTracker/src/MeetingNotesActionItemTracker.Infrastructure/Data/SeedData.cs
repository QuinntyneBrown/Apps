// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MeetingNotesActionItemTracker.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using MeetingNotesActionItemTracker.Core.Model.UserAggregate;
using MeetingNotesActionItemTracker.Core.Model.UserAggregate.Entities;
using MeetingNotesActionItemTracker.Core.Services;
namespace MeetingNotesActionItemTracker.Infrastructure;

/// <summary>
/// Provides seed data for the MeetingNotesActionItemTracker database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(MeetingNotesActionItemTrackerContext context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Meetings.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedMeetingsAsync(context);
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

    private static async Task SeedMeetingsAsync(MeetingNotesActionItemTrackerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        // Create meetings
        var meetings = new List<Meeting>
        {
            new Meeting
            {
                MeetingId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Title = "Q1 Planning Session",
                MeetingDateTime = DateTime.UtcNow.AddDays(-7),
                DurationMinutes = 90,
                Location = "Conference Room A",
                Attendees = new List<string> { "John Smith", "Jane Doe", "Bob Johnson" },
                Agenda = "1. Review Q4 results\n2. Plan Q1 objectives\n3. Resource allocation",
                Summary = "Discussed Q1 goals and assigned key initiatives to team members.",
                CreatedAt = DateTime.UtcNow.AddDays(-7),
            },
            new Meeting
            {
                MeetingId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Title = "Weekly Team Sync",
                MeetingDateTime = DateTime.UtcNow.AddDays(-2),
                DurationMinutes = 30,
                Location = "https://meet.example.com/team-sync",
                Attendees = new List<string> { "Alice Brown", "Charlie Wilson" },
                Agenda = "Updates on ongoing projects",
                Summary = "Team progress update - all projects on track.",
                CreatedAt = DateTime.UtcNow.AddDays(-2),
            },
        };

        context.Meetings.AddRange(meetings);

        // Create notes
        var notes = new List<Note>
        {
            new Note
            {
                NoteId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                MeetingId = meetings[0].MeetingId,
                Content = "Focus on customer acquisition in Q1",
                Category = "Strategic",
                IsImportant = true,
                CreatedAt = DateTime.UtcNow.AddDays(-7),
            },
            new Note
            {
                NoteId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                MeetingId = meetings[0].MeetingId,
                Content = "Budget approved for new marketing campaigns",
                Category = "Financial",
                IsImportant = true,
                CreatedAt = DateTime.UtcNow.AddDays(-7),
            },
            new Note
            {
                NoteId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                MeetingId = meetings[1].MeetingId,
                Content = "Project A is ahead of schedule",
                Category = "Progress",
                IsImportant = false,
                CreatedAt = DateTime.UtcNow.AddDays(-2),
            },
        };

        context.Notes.AddRange(notes);

        // Create action items
        var actionItems = new List<ActionItem>
        {
            new ActionItem
            {
                ActionItemId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                MeetingId = meetings[0].MeetingId,
                Description = "Prepare marketing plan for Q1",
                ResponsiblePerson = "Jane Doe",
                DueDate = DateTime.UtcNow.AddDays(14),
                Priority = Priority.High,
                Status = ActionItemStatus.InProgress,
                Notes = "Include digital and traditional channels",
                CreatedAt = DateTime.UtcNow.AddDays(-7),
            },
            new ActionItem
            {
                ActionItemId = Guid.Parse("55555555-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                MeetingId = meetings[0].MeetingId,
                Description = "Schedule customer feedback sessions",
                ResponsiblePerson = "John Smith",
                DueDate = DateTime.UtcNow.AddDays(7),
                Priority = Priority.Medium,
                Status = ActionItemStatus.NotStarted,
                CreatedAt = DateTime.UtcNow.AddDays(-7),
            },
            new ActionItem
            {
                ActionItemId = Guid.Parse("66666666-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                MeetingId = meetings[1].MeetingId,
                Description = "Update project documentation",
                ResponsiblePerson = "Alice Brown",
                DueDate = DateTime.UtcNow.AddDays(3),
                Priority = Priority.Low,
                Status = ActionItemStatus.NotStarted,
                CreatedAt = DateTime.UtcNow.AddDays(-2),
            },
        };

        context.ActionItems.AddRange(actionItems);

        await context.SaveChangesAsync();
    }
}
