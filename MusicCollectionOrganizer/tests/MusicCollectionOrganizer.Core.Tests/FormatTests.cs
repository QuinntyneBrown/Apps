// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MusicCollectionOrganizer.Core.Tests;

public class FormatTests
{
    [Test]
    public void Format_CD_HasCorrectValue()
    {
        // Arrange & Act
        var format = Format.CD;

        // Assert
        Assert.That(format, Is.EqualTo(Format.CD));
        Assert.That((int)format, Is.EqualTo(0));
    }

    [Test]
    public void Format_Vinyl_HasCorrectValue()
    {
        // Arrange & Act
        var format = Format.Vinyl;

        // Assert
        Assert.That(format, Is.EqualTo(Format.Vinyl));
        Assert.That((int)format, Is.EqualTo(1));
    }

    [Test]
    public void Format_Cassette_HasCorrectValue()
    {
        // Arrange & Act
        var format = Format.Cassette;

        // Assert
        Assert.That(format, Is.EqualTo(Format.Cassette));
        Assert.That((int)format, Is.EqualTo(2));
    }

    [Test]
    public void Format_Digital_HasCorrectValue()
    {
        // Arrange & Act
        var format = Format.Digital;

        // Assert
        Assert.That(format, Is.EqualTo(Format.Digital));
        Assert.That((int)format, Is.EqualTo(3));
    }

    [Test]
    public void Format_StreamingOnly_HasCorrectValue()
    {
        // Arrange & Act
        var format = Format.StreamingOnly;

        // Assert
        Assert.That(format, Is.EqualTo(Format.StreamingOnly));
        Assert.That((int)format, Is.EqualTo(4));
    }

    [Test]
    public void Format_AllValues_CanBeAssigned()
    {
        // Arrange
        var formats = new[]
        {
            Format.CD,
            Format.Vinyl,
            Format.Cassette,
            Format.Digital,
            Format.StreamingOnly
        };

        // Act & Assert
        foreach (var format in formats)
        {
            var album = new Album { Format = format };
            Assert.That(album.Format, Is.EqualTo(format));
        }
    }
}
