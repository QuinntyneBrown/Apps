// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BBQGrillingRecipeBook.Core.Tests;

public class MeatTypeTests
{
    [Test]
    public void MeatType_Beef_HasCorrectValue()
    {
        // Arrange & Act
        var meatType = MeatType.Beef;

        // Assert
        Assert.That((int)meatType, Is.EqualTo(0));
    }

    [Test]
    public void MeatType_Pork_HasCorrectValue()
    {
        // Arrange & Act
        var meatType = MeatType.Pork;

        // Assert
        Assert.That((int)meatType, Is.EqualTo(1));
    }

    [Test]
    public void MeatType_Chicken_HasCorrectValue()
    {
        // Arrange & Act
        var meatType = MeatType.Chicken;

        // Assert
        Assert.That((int)meatType, Is.EqualTo(2));
    }

    [Test]
    public void MeatType_Turkey_HasCorrectValue()
    {
        // Arrange & Act
        var meatType = MeatType.Turkey;

        // Assert
        Assert.That((int)meatType, Is.EqualTo(3));
    }

    [Test]
    public void MeatType_Lamb_HasCorrectValue()
    {
        // Arrange & Act
        var meatType = MeatType.Lamb;

        // Assert
        Assert.That((int)meatType, Is.EqualTo(4));
    }

    [Test]
    public void MeatType_Seafood_HasCorrectValue()
    {
        // Arrange & Act
        var meatType = MeatType.Seafood;

        // Assert
        Assert.That((int)meatType, Is.EqualTo(5));
    }

    [Test]
    public void MeatType_Vegetables_HasCorrectValue()
    {
        // Arrange & Act
        var meatType = MeatType.Vegetables;

        // Assert
        Assert.That((int)meatType, Is.EqualTo(6));
    }

    [Test]
    public void MeatType_Mixed_HasCorrectValue()
    {
        // Arrange & Act
        var meatType = MeatType.Mixed;

        // Assert
        Assert.That((int)meatType, Is.EqualTo(7));
    }

    [Test]
    public void MeatType_Other_HasCorrectValue()
    {
        // Arrange & Act
        var meatType = MeatType.Other;

        // Assert
        Assert.That((int)meatType, Is.EqualTo(8));
    }

    [Test]
    public void MeatType_CanBeAssignedToVariable()
    {
        // Arrange & Act
        MeatType meatType = MeatType.Beef;

        // Assert
        Assert.That(meatType, Is.EqualTo(MeatType.Beef));
    }

    [Test]
    public void MeatType_AllValues_CanBeEnumerated()
    {
        // Arrange & Act
        var allValues = Enum.GetValues<MeatType>();

        // Assert
        Assert.That(allValues, Has.Length.EqualTo(9));
        Assert.That(allValues, Contains.Item(MeatType.Beef));
        Assert.That(allValues, Contains.Item(MeatType.Pork));
        Assert.That(allValues, Contains.Item(MeatType.Chicken));
        Assert.That(allValues, Contains.Item(MeatType.Turkey));
        Assert.That(allValues, Contains.Item(MeatType.Lamb));
        Assert.That(allValues, Contains.Item(MeatType.Seafood));
        Assert.That(allValues, Contains.Item(MeatType.Vegetables));
        Assert.That(allValues, Contains.Item(MeatType.Mixed));
        Assert.That(allValues, Contains.Item(MeatType.Other));
    }
}
