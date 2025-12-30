// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyTreeBuilder.Infrastructure.Tests;

/// <summary>
/// Unit tests for the FamilyTreeBuilderContext.
/// </summary>
[TestFixture]
public class FamilyTreeBuilderContextTests
{
    private FamilyTreeBuilderContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<FamilyTreeBuilderContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new FamilyTreeBuilderContext(options);
    }

    /// <summary>
    /// Tears down the test context.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    /// <summary>
    /// Tests that Persons can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Persons_CanAddAndRetrieve()
    {
        // Arrange
        var person = new Person
        {
            PersonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            FirstName = "Test",
            LastName = "Person",
            Gender = Gender.Male,
            DateOfBirth = new DateTime(1980, 1, 1),
            BirthPlace = "Test City",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Persons.Add(person);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Persons.FindAsync(person.PersonId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.FirstName, Is.EqualTo("Test"));
        Assert.That(retrieved.LastName, Is.EqualTo("Person"));
        Assert.That(retrieved.Gender, Is.EqualTo(Gender.Male));
    }

    /// <summary>
    /// Tests that Relationships can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Relationships_CanAddAndRetrieve()
    {
        // Arrange
        var person1 = new Person
        {
            PersonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            FirstName = "Parent",
            CreatedAt = DateTime.UtcNow,
        };

        var person2 = new Person
        {
            PersonId = Guid.NewGuid(),
            UserId = person1.UserId,
            FirstName = "Child",
            CreatedAt = DateTime.UtcNow,
        };

        var relationship = new Relationship
        {
            RelationshipId = Guid.NewGuid(),
            PersonId = person1.PersonId,
            RelatedPersonId = person2.PersonId,
            RelationshipType = RelationshipType.Child,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Persons.AddRange(person1, person2);
        _context.Relationships.Add(relationship);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Relationships.FindAsync(relationship.RelationshipId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.PersonId, Is.EqualTo(person1.PersonId));
        Assert.That(retrieved.RelatedPersonId, Is.EqualTo(person2.PersonId));
        Assert.That(retrieved.RelationshipType, Is.EqualTo(RelationshipType.Child));
    }

    /// <summary>
    /// Tests that Stories can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Stories_CanAddAndRetrieve()
    {
        // Arrange
        var person = new Person
        {
            PersonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            FirstName = "Story",
            LastName = "Teller",
            CreatedAt = DateTime.UtcNow,
        };

        var story = new Story
        {
            StoryId = Guid.NewGuid(),
            PersonId = person.PersonId,
            Title = "Test Story",
            Content = "This is a test story about a person's life.",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Persons.Add(person);
        _context.Stories.Add(story);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Stories.FindAsync(story.StoryId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Test Story"));
        Assert.That(retrieved.Content, Is.EqualTo("This is a test story about a person's life."));
    }

    /// <summary>
    /// Tests that FamilyPhotos can be added and retrieved.
    /// </summary>
    [Test]
    public async Task FamilyPhotos_CanAddAndRetrieve()
    {
        // Arrange
        var person = new Person
        {
            PersonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            FirstName = "Photo",
            LastName = "Subject",
            CreatedAt = DateTime.UtcNow,
        };

        var photo = new FamilyPhoto
        {
            FamilyPhotoId = Guid.NewGuid(),
            PersonId = person.PersonId,
            PhotoUrl = "https://example.com/photo.jpg",
            Caption = "Test photo",
            DateTaken = new DateTime(2020, 1, 1),
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Persons.Add(person);
        _context.FamilyPhotos.Add(photo);
        await _context.SaveChangesAsync();

        var retrieved = await _context.FamilyPhotos.FindAsync(photo.FamilyPhotoId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Caption, Is.EqualTo("Test photo"));
        Assert.That(retrieved.PhotoUrl, Is.EqualTo("https://example.com/photo.jpg"));
    }

    /// <summary>
    /// Tests cascade delete for Person and related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedEntities()
    {
        // Arrange
        var person = new Person
        {
            PersonId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            FirstName = "Delete",
            LastName = "Test",
            CreatedAt = DateTime.UtcNow,
        };

        var story = new Story
        {
            StoryId = Guid.NewGuid(),
            PersonId = person.PersonId,
            Title = "Story to Delete",
            CreatedAt = DateTime.UtcNow,
        };

        _context.Persons.Add(person);
        _context.Stories.Add(story);
        await _context.SaveChangesAsync();

        // Act
        _context.Persons.Remove(person);
        await _context.SaveChangesAsync();

        var retrievedStory = await _context.Stories.FindAsync(story.StoryId);

        // Assert
        Assert.That(retrievedStory, Is.Null);
    }
}
