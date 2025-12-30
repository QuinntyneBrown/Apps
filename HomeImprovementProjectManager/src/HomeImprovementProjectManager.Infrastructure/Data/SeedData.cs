// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeImprovementProjectManager.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeImprovementProjectManager.Infrastructure;

/// <summary>
/// Provides seed data for the HomeImprovementProjectManager database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(HomeImprovementProjectManagerContext context, ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Projects.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedProjectsAsync(context);
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

    private static async Task SeedProjectsAsync(HomeImprovementProjectManagerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var projects = new List<Project>
        {
            new Project
            {
                ProjectId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Kitchen Remodel",
                Description = "Complete kitchen renovation including new cabinets, countertops, and appliances",
                Status = ProjectStatus.InProgress,
                StartDate = new DateTime(2024, 10, 1),
                EndDate = new DateTime(2025, 2, 28),
                EstimatedCost = 35000m,
                ActualCost = 18500m,
                CreatedAt = DateTime.UtcNow,
            },
            new Project
            {
                ProjectId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Name = "Bathroom Update",
                Description = "New tile, fixtures, and vanity in master bathroom",
                Status = ProjectStatus.Planning,
                StartDate = new DateTime(2025, 3, 1),
                EstimatedCost = 12000m,
                CreatedAt = DateTime.UtcNow,
            },
            new Project
            {
                ProjectId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Name = "Deck Construction",
                Description = "Build new composite deck with built-in seating",
                Status = ProjectStatus.Completed,
                StartDate = new DateTime(2024, 5, 1),
                EndDate = new DateTime(2024, 7, 15),
                EstimatedCost = 8000m,
                ActualCost = 8750m,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Projects.AddRange(projects);

        // Add budgets for kitchen remodel
        var budgets = new List<Budget>
        {
            new Budget
            {
                BudgetId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                ProjectId = projects[0].ProjectId,
                Category = "Cabinets",
                AllocatedAmount = 15000m,
                SpentAmount = 14200m,
                CreatedAt = DateTime.UtcNow,
            },
            new Budget
            {
                BudgetId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                ProjectId = projects[0].ProjectId,
                Category = "Countertops",
                AllocatedAmount = 8000m,
                SpentAmount = 7500m,
                CreatedAt = DateTime.UtcNow,
            },
            new Budget
            {
                BudgetId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                ProjectId = projects[0].ProjectId,
                Category = "Appliances",
                AllocatedAmount = 12000m,
                SpentAmount = 11800m,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Budgets.AddRange(budgets);

        // Add contractors
        var contractors = new List<Contractor>
        {
            new Contractor
            {
                ContractorId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                ProjectId = projects[0].ProjectId,
                Name = "ABC Kitchen & Bath",
                Trade = "General Contractor",
                PhoneNumber = "555-0101",
                Email = "contact@abckitchen.com",
                Rating = 5,
                CreatedAt = DateTime.UtcNow,
            },
            new Contractor
            {
                ContractorId = Guid.Parse("55555555-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                ProjectId = projects[0].ProjectId,
                Name = "Smith Plumbing",
                Trade = "Plumber",
                PhoneNumber = "555-0202",
                Email = "info@smithplumbing.com",
                Rating = 4,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Contractors.AddRange(contractors);

        // Add materials for kitchen remodel
        var materials = new List<Material>
        {
            new Material
            {
                MaterialId = Guid.Parse("66666666-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                ProjectId = projects[0].ProjectId,
                Name = "Quartz Countertop",
                Quantity = 45,
                Unit = "sq ft",
                UnitCost = 85m,
                TotalCost = 3825m,
                Supplier = "Stone World",
                CreatedAt = DateTime.UtcNow,
            },
            new Material
            {
                MaterialId = Guid.Parse("77777777-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                ProjectId = projects[0].ProjectId,
                Name = "Ceramic Tile",
                Quantity = 120,
                Unit = "sq ft",
                UnitCost = 12m,
                TotalCost = 1440m,
                Supplier = "Tile Depot",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Materials.AddRange(materials);

        await context.SaveChangesAsync();
    }
}
