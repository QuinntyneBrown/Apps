// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace GiftIdeaTracker.Core.Tests;

public class OccasionTests
{
    [Test]
    public void Occasion_Birthday_HasCorrectValue()
    {
        // Arrange & Act
        var occasion = Occasion.Birthday;

        // Assert
        Assert.That(occasion, Is.EqualTo(Occasion.Birthday));
        Assert.That((int)occasion, Is.EqualTo(0));
    }

    [Test]
    public void Occasion_Anniversary_HasCorrectValue()
    {
        // Arrange & Act
        var occasion = Occasion.Anniversary;

        // Assert
        Assert.That(occasion, Is.EqualTo(Occasion.Anniversary));
        Assert.That((int)occasion, Is.EqualTo(1));
    }

    [Test]
    public void Occasion_Christmas_HasCorrectValue()
    {
        // Arrange & Act
        var occasion = Occasion.Christmas;

        // Assert
        Assert.That(occasion, Is.EqualTo(Occasion.Christmas));
        Assert.That((int)occasion, Is.EqualTo(2));
    }

    [Test]
    public void Occasion_Graduation_HasCorrectValue()
    {
        // Arrange & Act
        var occasion = Occasion.Graduation;

        // Assert
        Assert.That(occasion, Is.EqualTo(Occasion.Graduation));
        Assert.That((int)occasion, Is.EqualTo(3));
    }

    [Test]
    public void Occasion_Wedding_HasCorrectValue()
    {
        // Arrange & Act
        var occasion = Occasion.Wedding;

        // Assert
        Assert.That(occasion, Is.EqualTo(Occasion.Wedding));
        Assert.That((int)occasion, Is.EqualTo(4));
    }

    [Test]
    public void Occasion_Other_HasCorrectValue()
    {
        // Arrange & Act
        var occasion = Occasion.Other;

        // Assert
        Assert.That(occasion, Is.EqualTo(Occasion.Other));
        Assert.That((int)occasion, Is.EqualTo(5));
    }

    [Test]
    public void Occasion_AllValues_CanBeAssigned()
    {
        // Arrange & Act
        var occasions = new[]
        {
            Occasion.Birthday,
            Occasion.Anniversary,
            Occasion.Christmas,
            Occasion.Graduation,
            Occasion.Wedding,
            Occasion.Other
        };

        // Assert
        Assert.That(occasions, Has.Length.EqualTo(6));
    }

    [Test]
    public void Occasion_CanBeCompared()
    {
        // Arrange
        var occasion1 = Occasion.Birthday;
        var occasion2 = Occasion.Birthday;
        var occasion3 = Occasion.Christmas;

        // Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(occasion1, Is.EqualTo(occasion2));
            Assert.That(occasion1, Is.Not.EqualTo(occasion3));
        });
    }
}
