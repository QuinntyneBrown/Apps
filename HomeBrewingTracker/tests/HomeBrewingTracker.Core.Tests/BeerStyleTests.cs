// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeBrewingTracker.Core.Tests;

public class BeerStyleTests
{
    [Test]
    public void BeerStyle_PaleAle_HasCorrectValue()
    {
        // Arrange & Act
        var style = BeerStyle.PaleAle;

        // Assert
        Assert.That(style, Is.EqualTo(BeerStyle.PaleAle));
        Assert.That((int)style, Is.EqualTo(0));
    }

    [Test]
    public void BeerStyle_IPA_HasCorrectValue()
    {
        // Arrange & Act
        var style = BeerStyle.IPA;

        // Assert
        Assert.That(style, Is.EqualTo(BeerStyle.IPA));
        Assert.That((int)style, Is.EqualTo(1));
    }

    [Test]
    public void BeerStyle_Stout_HasCorrectValue()
    {
        // Arrange & Act
        var style = BeerStyle.Stout;

        // Assert
        Assert.That(style, Is.EqualTo(BeerStyle.Stout));
        Assert.That((int)style, Is.EqualTo(2));
    }

    [Test]
    public void BeerStyle_Porter_HasCorrectValue()
    {
        // Arrange & Act
        var style = BeerStyle.Porter;

        // Assert
        Assert.That(style, Is.EqualTo(BeerStyle.Porter));
        Assert.That((int)style, Is.EqualTo(3));
    }

    [Test]
    public void BeerStyle_Lager_HasCorrectValue()
    {
        // Arrange & Act
        var style = BeerStyle.Lager;

        // Assert
        Assert.That(style, Is.EqualTo(BeerStyle.Lager));
        Assert.That((int)style, Is.EqualTo(4));
    }

    [Test]
    public void BeerStyle_Pilsner_HasCorrectValue()
    {
        // Arrange & Act
        var style = BeerStyle.Pilsner;

        // Assert
        Assert.That(style, Is.EqualTo(BeerStyle.Pilsner));
        Assert.That((int)style, Is.EqualTo(5));
    }

    [Test]
    public void BeerStyle_Wheat_HasCorrectValue()
    {
        // Arrange & Act
        var style = BeerStyle.Wheat;

        // Assert
        Assert.That(style, Is.EqualTo(BeerStyle.Wheat));
        Assert.That((int)style, Is.EqualTo(6));
    }

    [Test]
    public void BeerStyle_Belgian_HasCorrectValue()
    {
        // Arrange & Act
        var style = BeerStyle.Belgian;

        // Assert
        Assert.That(style, Is.EqualTo(BeerStyle.Belgian));
        Assert.That((int)style, Is.EqualTo(7));
    }

    [Test]
    public void BeerStyle_Sour_HasCorrectValue()
    {
        // Arrange & Act
        var style = BeerStyle.Sour;

        // Assert
        Assert.That(style, Is.EqualTo(BeerStyle.Sour));
        Assert.That((int)style, Is.EqualTo(8));
    }

    [Test]
    public void BeerStyle_Amber_HasCorrectValue()
    {
        // Arrange & Act
        var style = BeerStyle.Amber;

        // Assert
        Assert.That(style, Is.EqualTo(BeerStyle.Amber));
        Assert.That((int)style, Is.EqualTo(9));
    }

    [Test]
    public void BeerStyle_BrownAle_HasCorrectValue()
    {
        // Arrange & Act
        var style = BeerStyle.BrownAle;

        // Assert
        Assert.That(style, Is.EqualTo(BeerStyle.BrownAle));
        Assert.That((int)style, Is.EqualTo(10));
    }

    [Test]
    public void BeerStyle_Other_HasCorrectValue()
    {
        // Arrange & Act
        var style = BeerStyle.Other;

        // Assert
        Assert.That(style, Is.EqualTo(BeerStyle.Other));
        Assert.That((int)style, Is.EqualTo(11));
    }

    [Test]
    public void BeerStyle_AllValues_CanBeAssigned()
    {
        // Arrange & Act
        var styles = new[]
        {
            BeerStyle.PaleAle,
            BeerStyle.IPA,
            BeerStyle.Stout,
            BeerStyle.Porter,
            BeerStyle.Lager,
            BeerStyle.Pilsner,
            BeerStyle.Wheat,
            BeerStyle.Belgian,
            BeerStyle.Sour,
            BeerStyle.Amber,
            BeerStyle.BrownAle,
            BeerStyle.Other
        };

        // Assert
        Assert.That(styles, Has.Length.EqualTo(12));
    }

    [Test]
    public void BeerStyle_CanBeCompared()
    {
        // Arrange
        var style1 = BeerStyle.IPA;
        var style2 = BeerStyle.IPA;
        var style3 = BeerStyle.Stout;

        // Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(style1, Is.EqualTo(style2));
            Assert.That(style1, Is.Not.EqualTo(style3));
        });
    }
}
