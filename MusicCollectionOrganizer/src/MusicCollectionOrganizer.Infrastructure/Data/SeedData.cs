// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MusicCollectionOrganizer.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using MusicCollectionOrganizer.Core.Model.UserAggregate;
using MusicCollectionOrganizer.Core.Model.UserAggregate.Entities;
using MusicCollectionOrganizer.Core.Services;
namespace MusicCollectionOrganizer.Infrastructure;

/// <summary>
/// Provides seed data for the MusicCollectionOrganizer database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(MusicCollectionOrganizerContext context context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Artists.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedMusicDataAsync(context);
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

    private static async Task SeedMusicDataAsync(MusicCollectionOrganizerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var artist1 = new Artist
        {
            ArtistId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            UserId = sampleUserId,
            Name = "The Beatles",
            Biography = "English rock band formed in Liverpool in 1960",
            Country = "United Kingdom",
            FormedYear = 1960,
            CreatedAt = DateTime.UtcNow,
        };

        var artist2 = new Artist
        {
            ArtistId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
            UserId = sampleUserId,
            Name = "Pink Floyd",
            Biography = "English progressive rock band formed in London in 1965",
            Country = "United Kingdom",
            FormedYear = 1965,
            CreatedAt = DateTime.UtcNow,
        };

        context.Artists.AddRange(artist1, artist2);

        var album1 = new Album
        {
            AlbumId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
            UserId = sampleUserId,
            Title = "Abbey Road",
            ArtistId = artist1.ArtistId,
            Format = Format.Vinyl,
            Genre = Genre.Rock,
            ReleaseYear = 1969,
            Label = "Apple Records",
            PurchasePrice = 29.99m,
            PurchaseDate = new DateTime(2024, 1, 15),
            Notes = "Classic album in excellent condition",
            CreatedAt = DateTime.UtcNow,
        };

        var album2 = new Album
        {
            AlbumId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
            UserId = sampleUserId,
            Title = "The Dark Side of the Moon",
            ArtistId = artist2.ArtistId,
            Format = Format.CD,
            Genre = Genre.Rock,
            ReleaseYear = 1973,
            Label = "Harvest Records",
            PurchasePrice = 14.99m,
            PurchaseDate = new DateTime(2024, 2, 20),
            Notes = "Remastered edition",
            CreatedAt = DateTime.UtcNow,
        };

        context.Albums.AddRange(album1, album2);

        var listeningLog1 = new ListeningLog
        {
            ListeningLogId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
            UserId = sampleUserId,
            AlbumId = album1.AlbumId,
            ListeningDate = new DateTime(2024, 6, 15),
            Rating = 5,
            Notes = "Still sounds amazing after all these years",
            CreatedAt = DateTime.UtcNow,
        };

        var listeningLog2 = new ListeningLog
        {
            ListeningLogId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
            UserId = sampleUserId,
            AlbumId = album2.AlbumId,
            ListeningDate = new DateTime(2024, 7, 1),
            Rating = 5,
            Notes = "Perfect late night listening",
            CreatedAt = DateTime.UtcNow,
        };

        context.ListeningLogs.AddRange(listeningLog1, listeningLog2);

        await context.SaveChangesAsync();
    }
}
