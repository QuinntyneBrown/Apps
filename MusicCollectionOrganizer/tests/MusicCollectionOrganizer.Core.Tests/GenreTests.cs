// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MusicCollectionOrganizer.Core.Tests;

public class GenreTests
{
    [Test]
    public void Genre_Rock_HasCorrectValue()
    {
        // Arrange & Act
        var genre = Genre.Rock;

        // Assert
        Assert.That(genre, Is.EqualTo(Genre.Rock));
        Assert.That((int)genre, Is.EqualTo(0));
    }

    [Test]
    public void Genre_Pop_HasCorrectValue()
    {
        // Arrange & Act
        var genre = Genre.Pop;

        // Assert
        Assert.That(genre, Is.EqualTo(Genre.Pop));
        Assert.That((int)genre, Is.EqualTo(1));
    }

    [Test]
    public void Genre_Jazz_HasCorrectValue()
    {
        // Arrange & Act
        var genre = Genre.Jazz;

        // Assert
        Assert.That(genre, Is.EqualTo(Genre.Jazz));
        Assert.That((int)genre, Is.EqualTo(2));
    }

    [Test]
    public void Genre_Classical_HasCorrectValue()
    {
        // Arrange & Act
        var genre = Genre.Classical;

        // Assert
        Assert.That(genre, Is.EqualTo(Genre.Classical));
        Assert.That((int)genre, Is.EqualTo(3));
    }

    [Test]
    public void Genre_HipHop_HasCorrectValue()
    {
        // Arrange & Act
        var genre = Genre.HipHop;

        // Assert
        Assert.That(genre, Is.EqualTo(Genre.HipHop));
        Assert.That((int)genre, Is.EqualTo(4));
    }

    [Test]
    public void Genre_Electronic_HasCorrectValue()
    {
        // Arrange & Act
        var genre = Genre.Electronic;

        // Assert
        Assert.That(genre, Is.EqualTo(Genre.Electronic));
        Assert.That((int)genre, Is.EqualTo(5));
    }

    [Test]
    public void Genre_Country_HasCorrectValue()
    {
        // Arrange & Act
        var genre = Genre.Country;

        // Assert
        Assert.That(genre, Is.EqualTo(Genre.Country));
        Assert.That((int)genre, Is.EqualTo(6));
    }

    [Test]
    public void Genre_Blues_HasCorrectValue()
    {
        // Arrange & Act
        var genre = Genre.Blues;

        // Assert
        Assert.That(genre, Is.EqualTo(Genre.Blues));
        Assert.That((int)genre, Is.EqualTo(7));
    }

    [Test]
    public void Genre_Metal_HasCorrectValue()
    {
        // Arrange & Act
        var genre = Genre.Metal;

        // Assert
        Assert.That(genre, Is.EqualTo(Genre.Metal));
        Assert.That((int)genre, Is.EqualTo(8));
    }

    [Test]
    public void Genre_Alternative_HasCorrectValue()
    {
        // Arrange & Act
        var genre = Genre.Alternative;

        // Assert
        Assert.That(genre, Is.EqualTo(Genre.Alternative));
        Assert.That((int)genre, Is.EqualTo(9));
    }

    [Test]
    public void Genre_Other_HasCorrectValue()
    {
        // Arrange & Act
        var genre = Genre.Other;

        // Assert
        Assert.That(genre, Is.EqualTo(Genre.Other));
        Assert.That((int)genre, Is.EqualTo(10));
    }

    [Test]
    public void Genre_AllValues_CanBeAssigned()
    {
        // Arrange
        var genres = new[]
        {
            Genre.Rock,
            Genre.Pop,
            Genre.Jazz,
            Genre.Classical,
            Genre.HipHop,
            Genre.Electronic,
            Genre.Country,
            Genre.Blues,
            Genre.Metal,
            Genre.Alternative,
            Genre.Other
        };

        // Act & Assert
        foreach (var genre in genres)
        {
            var album = new Album { Genre = genre };
            Assert.That(album.Genre, Is.EqualTo(genre));
        }
    }
}
