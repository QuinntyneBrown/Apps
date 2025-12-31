// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProfessionalNetworkCRM.Core;

using ProfessionalNetworkCRM.Core.Model.UserAggregate;
using ProfessionalNetworkCRM.Core.Model.UserAggregate.Entities;
using ProfessionalNetworkCRM.Core.Services;
namespace ProfessionalNetworkCRM.Infrastructure;

/// <summary>
/// Provides seed data for the ProfessionalNetworkCRM database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(ProfessionalNetworkCRMContext context context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Contacts.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedNetworkDataAsync(context);
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

    private static async Task SeedNetworkDataAsync(ProfessionalNetworkCRMContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        // Seed Contacts
        var contacts = new List<Contact>
        {
            new Contact
            {
                ContactId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                FirstName = "Sarah",
                LastName = "Johnson",
                ContactType = ContactType.Mentor,
                Company = "Tech Innovations Inc",
                JobTitle = "Senior Engineering Manager",
                Email = "sarah.johnson@techinnovations.com",
                Phone = "+1-555-0123",
                LinkedInUrl = "https://linkedin.com/in/sarahjohnson",
                Location = "San Francisco, CA",
                Notes = "Met at tech conference 2023. Very helpful with career advice.",
                Tags = new List<string> { "mentor", "engineering", "leadership" },
                DateMet = new DateTime(2023, 6, 15),
                LastContactedDate = new DateTime(2024, 5, 20),
                IsPriority = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Contact
            {
                ContactId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                FirstName = "Michael",
                LastName = "Chen",
                ContactType = ContactType.Colleague,
                Company = "DataCorp Solutions",
                JobTitle = "Product Manager",
                Email = "mchen@datacorp.com",
                Phone = "+1-555-0456",
                LinkedInUrl = "https://linkedin.com/in/michaelchen",
                Location = "Seattle, WA",
                Notes = "Former colleague, now at DataCorp. Great product insights.",
                Tags = new List<string> { "product", "data", "strategy" },
                DateMet = new DateTime(2022, 3, 10),
                LastContactedDate = new DateTime(2024, 6, 5),
                IsPriority = false,
                CreatedAt = DateTime.UtcNow,
            },
            new Contact
            {
                ContactId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                FirstName = "Emily",
                LastName = "Rodriguez",
                ContactType = ContactType.Client,
                Company = "Future Finance LLC",
                JobTitle = "VP of Technology",
                Email = "emily.r@futurefinance.com",
                Phone = "+1-555-0789",
                LinkedInUrl = "https://linkedin.com/in/emilyrodriguez",
                Location = "New York, NY",
                Notes = "Key decision maker for our largest client.",
                Tags = new List<string> { "client", "finance", "executive" },
                DateMet = new DateTime(2023, 1, 20),
                LastContactedDate = new DateTime(2024, 7, 1),
                IsPriority = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Contact
            {
                ContactId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                UserId = sampleUserId,
                FirstName = "David",
                LastName = "Park",
                ContactType = ContactType.IndustryPeer,
                Company = "Innovation Labs",
                JobTitle = "CTO",
                Email = "david.park@innovationlabs.com",
                LinkedInUrl = "https://linkedin.com/in/davidpark",
                Location = "Austin, TX",
                Notes = "Met at AWS Summit. Interesting collaboration opportunities.",
                Tags = new List<string> { "cto", "cloud", "innovation" },
                DateMet = new DateTime(2024, 4, 10),
                LastContactedDate = new DateTime(2024, 6, 15),
                IsPriority = false,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Contacts.AddRange(contacts);

        // Seed Interactions
        var interactions = new List<Interaction>
        {
            new Interaction
            {
                InteractionId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                ContactId = contacts[0].ContactId,
                InteractionType = "Coffee Meeting",
                InteractionDate = new DateTime(2024, 5, 20),
                Subject = "Career advice session",
                Notes = "Discussed career progression and leadership opportunities",
                Outcome = "Great insights on moving into management",
                DurationMinutes = 60,
                CreatedAt = DateTime.UtcNow,
            },
            new Interaction
            {
                InteractionId = Guid.Parse("22222222-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                ContactId = contacts[1].ContactId,
                InteractionType = "Email",
                InteractionDate = new DateTime(2024, 6, 5),
                Subject = "Product feedback request",
                Notes = "Shared feedback on their new feature release",
                Outcome = "Positive response, will consider our suggestions",
                CreatedAt = DateTime.UtcNow,
            },
            new Interaction
            {
                InteractionId = Guid.Parse("33333333-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                ContactId = contacts[2].ContactId,
                InteractionType = "Video Call",
                InteractionDate = new DateTime(2024, 7, 1),
                Subject = "Quarterly business review",
                Notes = "Discussed Q2 results and Q3 goals",
                Outcome = "Client satisfied, planning expansion",
                DurationMinutes = 45,
                CreatedAt = DateTime.UtcNow,
            },
            new Interaction
            {
                InteractionId = Guid.Parse("44444444-dddd-dddd-dddd-dddddddddddd"),
                UserId = sampleUserId,
                ContactId = contacts[3].ContactId,
                InteractionType = "Conference Meeting",
                InteractionDate = new DateTime(2024, 6, 15),
                Subject = "Follow-up from AWS Summit",
                Notes = "Explored potential partnership opportunities",
                Outcome = "Scheduled technical discussion for next month",
                DurationMinutes = 30,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Interactions.AddRange(interactions);

        // Seed FollowUps
        var followUps = new List<FollowUp>
        {
            new FollowUp
            {
                FollowUpId = Guid.Parse("55555555-eeee-eeee-eeee-eeeeeeeeeeee"),
                UserId = sampleUserId,
                ContactId = contacts[0].ContactId,
                Description = "Send thank you note and book summary",
                DueDate = DateTime.UtcNow.AddDays(7),
                Priority = "High",
                IsCompleted = false,
                Notes = "She recommended a book on leadership",
                CreatedAt = DateTime.UtcNow,
            },
            new FollowUp
            {
                FollowUpId = Guid.Parse("66666666-ffff-ffff-ffff-ffffffffffff"),
                UserId = sampleUserId,
                ContactId = contacts[2].ContactId,
                Description = "Prepare Q3 proposal",
                DueDate = DateTime.UtcNow.AddDays(14),
                Priority = "High",
                IsCompleted = false,
                Notes = "Client wants to expand engagement",
                CreatedAt = DateTime.UtcNow,
            },
            new FollowUp
            {
                FollowUpId = Guid.Parse("77777777-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                ContactId = contacts[3].ContactId,
                Description = "Schedule technical deep-dive",
                DueDate = DateTime.UtcNow.AddDays(21),
                Priority = "Medium",
                IsCompleted = false,
                Notes = "Discuss cloud architecture collaboration",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.FollowUps.AddRange(followUps);

        await context.SaveChangesAsync();
    }
}
