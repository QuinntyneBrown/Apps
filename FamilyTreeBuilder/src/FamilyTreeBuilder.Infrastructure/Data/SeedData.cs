// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FamilyTreeBuilder.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using FamilyTreeBuilder.Core.Model.UserAggregate;
using FamilyTreeBuilder.Core.Model.UserAggregate.Entities;
using FamilyTreeBuilder.Core.Services;
namespace FamilyTreeBuilder.Infrastructure;

/// <summary>
/// Provides seed data for the FamilyTreeBuilder database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(FamilyTreeBuilderContext context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Persons.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedPersonsAsync(context);
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

    private static async Task SeedPersonsAsync(FamilyTreeBuilderContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var persons = new List<Person>
        {
            new Person
            {
                PersonId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                FirstName = "John",
                LastName = "Smith",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(1950, 3, 15),
                BirthPlace = "New York, NY",
                CreatedAt = DateTime.UtcNow,
            },
            new Person
            {
                PersonId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                FirstName = "Mary",
                LastName = "Smith",
                Gender = Gender.Female,
                DateOfBirth = new DateTime(1952, 7, 22),
                BirthPlace = "Boston, MA",
                CreatedAt = DateTime.UtcNow,
            },
            new Person
            {
                PersonId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                FirstName = "Robert",
                LastName = "Smith",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(1975, 11, 8),
                BirthPlace = "New York, NY",
                CreatedAt = DateTime.UtcNow,
            },
            new Person
            {
                PersonId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                UserId = sampleUserId,
                FirstName = "Sarah",
                LastName = "Smith",
                Gender = Gender.Female,
                DateOfBirth = new DateTime(1978, 5, 20),
                BirthPlace = "Chicago, IL",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Persons.AddRange(persons);

        var relationships = new List<Relationship>
        {
            new Relationship
            {
                RelationshipId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                PersonId = persons[0].PersonId, // John
                RelatedPersonId = persons[1].PersonId, // Mary
                RelationshipType = RelationshipType.Spouse,
                CreatedAt = DateTime.UtcNow,
            },
            new Relationship
            {
                RelationshipId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                PersonId = persons[0].PersonId, // John
                RelatedPersonId = persons[2].PersonId, // Robert
                RelationshipType = RelationshipType.Child,
                CreatedAt = DateTime.UtcNow,
            },
            new Relationship
            {
                RelationshipId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                PersonId = persons[2].PersonId, // Robert
                RelatedPersonId = persons[0].PersonId, // John
                RelationshipType = RelationshipType.Parent,
                CreatedAt = DateTime.UtcNow,
            },
            new Relationship
            {
                RelationshipId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                PersonId = persons[2].PersonId, // Robert
                RelatedPersonId = persons[3].PersonId, // Sarah
                RelationshipType = RelationshipType.Sibling,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Relationships.AddRange(relationships);

        var stories = new List<Story>
        {
            new Story
            {
                StoryId = Guid.Parse("aaa11111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                PersonId = persons[0].PersonId,
                Title = "John's Early Years",
                Content = "John grew up in New York during the 1950s. He was known for his love of baseball and often played with neighborhood kids.",
                CreatedAt = DateTime.UtcNow,
            },
            new Story
            {
                StoryId = Guid.Parse("bbb22222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                PersonId = persons[1].PersonId,
                Title = "Mary's Journey",
                Content = "Mary moved to New York in 1970 where she met John at a local community event. They married two years later.",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Stories.AddRange(stories);

        var photos = new List<FamilyPhoto>
        {
            new FamilyPhoto
            {
                FamilyPhotoId = Guid.Parse("111aaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                PersonId = persons[0].PersonId,
                PhotoUrl = "https://example.com/photos/john-1950.jpg",
                Caption = "John as a young boy",
                DateTaken = new DateTime(1955, 6, 1),
                CreatedAt = DateTime.UtcNow,
            },
            new FamilyPhoto
            {
                FamilyPhotoId = Guid.Parse("222bbbbb-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                PersonId = persons[1].PersonId,
                PhotoUrl = "https://example.com/photos/mary-wedding.jpg",
                Caption = "Mary and John's wedding day",
                DateTaken = new DateTime(1972, 9, 15),
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.FamilyPhotos.AddRange(photos);

        await context.SaveChangesAsync();
    }
}
