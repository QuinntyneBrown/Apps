// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhotographySessionLogger.Core;

namespace PhotographySessionLogger.Infrastructure;

/// <summary>
/// Provides seed data for the PhotographySessionLogger database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(PhotographySessionLoggerContext context, ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Sessions.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedPhotographyDataAsync(context);
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

    private static async Task SeedPhotographyDataAsync(PhotographySessionLoggerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        // Seed Gear
        var gears = new List<Gear>
        {
            new Gear
            {
                GearId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Canon EOS R5",
                GearType = "Camera",
                Brand = "Canon",
                Model = "EOS R5",
                PurchaseDate = new DateTime(2022, 5, 15),
                PurchasePrice = 3899.99m,
                Notes = "Primary camera body",
                CreatedAt = DateTime.UtcNow,
            },
            new Gear
            {
                GearId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Name = "RF 24-70mm f/2.8",
                GearType = "Lens",
                Brand = "Canon",
                Model = "RF 24-70mm f/2.8L IS USM",
                PurchaseDate = new DateTime(2022, 6, 1),
                PurchasePrice = 2299.99m,
                Notes = "Versatile zoom lens",
                CreatedAt = DateTime.UtcNow,
            },
            new Gear
            {
                GearId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Name = "Manfrotto Tripod",
                GearType = "Tripod",
                Brand = "Manfrotto",
                Model = "MT055XPRO3",
                PurchaseDate = new DateTime(2022, 3, 10),
                PurchasePrice = 249.99m,
                Notes = "Sturdy aluminum tripod",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Gears.AddRange(gears);

        // Seed Projects
        var projects = new List<Project>
        {
            new Project
            {
                ProjectId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Wedding Portfolio 2024",
                Description = "Build comprehensive wedding photography portfolio",
                DueDate = new DateTime(2024, 12, 31),
                IsCompleted = false,
                Notes = "Focus on candid moments and emotional shots",
                CreatedAt = DateTime.UtcNow,
            },
            new Project
            {
                ProjectId = Guid.Parse("22222222-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Name = "Nature Photography Series",
                Description = "Capture local wildlife and landscapes",
                DueDate = new DateTime(2024, 10, 1),
                IsCompleted = true,
                Notes = "Completed - 50 images selected",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Projects.AddRange(projects);

        // Seed Sessions
        var sessions = new List<Session>
        {
            new Session
            {
                SessionId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                UserId = sampleUserId,
                Title = "Smith Wedding Ceremony",
                SessionType = SessionType.Wedding,
                SessionDate = new DateTime(2024, 6, 15, 14, 0, 0),
                Location = "Grand Hotel Ballroom",
                Client = "John and Jane Smith",
                Notes = "Beautiful outdoor ceremony, natural lighting",
                CreatedAt = DateTime.UtcNow,
            },
            new Session
            {
                SessionId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                UserId = sampleUserId,
                Title = "Mountain Landscape Shoot",
                SessionType = SessionType.Landscape,
                SessionDate = new DateTime(2024, 5, 20, 6, 0, 0),
                Location = "Rocky Mountain National Park",
                Notes = "Golden hour photography, stunning sunrise",
                CreatedAt = DateTime.UtcNow,
            },
            new Session
            {
                SessionId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                UserId = sampleUserId,
                Title = "Corporate Headshots",
                SessionType = SessionType.Portrait,
                SessionDate = new DateTime(2024, 7, 10, 10, 0, 0),
                Location = "Tech Corp Office",
                Client = "Tech Corp Inc.",
                Notes = "Professional headshots for 15 employees",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Sessions.AddRange(sessions);

        // Seed Photos
        var photos = new List<Photo>
        {
            new Photo
            {
                PhotoId = Guid.Parse("33333333-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                SessionId = sessions[0].SessionId,
                FileName = "DSC_0001.RAW",
                FilePath = "/photos/2024/06/wedding/DSC_0001.RAW",
                CameraSettings = "ISO 400, f/2.8, 1/250s",
                Rating = 5,
                Tags = "ceremony,bride,emotional",
                CreatedAt = DateTime.UtcNow,
            },
            new Photo
            {
                PhotoId = Guid.Parse("44444444-dddd-dddd-dddd-dddddddddddd"),
                UserId = sampleUserId,
                SessionId = sessions[1].SessionId,
                FileName = "DSC_0150.RAW",
                FilePath = "/photos/2024/05/landscape/DSC_0150.RAW",
                CameraSettings = "ISO 100, f/8, 1/60s",
                Rating = 4,
                Tags = "landscape,sunrise,mountains",
                CreatedAt = DateTime.UtcNow,
            },
            new Photo
            {
                PhotoId = Guid.Parse("55555555-eeee-eeee-eeee-eeeeeeeeeeee"),
                UserId = sampleUserId,
                SessionId = sessions[2].SessionId,
                FileName = "DSC_0200.RAW",
                FilePath = "/photos/2024/07/corporate/DSC_0200.RAW",
                CameraSettings = "ISO 200, f/4, 1/125s",
                Rating = 3,
                Tags = "portrait,professional,headshot",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Photos.AddRange(photos);

        await context.SaveChangesAsync();
    }
}
