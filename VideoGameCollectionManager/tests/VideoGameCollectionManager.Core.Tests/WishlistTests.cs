// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace VideoGameCollectionManager.Core.Tests;

public class WishlistTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesWishlist()
    {
        // Arrange
        var wishlistId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var title = "Elden Ring";
        var platform = Platform.PlayStation5;
        var genre = Genre.RPG;
        var priority = 5;
        var notes = "Must buy on sale";

        // Act
        var wishlist = new Wishlist
        {
            WishlistId = wishlistId,
            UserId = userId,
            Title = title,
            Platform = platform,
            Genre = genre,
            Priority = priority,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(wishlist.WishlistId, Is.EqualTo(wishlistId));
            Assert.That(wishlist.UserId, Is.EqualTo(userId));
            Assert.That(wishlist.Title, Is.EqualTo(title));
            Assert.That(wishlist.Platform, Is.EqualTo(platform));
            Assert.That(wishlist.Genre, Is.EqualTo(genre));
            Assert.That(wishlist.Priority, Is.EqualTo(priority));
            Assert.That(wishlist.Notes, Is.EqualTo(notes));
            Assert.That(wishlist.IsAcquired, Is.False);
        });
    }

    [Test]
    public void Priority_DefaultsTo3()
    {
        // Arrange & Act
        var wishlist = new Wishlist
        {
            WishlistId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Game Title"
        };

        // Assert
        Assert.That(wishlist.Priority, Is.EqualTo(3));
    }

    [Test]
    public void IsAcquired_DefaultsToFalse()
    {
        // Arrange & Act
        var wishlist = new Wishlist
        {
            WishlistId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Game Title"
        };

        // Assert
        Assert.That(wishlist.IsAcquired, Is.False);
    }

    [Test]
    public void CreatedAt_DefaultsToUtcNow()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow;

        // Act
        var wishlist = new Wishlist
        {
            WishlistId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Upcoming Game"
        };

        var afterCreation = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(wishlist.CreatedAt, Is.GreaterThanOrEqualTo(beforeCreation));
            Assert.That(wishlist.CreatedAt, Is.LessThanOrEqualTo(afterCreation));
        });
    }

    [Test]
    public void Wishlist_OptionalFields_CanBeNull()
    {
        // Arrange & Act
        var wishlist = new Wishlist
        {
            WishlistId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Generic Game"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(wishlist.Platform, Is.Null);
            Assert.That(wishlist.Genre, Is.Null);
            Assert.That(wishlist.Notes, Is.Null);
        });
    }

    [Test]
    public void IsAcquired_CanBeSetToTrue()
    {
        // Arrange
        var wishlist = new Wishlist
        {
            WishlistId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Acquired Game",
            IsAcquired = false
        };

        // Act
        wishlist.IsAcquired = true;

        // Assert
        Assert.That(wishlist.IsAcquired, Is.True);
    }

    [Test]
    public void Priority_CanBeSetToHighValue()
    {
        // Arrange & Act
        var wishlist = new Wishlist
        {
            WishlistId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "High Priority Game",
            Priority = 10
        };

        // Assert
        Assert.That(wishlist.Priority, Is.EqualTo(10));
    }

    [Test]
    public void Priority_CanBeSetToLowValue()
    {
        // Arrange & Act
        var wishlist = new Wishlist
        {
            WishlistId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Low Priority Game",
            Priority = 1
        };

        // Assert
        Assert.That(wishlist.Priority, Is.EqualTo(1));
    }

    [Test]
    public void Wishlist_WithPlatformAndGenre_StoresCorrectly()
    {
        // Arrange & Act
        var wishlist = new Wishlist
        {
            WishlistId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Specific Game",
            Platform = Platform.NintendoSwitch,
            Genre = Genre.Adventure
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(wishlist.Platform, Is.EqualTo(Platform.NintendoSwitch));
            Assert.That(wishlist.Genre, Is.EqualTo(Genre.Adventure));
        });
    }

    [Test]
    public void Wishlist_WithLongNotes_StoresCorrectly()
    {
        // Arrange
        var longNotes = "Waiting for this game to go on sale during the summer sale. " +
                       "Reviews are positive and gameplay looks amazing. " +
                       "Will purchase when price drops below $40.";

        // Act
        var wishlist = new Wishlist
        {
            WishlistId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Wishlist Game",
            Notes = longNotes
        };

        // Assert
        Assert.That(wishlist.Notes, Is.EqualTo(longNotes));
    }
}
