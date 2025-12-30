// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ConversationStarterApp.Core.Tests;

public class DepthTests
{
    [Test]
    public void Depth_Surface_HasCorrectValue()
    {
        // Arrange & Act
        var depth = Depth.Surface;

        // Assert
        Assert.That((int)depth, Is.EqualTo(0));
    }

    [Test]
    public void Depth_Moderate_HasCorrectValue()
    {
        // Arrange & Act
        var depth = Depth.Moderate;

        // Assert
        Assert.That((int)depth, Is.EqualTo(1));
    }

    [Test]
    public void Depth_Deep_HasCorrectValue()
    {
        // Arrange & Act
        var depth = Depth.Deep;

        // Assert
        Assert.That((int)depth, Is.EqualTo(2));
    }

    [Test]
    public void Depth_Intimate_HasCorrectValue()
    {
        // Arrange & Act
        var depth = Depth.Intimate;

        // Assert
        Assert.That((int)depth, Is.EqualTo(3));
    }

    [Test]
    public void Depth_AllValues_CanBeAssigned()
    {
        // Arrange & Act
        var surface = Depth.Surface;
        var moderate = Depth.Moderate;
        var deep = Depth.Deep;
        var intimate = Depth.Intimate;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(surface, Is.EqualTo(Depth.Surface));
            Assert.That(moderate, Is.EqualTo(Depth.Moderate));
            Assert.That(deep, Is.EqualTo(Depth.Deep));
            Assert.That(intimate, Is.EqualTo(Depth.Intimate));
        });
    }
}
