// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MovieTVShowWatchlist.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using MovieTVShowWatchlist.Core.Model.UserAggregate;
using MovieTVShowWatchlist.Core.Model.UserAggregate.Entities;
using MovieTVShowWatchlist.Core.Services;
namespace MovieTVShowWatchlist.Infrastructure;

/// <summary>
/// Provides seed data for the MovieTVShowWatchlist database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(MovieTVShowWatchlistContext context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Users.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedUsersAndContentAsync(context);
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

    private static async Task SeedUsersAndContentAsync(MovieTVShowWatchlistContext context)
    {
        var user = new User
        {
            UserId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            Username = "moviefan",
            Email = "moviefan@example.com",
            DisplayName = "Movie Fan",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        context.Users.Add(user);

        var movie = new Movie
        {
            MovieId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
            Title = "The Shawshank Redemption",
            ReleaseYear = 1994,
            Director = "Frank Darabont",
            Runtime = 142,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        context.Movies.Add(movie);

        var tvShow = new TVShow
        {
            TVShowId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
            Title = "Breaking Bad",
            PremiereYear = 2008,
            NumberOfSeasons = 5,
            Status = ShowStatus.Completed,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };

        context.TVShows.Add(tvShow);

        await context.SaveChangesAsync();
    }
}
