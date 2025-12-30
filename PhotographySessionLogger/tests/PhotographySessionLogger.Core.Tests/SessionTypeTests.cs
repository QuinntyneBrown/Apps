// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PhotographySessionLogger.Core.Tests;

public class SessionTypeTests
{
    [Test]
    public void Portrait_HasCorrectValue()
    {
        // Assert
        Assert.That((int)SessionType.Portrait, Is.EqualTo(0));
    }

    [Test]
    public void Landscape_HasCorrectValue()
    {
        // Assert
        Assert.That((int)SessionType.Landscape, Is.EqualTo(1));
    }

    [Test]
    public void Wedding_HasCorrectValue()
    {
        // Assert
        Assert.That((int)SessionType.Wedding, Is.EqualTo(2));
    }

    [Test]
    public void Event_HasCorrectValue()
    {
        // Assert
        Assert.That((int)SessionType.Event, Is.EqualTo(3));
    }

    [Test]
    public void Product_HasCorrectValue()
    {
        // Assert
        Assert.That((int)SessionType.Product, Is.EqualTo(4));
    }

    [Test]
    public void Wildlife_HasCorrectValue()
    {
        // Assert
        Assert.That((int)SessionType.Wildlife, Is.EqualTo(5));
    }

    [Test]
    public void Sports_HasCorrectValue()
    {
        // Assert
        Assert.That((int)SessionType.Sports, Is.EqualTo(6));
    }

    [Test]
    public void Macro_HasCorrectValue()
    {
        // Assert
        Assert.That((int)SessionType.Macro, Is.EqualTo(7));
    }

    [Test]
    public void Other_HasCorrectValue()
    {
        // Assert
        Assert.That((int)SessionType.Other, Is.EqualTo(8));
    }

    [Test]
    public void AllValues_CanBeAssigned()
    {
        // Arrange & Act
        var portrait = SessionType.Portrait;
        var landscape = SessionType.Landscape;
        var wedding = SessionType.Wedding;
        var evt = SessionType.Event;
        var product = SessionType.Product;
        var wildlife = SessionType.Wildlife;
        var sports = SessionType.Sports;
        var macro = SessionType.Macro;
        var other = SessionType.Other;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(portrait, Is.EqualTo(SessionType.Portrait));
            Assert.That(landscape, Is.EqualTo(SessionType.Landscape));
            Assert.That(wedding, Is.EqualTo(SessionType.Wedding));
            Assert.That(evt, Is.EqualTo(SessionType.Event));
            Assert.That(product, Is.EqualTo(SessionType.Product));
            Assert.That(wildlife, Is.EqualTo(SessionType.Wildlife));
            Assert.That(sports, Is.EqualTo(SessionType.Sports));
            Assert.That(macro, Is.EqualTo(SessionType.Macro));
            Assert.That(other, Is.EqualTo(SessionType.Other));
        });
    }
}
