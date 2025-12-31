// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyPhotoAlbumOrganizer.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using FamilyPhotoAlbumOrganizer.Core.Model.UserAggregate;
using FamilyPhotoAlbumOrganizer.Core.Model.UserAggregate.Entities;
using FamilyPhotoAlbumOrganizer.Core.Services;
namespace FamilyPhotoAlbumOrganizer.Infrastructure;

/// <summary>
/// Provides seed data for the FamilyPhotoAlbumOrganizer database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(FamilyPhotoAlbumOrganizerContext context context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Albums.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedAlbumsAsync(context);
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

    private static async Task SeedAlbumsAsync(FamilyPhotoAlbumOrganizerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var albums = new List<Album>
        {
            new Album
            {
                AlbumId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Summer Vacation 2024",
                Description = "Our amazing summer trip to the beach",
                CoverPhotoUrl = "https://example.com/photos/summer-2024-cover.jpg",
                CreatedDate = new DateTime(2024, 6, 15),
                CreatedAt = DateTime.UtcNow,
            },
            new Album
            {
                AlbumId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Name = "Holiday Memories",
                Description = "Christmas and New Year celebrations",
                CoverPhotoUrl = "https://example.com/photos/holiday-cover.jpg",
                CreatedDate = new DateTime(2023, 12, 25),
                CreatedAt = DateTime.UtcNow,
            },
            new Album
            {
                AlbumId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Name = "Family Gatherings",
                Description = "Special moments with family",
                CoverPhotoUrl = "https://example.com/photos/family-cover.jpg",
                CreatedDate = new DateTime(2024, 1, 1),
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Albums.AddRange(albums);

        var photos = new List<Photo>
        {
            new Photo
            {
                PhotoId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                AlbumId = albums[0].AlbumId,
                FileName = "beach-sunset.jpg",
                FileUrl = "https://example.com/photos/beach-sunset.jpg",
                ThumbnailUrl = "https://example.com/photos/thumbs/beach-sunset.jpg",
                Caption = "Beautiful sunset at the beach",
                DateTaken = new DateTime(2024, 6, 20, 18, 30, 0),
                Location = "Santa Monica Beach, CA",
                IsFavorite = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Photo
            {
                PhotoId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                AlbumId = albums[0].AlbumId,
                FileName = "family-beach.jpg",
                FileUrl = "https://example.com/photos/family-beach.jpg",
                ThumbnailUrl = "https://example.com/photos/thumbs/family-beach.jpg",
                Caption = "Family fun at the beach",
                DateTaken = new DateTime(2024, 6, 21, 14, 0, 0),
                Location = "Santa Monica Beach, CA",
                IsFavorite = false,
                CreatedAt = DateTime.UtcNow,
            },
            new Photo
            {
                PhotoId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                AlbumId = albums[1].AlbumId,
                FileName = "christmas-tree.jpg",
                FileUrl = "https://example.com/photos/christmas-tree.jpg",
                ThumbnailUrl = "https://example.com/photos/thumbs/christmas-tree.jpg",
                Caption = "Our festive Christmas tree",
                DateTaken = new DateTime(2023, 12, 25, 8, 0, 0),
                Location = "Home",
                IsFavorite = true,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Photos.AddRange(photos);

        var tags = new List<Tag>
        {
            new Tag
            {
                TagId = Guid.Parse("aaa11111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Beach",
                CreatedAt = DateTime.UtcNow,
            },
            new Tag
            {
                TagId = Guid.Parse("bbb22222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Vacation",
                CreatedAt = DateTime.UtcNow,
            },
            new Tag
            {
                TagId = Guid.Parse("ccc33333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Family",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Tags.AddRange(tags);

        // Associate tags with photos
        photos[0].Tags.Add(tags[0]); // Beach sunset - Beach
        photos[0].Tags.Add(tags[1]); // Beach sunset - Vacation
        photos[1].Tags.Add(tags[0]); // Family beach - Beach
        photos[1].Tags.Add(tags[1]); // Family beach - Vacation
        photos[1].Tags.Add(tags[2]); // Family beach - Family
        photos[2].Tags.Add(tags[2]); // Christmas tree - Family

        var personTags = new List<PersonTag>
        {
            new PersonTag
            {
                PersonTagId = Guid.Parse("111aaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                PhotoId = photos[1].PhotoId,
                PersonName = "Mom",
                CoordinateX = 100,
                CoordinateY = 150,
                CreatedAt = DateTime.UtcNow,
            },
            new PersonTag
            {
                PersonTagId = Guid.Parse("222bbbbb-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                PhotoId = photos[1].PhotoId,
                PersonName = "Dad",
                CoordinateX = 300,
                CoordinateY = 150,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.PersonTags.AddRange(personTags);

        await context.SaveChangesAsync();
    }
}
