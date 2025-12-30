// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BookReadingTrackerLibrary.Core.Tests;

public class WishlistTests
{
    [Test]
    public void Constructor_DefaultValues_SetsPropertiesCorrectly()
    {
        // Arrange & Act
        var wishlist = new Wishlist
        {
            WishlistId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "The Great Gatsby",
            Author = "F. Scott Fitzgerald"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(wishlist.WishlistId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(wishlist.UserId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(wishlist.Title, Is.EqualTo("The Great Gatsby"));
            Assert.That(wishlist.Author, Is.EqualTo("F. Scott Fitzgerald"));
            Assert.That(wishlist.Priority, Is.EqualTo(3));
            Assert.That(wishlist.IsAcquired, Is.False);
            Assert.That(wishlist.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void MarkAsAcquired_SetsIsAcquiredToTrue()
    {
        // Arrange
        var wishlist = new Wishlist
        {
            IsAcquired = false
        };

        // Act
        wishlist.MarkAsAcquired();

        // Assert
        Assert.That(wishlist.IsAcquired, Is.True);
    }

    [Test]
    public void Wishlist_WithISBN_StoresISBNCorrectly()
    {
        // Arrange & Act
        var wishlist = new Wishlist
        {
            ISBN = "978-0-7432-7356-5"
        };

        // Assert
        Assert.That(wishlist.ISBN, Is.EqualTo("978-0-7432-7356-5"));
    }

    [Test]
    public void Wishlist_WithGenre_StoresGenreCorrectly()
    {
        // Arrange & Act
        var wishlist = new Wishlist
        {
            Genre = Genre.Mystery
        };

        // Assert
        Assert.That(wishlist.Genre, Is.EqualTo(Genre.Mystery));
    }

    [Test]
    public void Wishlist_WithNullGenre_AllowsNull()
    {
        // Arrange & Act
        var wishlist = new Wishlist
        {
            Genre = null
        };

        // Assert
        Assert.That(wishlist.Genre, Is.Null);
    }

    [Test]
    public void Wishlist_WithNotes_StoresNotesCorrectly()
    {
        // Arrange & Act
        var wishlist = new Wishlist
        {
            Notes = "Recommended by a friend"
        };

        // Assert
        Assert.That(wishlist.Notes, Is.EqualTo("Recommended by a friend"));
    }

    [Test]
    public void Wishlist_WithPriority1_StoresPriorityCorrectly()
    {
        // Arrange & Act
        var wishlist = new Wishlist
        {
            Priority = 1
        };

        // Assert
        Assert.That(wishlist.Priority, Is.EqualTo(1));
    }

    [Test]
    public void Wishlist_WithPriority5_StoresPriorityCorrectly()
    {
        // Arrange & Act
        var wishlist = new Wishlist
        {
            Priority = 5
        };

        // Assert
        Assert.That(wishlist.Priority, Is.EqualTo(5));
    }

    [Test]
    public void Wishlist_DefaultPriority_Is3()
    {
        // Arrange & Act
        var wishlist = new Wishlist();

        // Assert
        Assert.That(wishlist.Priority, Is.EqualTo(3));
    }

    [Test]
    public void Wishlist_AllPrioritiesFrom1To5_CanBeSet()
    {
        // Arrange & Act & Assert
        for (int priority = 1; priority <= 5; priority++)
        {
            var wishlist = new Wishlist { Priority = priority };
            Assert.That(wishlist.Priority, Is.EqualTo(priority));
        }
    }

    [Test]
    public void Wishlist_AllGenresCanBeAssigned()
    {
        // Arrange & Act & Assert
        var genres = Enum.GetValues<Genre>();
        foreach (var genre in genres)
        {
            var wishlist = new Wishlist { Genre = genre };
            Assert.That(wishlist.Genre, Is.EqualTo(genre));
        }
    }
}
