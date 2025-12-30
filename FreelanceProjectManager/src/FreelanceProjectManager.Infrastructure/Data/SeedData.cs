// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FreelanceProjectManager.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FreelanceProjectManager.Infrastructure;

/// <summary>
/// Provides seed data for the FreelanceProjectManager database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(FreelanceProjectManagerContext context, ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Clients.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedClientsAndProjectsAsync(context);
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

    private static async Task SeedClientsAndProjectsAsync(FreelanceProjectManagerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var clients = new List<Client>
        {
            new Client
            {
                ClientId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "John Smith",
                CompanyName = "Smith Industries",
                Email = "john.smith@smithind.com",
                Phone = "+1-555-0123",
                Address = "123 Business Ave, New York, NY 10001",
                Website = "https://smithindustries.com",
                Notes = "Long-term client, prefers email communication",
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-180),
            },
            new Client
            {
                ClientId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Name = "Sarah Johnson",
                CompanyName = "TechStart Solutions",
                Email = "sarah@techstart.io",
                Phone = "+1-555-0456",
                Address = "456 Startup Blvd, San Francisco, CA 94102",
                Website = "https://techstart.io",
                Notes = "Fast-growing startup, urgent projects",
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-90),
            },
            new Client
            {
                ClientId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Name = "Michael Brown",
                CompanyName = "Brown Consulting",
                Email = "michael@brownconsulting.com",
                Phone = "+1-555-0789",
                IsActive = true,
                CreatedAt = DateTime.UtcNow.AddDays(-30),
            },
        };

        context.Clients.AddRange(clients);

        var projects = new List<Project>
        {
            new Project
            {
                ProjectId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                ClientId = clients[0].ClientId,
                Name = "Website Redesign",
                Description = "Complete redesign of corporate website with modern UI/UX",
                Status = ProjectStatus.InProgress,
                StartDate = DateTime.UtcNow.AddDays(-60),
                DueDate = DateTime.UtcNow.AddDays(30),
                HourlyRate = 150.00m,
                Currency = "USD",
                Notes = "Client wants weekly progress updates",
                CreatedAt = DateTime.UtcNow.AddDays(-60),
            },
            new Project
            {
                ProjectId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                ClientId = clients[1].ClientId,
                Name = "Mobile App Development",
                Description = "iOS and Android app for inventory management",
                Status = ProjectStatus.InProgress,
                StartDate = DateTime.UtcNow.AddDays(-30),
                DueDate = DateTime.UtcNow.AddDays(60),
                FixedBudget = 25000.00m,
                Currency = "USD",
                Notes = "Agile methodology, bi-weekly sprints",
                CreatedAt = DateTime.UtcNow.AddDays(-30),
            },
            new Project
            {
                ProjectId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                ClientId = clients[0].ClientId,
                Name = "Database Migration",
                Description = "Migrate legacy database to cloud infrastructure",
                Status = ProjectStatus.Completed,
                StartDate = DateTime.UtcNow.AddDays(-120),
                DueDate = DateTime.UtcNow.AddDays(-30),
                CompletionDate = DateTime.UtcNow.AddDays(-35),
                HourlyRate = 175.00m,
                Currency = "USD",
                Notes = "Successfully completed ahead of schedule",
                CreatedAt = DateTime.UtcNow.AddDays(-120),
            },
        };

        context.Projects.AddRange(projects);

        var timeEntries = new List<TimeEntry>
        {
            new TimeEntry
            {
                TimeEntryId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                ProjectId = projects[0].ProjectId,
                WorkDate = DateTime.UtcNow.AddDays(-5),
                Hours = 6.5m,
                Description = "Implemented homepage responsive design",
                IsBillable = true,
                IsInvoiced = false,
                CreatedAt = DateTime.UtcNow.AddDays(-5),
            },
            new TimeEntry
            {
                TimeEntryId = Guid.Parse("55555555-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                ProjectId = projects[0].ProjectId,
                WorkDate = DateTime.UtcNow.AddDays(-3),
                Hours = 8.0m,
                Description = "Developed contact form with validation",
                IsBillable = true,
                IsInvoiced = false,
                CreatedAt = DateTime.UtcNow.AddDays(-3),
            },
            new TimeEntry
            {
                TimeEntryId = Guid.Parse("66666666-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                ProjectId = projects[1].ProjectId,
                WorkDate = DateTime.UtcNow.AddDays(-2),
                Hours = 7.5m,
                Description = "Implemented authentication module",
                IsBillable = true,
                IsInvoiced = false,
                CreatedAt = DateTime.UtcNow.AddDays(-2),
            },
        };

        context.TimeEntries.AddRange(timeEntries);

        var invoices = new List<Invoice>
        {
            new Invoice
            {
                InvoiceId = Guid.Parse("77777777-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                ClientId = clients[0].ClientId,
                ProjectId = projects[2].ProjectId,
                InvoiceNumber = "INV-2024-001",
                InvoiceDate = DateTime.UtcNow.AddDays(-40),
                DueDate = DateTime.UtcNow.AddDays(-10),
                TotalAmount = 8750.00m,
                Currency = "USD",
                Status = "Paid",
                PaidDate = DateTime.UtcNow.AddDays(-12),
                Notes = "Payment received on time",
                CreatedAt = DateTime.UtcNow.AddDays(-40),
            },
            new Invoice
            {
                InvoiceId = Guid.Parse("88888888-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                ClientId = clients[1].ClientId,
                ProjectId = projects[1].ProjectId,
                InvoiceNumber = "INV-2024-002",
                InvoiceDate = DateTime.UtcNow.AddDays(-15),
                DueDate = DateTime.UtcNow.AddDays(15),
                TotalAmount = 10000.00m,
                Currency = "USD",
                Status = "Sent",
                Notes = "First milestone payment",
                CreatedAt = DateTime.UtcNow.AddDays(-15),
            },
        };

        context.Invoices.AddRange(invoices);

        await context.SaveChangesAsync();
    }
}
