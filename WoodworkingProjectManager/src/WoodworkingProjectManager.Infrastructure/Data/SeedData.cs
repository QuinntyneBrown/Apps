// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using WoodworkingProjectManager.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using WoodworkingProjectManager.Core.Model.UserAggregate;
using WoodworkingProjectManager.Core.Model.UserAggregate.Entities;
using WoodworkingProjectManager.Core.Services;
namespace WoodworkingProjectManager.Infrastructure;

/// <summary>
/// Provides seed data for the WoodworkingProjectManager database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(WoodworkingProjectManagerContext context context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

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

    private static async Task SeedProjectsAsync(WoodworkingProjectManagerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var projects = new List<Project>
        {
            new Project
            {
                ProjectId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Oak Dining Table",
                Description = "A beautiful handcrafted dining table made from red oak",
                Status = ProjectStatus.InProgress,
                WoodType = WoodType.Oak,
                StartDate = DateTime.UtcNow.AddDays(-30),
                EstimatedCost = 800.00m,
                ActualCost = 650.00m,
                Notes = "Using traditional mortise and tenon joinery",
                CreatedAt = DateTime.UtcNow.AddDays(-45),
            },
            new Project
            {
                ProjectId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Name = "Walnut Bookshelf",
                Description = "Floor-to-ceiling bookshelf with adjustable shelves",
                Status = ProjectStatus.Planning,
                WoodType = WoodType.Walnut,
                EstimatedCost = 1200.00m,
                Notes = "Need to measure exact wall dimensions before cutting",
                CreatedAt = DateTime.UtcNow.AddDays(-15),
            },
            new Project
            {
                ProjectId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Name = "Pine Tool Chest",
                Description = "Classic tool chest with dovetail corners",
                Status = ProjectStatus.Completed,
                WoodType = WoodType.Pine,
                StartDate = DateTime.UtcNow.AddDays(-90),
                CompletionDate = DateTime.UtcNow.AddDays(-10),
                EstimatedCost = 250.00m,
                ActualCost = 280.00m,
                Notes = "First attempt at dovetail joints turned out great!",
                CreatedAt = DateTime.UtcNow.AddDays(-95),
            },
            new Project
            {
                ProjectId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                UserId = sampleUserId,
                Name = "Maple Cutting Board",
                Description = "End-grain cutting board with food-safe finish",
                Status = ProjectStatus.OnHold,
                WoodType = WoodType.Maple,
                StartDate = DateTime.UtcNow.AddDays(-60),
                EstimatedCost = 150.00m,
                ActualCost = 120.00m,
                Notes = "Waiting for food-safe mineral oil to arrive",
                CreatedAt = DateTime.UtcNow.AddDays(-70),
            },
            new Project
            {
                ProjectId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                UserId = sampleUserId,
                Name = "Cherry Jewelry Box",
                Description = "Small decorative box with felt lining",
                Status = ProjectStatus.Planning,
                WoodType = WoodType.Cherry,
                EstimatedCost = 100.00m,
                Notes = "Gift for anniversary - need to source hardware",
                CreatedAt = DateTime.UtcNow.AddDays(-5),
            },
        };

        context.Projects.AddRange(projects);

        // Add sample materials for the first project
        var materials = new List<Material>
        {
            new Material
            {
                MaterialId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                ProjectId = projects[0].ProjectId,
                Name = "Red Oak Lumber",
                Description = "4/4 rough sawn red oak boards",
                Quantity = 24,
                Unit = "board feet",
                Cost = 8.50m,
                Supplier = "Local Hardwood Supplier",
                CreatedAt = DateTime.UtcNow.AddDays(-40),
            },
            new Material
            {
                MaterialId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                ProjectId = projects[0].ProjectId,
                Name = "Wood Glue",
                Description = "Titebond III waterproof glue",
                Quantity = 2,
                Unit = "bottles",
                Cost = 12.99m,
                Supplier = "Woodcraft",
                CreatedAt = DateTime.UtcNow.AddDays(-35),
            },
        };

        context.Materials.AddRange(materials);

        // Add sample tools
        var tools = new List<Tool>
        {
            new Tool
            {
                ToolId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Table Saw",
                Brand = "SawStop",
                Model = "PCS31230",
                Description = "Professional cabinet saw with safety brake",
                PurchasePrice = 3200.00m,
                PurchaseDate = DateTime.UtcNow.AddYears(-2),
                Location = "Workshop - Main Area",
                Notes = "Best investment ever - safety is paramount",
                CreatedAt = DateTime.UtcNow.AddYears(-2),
            },
            new Tool
            {
                ToolId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Hand Plane",
                Brand = "Lie-Nielsen",
                Model = "No. 4 Smoothing Plane",
                Description = "Bronze smoothing plane for fine finishing",
                PurchasePrice = 395.00m,
                PurchaseDate = DateTime.UtcNow.AddYears(-1),
                Location = "Tool Cabinet - Drawer 2",
                Notes = "Need to sharpen blade soon",
                CreatedAt = DateTime.UtcNow.AddYears(-1),
            },
            new Tool
            {
                ToolId = Guid.Parse("55555555-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Chisel Set",
                Brand = "Stanley",
                Model = "750 Series",
                Description = "Six-piece bevel edge chisel set",
                PurchasePrice = 89.99m,
                PurchaseDate = DateTime.UtcNow.AddMonths(-6),
                Location = "Tool Cabinet - Drawer 1",
                Notes = "Great starter set for the price",
                CreatedAt = DateTime.UtcNow.AddMonths(-6),
            },
        };

        context.Tools.AddRange(tools);

        await context.SaveChangesAsync();
    }
}
