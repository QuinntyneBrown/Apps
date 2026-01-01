// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DateNightIdeaGenerator.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using DateNightIdeaGenerator.Core.Model.UserAggregate;
using DateNightIdeaGenerator.Core.Model.UserAggregate.Entities;
using DateNightIdeaGenerator.Core.Services;
namespace DateNightIdeaGenerator.Infrastructure;

/// <summary>
/// Provides seed data for the DateNightIdeaGenerator database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(DateNightIdeaGeneratorContext context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.DateIdeas.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedDateIdeasAsync(context);
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

    private static async Task SeedDateIdeasAsync(DateNightIdeaGeneratorContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var dateIdeas = new List<DateIdea>
        {
            new DateIdea
            {
                DateIdeaId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Title = "Sunset Picnic at the Park",
                Description = "Pack a basket with your favorite snacks and drinks, bring a blanket, and enjoy a romantic sunset at your local park.",
                Category = Category.Outdoor,
                BudgetRange = BudgetRange.Low,
                Location = "Local Park",
                DurationMinutes = 120,
                Season = "Spring, Summer, Fall",
                IsFavorite = true,
                HasBeenTried = false,
                CreatedAt = DateTime.UtcNow,
            },
            new DateIdea
            {
                DateIdeaId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                Title = "Cooking Class Together",
                Description = "Learn to make a new cuisine together in a fun cooking class. Great for teamwork and you get to enjoy the meal you create!",
                Category = Category.FoodAndDining,
                BudgetRange = BudgetRange.Medium,
                Location = "Culinary School",
                DurationMinutes = 180,
                Season = "Year-round",
                IsFavorite = false,
                HasBeenTried = false,
                CreatedAt = DateTime.UtcNow,
            },
            new DateIdea
            {
                DateIdeaId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                Title = "Museum and Coffee Date",
                Description = "Explore a local museum followed by coffee and conversation at a nearby cafe.",
                Category = Category.Cultural,
                BudgetRange = BudgetRange.Medium,
                Location = "Art Museum",
                DurationMinutes = 180,
                Season = "Year-round",
                IsFavorite = true,
                HasBeenTried = true,
                CreatedAt = DateTime.UtcNow,
            },
            new DateIdea
            {
                DateIdeaId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                UserId = sampleUserId,
                Title = "Star Gazing Night",
                Description = "Drive to a dark sky location, bring blankets and hot chocolate, and spend the night identifying constellations.",
                Category = Category.Romantic,
                BudgetRange = BudgetRange.Free,
                Location = "Dark Sky Location",
                DurationMinutes = 180,
                Season = "Summer, Fall",
                IsFavorite = true,
                HasBeenTried = false,
                CreatedAt = DateTime.UtcNow,
            },
            new DateIdea
            {
                DateIdeaId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                UserId = sampleUserId,
                Title = "Rock Climbing Adventure",
                Description = "Try indoor rock climbing together for an adventurous and physically engaging date.",
                Category = Category.Adventure,
                BudgetRange = BudgetRange.Low,
                Location = "Indoor Climbing Gym",
                DurationMinutes = 120,
                Season = "Year-round",
                IsFavorite = false,
                HasBeenTried = false,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.DateIdeas.AddRange(dateIdeas);

        // Add sample experience for the museum date
        var experience = new Experience
        {
            ExperienceId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            DateIdeaId = dateIdeas[2].DateIdeaId,
            UserId = sampleUserId,
            ExperienceDate = DateTime.UtcNow.AddMonths(-1),
            Notes = "Had an amazing time at the modern art exhibit. The coffee shop nearby had great atmosphere for discussing what we saw.",
            ActualCost = 45.50m,
            Photos = "photo1.jpg,photo2.jpg",
            WasSuccessful = true,
            WouldRepeat = true,
            CreatedAt = DateTime.UtcNow.AddMonths(-1),
        };

        context.Experiences.Add(experience);

        // Add sample ratings
        var ratings = new List<Rating>
        {
            new Rating
            {
                RatingId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                DateIdeaId = dateIdeas[2].DateIdeaId,
                UserId = sampleUserId,
                Score = 5,
                Review = "Perfect date! The museum was fascinating and we had great conversations over coffee.",
                CreatedAt = DateTime.UtcNow.AddMonths(-1),
            },
            new Rating
            {
                RatingId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                ExperienceId = experience.ExperienceId,
                UserId = sampleUserId,
                Score = 5,
                Review = "Everything went smoothly. Would definitely do this again!",
                CreatedAt = DateTime.UtcNow.AddMonths(-1),
            },
            new Rating
            {
                RatingId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                DateIdeaId = dateIdeas[0].DateIdeaId,
                UserId = sampleUserId,
                Score = 5,
                Review = "Can't wait to try this! Sunset picnics are so romantic.",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Ratings.AddRange(ratings);

        await context.SaveChangesAsync();
    }
}
