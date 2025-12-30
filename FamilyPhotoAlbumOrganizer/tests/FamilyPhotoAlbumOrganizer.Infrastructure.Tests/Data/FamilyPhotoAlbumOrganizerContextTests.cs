// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyPhotoAlbumOrganizer.Infrastructure.Tests;

/// <summary>
/// Unit tests for the FamilyPhotoAlbumOrganizerContext.
/// </summary>
[TestFixture]
public class FamilyPhotoAlbumOrganizerContextTests
{
    private FamilyPhotoAlbumOrganizerContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<FamilyPhotoAlbumOrganizerContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new FamilyPhotoAlbumOrganizerContext(options);
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
    /// Tests that Albums can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Albums_CanAddAndRetrieve()
    {
        // Arrange
        var album = new Album
        {
            AlbumId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Album",
            Description = "A test album",
            CreatedDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Albums.Add(album);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Albums.FindAsync(album.AlbumId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Album"));
        Assert.That(retrieved.Description, Is.EqualTo("A test album"));
    }

    /// <summary>
    /// Tests that Photos can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Photos_CanAddAndRetrieve()
    {
        // Arrange
        var photo = new Photo
        {
            PhotoId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            FileName = "test-photo.jpg",
            FileUrl = "https://example.com/test-photo.jpg",
            Caption = "A test photo",
            IsFavorite = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Photos.Add(photo);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Photos.FindAsync(photo.PhotoId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.FileName, Is.EqualTo("test-photo.jpg"));
        Assert.That(retrieved.IsFavorite, Is.True);
    }

    /// <summary>
    /// Tests that Tags can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Tags_CanAddAndRetrieve()
    {
        // Arrange
        var tag = new Tag
        {
            TagId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Tag",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Tags.Add(tag);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Tags.FindAsync(tag.TagId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Tag"));
    }

    /// <summary>
    /// Tests that PersonTags can be added and retrieved.
    /// </summary>
    [Test]
    public async Task PersonTags_CanAddAndRetrieve()
    {
        // Arrange
        var photo = new Photo
        {
            PhotoId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            FileName = "family-photo.jpg",
            FileUrl = "https://example.com/family-photo.jpg",
            IsFavorite = false,
            CreatedAt = DateTime.UtcNow,
        };

        var personTag = new PersonTag
        {
            PersonTagId = Guid.NewGuid(),
            PhotoId = photo.PhotoId,
            PersonName = "John Doe",
            CoordinateX = 100,
            CoordinateY = 200,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Photos.Add(photo);
        _context.PersonTags.Add(personTag);
        await _context.SaveChangesAsync();

        var retrieved = await _context.PersonTags.FindAsync(personTag.PersonTagId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.PersonName, Is.EqualTo("John Doe"));
        Assert.That(retrieved.CoordinateX, Is.EqualTo(100));
    }

    /// <summary>
    /// Tests that Photos can be associated with Albums.
    /// </summary>
    [Test]
    public async Task Photos_CanBeAssociatedWithAlbums()
    {
        // Arrange
        var album = new Album
        {
            AlbumId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Family Album",
            CreatedDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        var photo = new Photo
        {
            PhotoId = Guid.NewGuid(),
            UserId = album.UserId,
            AlbumId = album.AlbumId,
            FileName = "family.jpg",
            FileUrl = "https://example.com/family.jpg",
            IsFavorite = false,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Albums.Add(album);
        _context.Photos.Add(photo);
        await _context.SaveChangesAsync();

        var retrievedPhoto = await _context.Photos
            .Include(p => p.Album)
            .FirstOrDefaultAsync(p => p.PhotoId == photo.PhotoId);

        // Assert
        Assert.That(retrievedPhoto, Is.Not.Null);
        Assert.That(retrievedPhoto!.Album, Is.Not.Null);
        Assert.That(retrievedPhoto.Album!.Name, Is.EqualTo("Family Album"));
    }

    /// <summary>
    /// Tests that Photos can have multiple Tags.
    /// </summary>
    [Test]
    public async Task Photos_CanHaveMultipleTags()
    {
        // Arrange
        var photo = new Photo
        {
            PhotoId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            FileName = "vacation.jpg",
            FileUrl = "https://example.com/vacation.jpg",
            IsFavorite = true,
            CreatedAt = DateTime.UtcNow,
        };

        var tag1 = new Tag
        {
            TagId = Guid.NewGuid(),
            UserId = photo.UserId,
            Name = "Vacation",
            CreatedAt = DateTime.UtcNow,
        };

        var tag2 = new Tag
        {
            TagId = Guid.NewGuid(),
            UserId = photo.UserId,
            Name = "Beach",
            CreatedAt = DateTime.UtcNow,
        };

        photo.Tags.Add(tag1);
        photo.Tags.Add(tag2);

        // Act
        _context.Photos.Add(photo);
        _context.Tags.AddRange(tag1, tag2);
        await _context.SaveChangesAsync();

        var retrievedPhoto = await _context.Photos
            .Include(p => p.Tags)
            .FirstOrDefaultAsync(p => p.PhotoId == photo.PhotoId);

        // Assert
        Assert.That(retrievedPhoto, Is.Not.Null);
        Assert.That(retrievedPhoto!.Tags, Has.Count.EqualTo(2));
        Assert.That(retrievedPhoto.Tags.Select(t => t.Name), Contains.Item("Vacation"));
        Assert.That(retrievedPhoto.Tags.Select(t => t.Name), Contains.Item("Beach"));
    }
}
