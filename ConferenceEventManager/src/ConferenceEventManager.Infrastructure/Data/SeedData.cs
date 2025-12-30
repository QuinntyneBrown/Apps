// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConferenceEventManager.Core;

namespace ConferenceEventManager.Infrastructure.Data;

/// <summary>
/// Provides seed data for the ConferenceEventManager system.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Gets sample events for seeding.
    /// </summary>
    /// <param name="userId">The user ID to associate with the data.</param>
    /// <returns>A collection of sample events.</returns>
    public static IEnumerable<Event> GetSampleEvents(Guid userId)
    {
        return new List<Event>
        {
            new()
            {
                EventId = Guid.NewGuid(),
                UserId = userId,
                Name = "Microsoft Build 2024",
                EventType = EventType.Conference,
                StartDate = DateTime.UtcNow.AddMonths(2),
                EndDate = DateTime.UtcNow.AddMonths(2).AddDays(3),
                Location = "Seattle, WA",
                IsVirtual = false,
                Website = "https://build.microsoft.com",
                RegistrationFee = 2495.00m,
                IsRegistered = true,
                DidAttend = false,
                Notes = "Annual developer conference by Microsoft",
                CreatedAt = DateTime.UtcNow.AddDays(-60)
            },
            new()
            {
                EventId = Guid.NewGuid(),
                UserId = userId,
                Name = "AWS re:Invent",
                EventType = EventType.Conference,
                StartDate = DateTime.UtcNow.AddMonths(3),
                EndDate = DateTime.UtcNow.AddMonths(3).AddDays(5),
                Location = "Las Vegas, NV",
                IsVirtual = false,
                Website = "https://reinvent.awsevents.com",
                RegistrationFee = 1799.00m,
                IsRegistered = false,
                DidAttend = false,
                Notes = "AWS's flagship cloud computing conference",
                CreatedAt = DateTime.UtcNow.AddDays(-45)
            },
            new()
            {
                EventId = Guid.NewGuid(),
                UserId = userId,
                Name = "Local Tech Meetup",
                EventType = EventType.Meetup,
                StartDate = DateTime.UtcNow.AddDays(-7),
                EndDate = DateTime.UtcNow.AddDays(-7).AddHours(2),
                Location = "Downtown Community Center",
                IsVirtual = false,
                Website = "https://meetup.com/local-tech",
                IsRegistered = true,
                DidAttend = true,
                Notes = "Great networking opportunity with local developers",
                CreatedAt = DateTime.UtcNow.AddDays(-30)
            }
        };
    }

    /// <summary>
    /// Gets sample sessions for seeding.
    /// </summary>
    /// <param name="userId">The user ID to associate with the data.</param>
    /// <param name="eventId">The event ID to associate with the sessions.</param>
    /// <returns>A collection of sample sessions.</returns>
    public static IEnumerable<Session> GetSampleSessions(Guid userId, Guid eventId)
    {
        var baseDate = DateTime.UtcNow.AddMonths(2);
        return new List<Session>
        {
            new()
            {
                SessionId = Guid.NewGuid(),
                UserId = userId,
                EventId = eventId,
                Title = "Building Cloud-Native Applications with .NET",
                Speaker = "Scott Hunter",
                Description = "Learn how to build modern cloud-native applications using .NET 8 and Azure services",
                StartTime = baseDate.AddHours(9),
                EndTime = baseDate.AddHours(10),
                Room = "Hall A",
                PlansToAttend = true,
                DidAttend = false,
                CreatedAt = DateTime.UtcNow.AddDays(-45)
            },
            new()
            {
                SessionId = Guid.NewGuid(),
                UserId = userId,
                EventId = eventId,
                Title = "AI and Machine Learning in Production",
                Speaker = "Sarah Williams",
                Description = "Best practices for deploying ML models at scale",
                StartTime = baseDate.AddHours(11),
                EndTime = baseDate.AddHours(12),
                Room = "Hall B",
                PlansToAttend = true,
                DidAttend = false,
                Notes = "Interested in Azure ML platform",
                CreatedAt = DateTime.UtcNow.AddDays(-40)
            },
            new()
            {
                SessionId = Guid.NewGuid(),
                UserId = userId,
                EventId = eventId,
                Title = "Keynote: The Future of Software Development",
                Speaker = "Satya Nadella",
                Description = "Opening keynote discussing the future direction of technology",
                StartTime = baseDate.AddHours(8),
                EndTime = baseDate.AddHours(9),
                Room = "Main Stage",
                PlansToAttend = true,
                DidAttend = false,
                CreatedAt = DateTime.UtcNow.AddDays(-50)
            }
        };
    }

    /// <summary>
    /// Gets sample contacts for seeding.
    /// </summary>
    /// <param name="userId">The user ID to associate with the data.</param>
    /// <param name="eventId">The event ID to associate with the contacts.</param>
    /// <returns>A collection of sample contacts.</returns>
    public static IEnumerable<Contact> GetSampleContacts(Guid userId, Guid eventId)
    {
        return new List<Contact>
        {
            new()
            {
                ContactId = Guid.NewGuid(),
                UserId = userId,
                EventId = eventId,
                Name = "Jane Smith",
                Company = "Microsoft",
                JobTitle = "Principal Software Engineer",
                Email = "jane.smith@microsoft.com",
                LinkedInUrl = "https://linkedin.com/in/janesmith",
                Notes = "Works on Azure, interested in collaboration opportunities",
                CreatedAt = DateTime.UtcNow.AddDays(-7)
            },
            new()
            {
                ContactId = Guid.NewGuid(),
                UserId = userId,
                EventId = eventId,
                Name = "David Chen",
                Company = "Startup XYZ",
                JobTitle = "CTO",
                Email = "david@startupxyz.com",
                LinkedInUrl = "https://linkedin.com/in/davidchen",
                Notes = "Looking for tech partners, potential client",
                CreatedAt = DateTime.UtcNow.AddDays(-7)
            }
        };
    }

    /// <summary>
    /// Gets sample notes for seeding.
    /// </summary>
    /// <param name="userId">The user ID to associate with the data.</param>
    /// <param name="eventId">The event ID to associate with the notes.</param>
    /// <returns>A collection of sample notes.</returns>
    public static IEnumerable<Note> GetSampleNotes(Guid userId, Guid eventId)
    {
        return new List<Note>
        {
            new()
            {
                NoteId = Guid.NewGuid(),
                UserId = userId,
                EventId = eventId,
                Content = "Key takeaway from keynote: Focus on AI-first development. New Azure services announced for ML workloads.",
                Category = "Keynote",
                Tags = new List<string> { "AI", "Azure", "Strategy" },
                CreatedAt = DateTime.UtcNow.AddDays(-7)
            },
            new()
            {
                NoteId = Guid.NewGuid(),
                UserId = userId,
                EventId = eventId,
                Content = "Met several potential clients at the networking session. Follow up with Jane Smith about Azure collaboration.",
                Category = "Networking",
                Tags = new List<string> { "Contacts", "Follow-up", "Business" },
                CreatedAt = DateTime.UtcNow.AddDays(-6)
            },
            new()
            {
                NoteId = Guid.NewGuid(),
                UserId = userId,
                EventId = eventId,
                Content = "Interesting session on microservices architecture. Consider implementing event-driven patterns in our next project.",
                Category = "Technical",
                Tags = new List<string> { "Architecture", "Microservices", "Ideas" },
                CreatedAt = DateTime.UtcNow.AddDays(-6)
            }
        };
    }
}
