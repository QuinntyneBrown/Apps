// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace GroceryShoppingListApp.Core.Tests;

public class CategoryTests
{
    [Test]
    public void Category_Produce_HasCorrectValue()
    {
        // Arrange & Act
        var category = Category.Produce;

        // Assert
        Assert.That(category, Is.EqualTo(Category.Produce));
        Assert.That((int)category, Is.EqualTo(0));
    }

    [Test]
    public void Category_Dairy_HasCorrectValue()
    {
        // Arrange & Act
        var category = Category.Dairy;

        // Assert
        Assert.That(category, Is.EqualTo(Category.Dairy));
        Assert.That((int)category, Is.EqualTo(1));
    }

    [Test]
    public void Category_Meat_HasCorrectValue()
    {
        // Arrange & Act
        var category = Category.Meat;

        // Assert
        Assert.That(category, Is.EqualTo(Category.Meat));
        Assert.That((int)category, Is.EqualTo(2));
    }

    [Test]
    public void Category_Bakery_HasCorrectValue()
    {
        // Arrange & Act
        var category = Category.Bakery;

        // Assert
        Assert.That(category, Is.EqualTo(Category.Bakery));
        Assert.That((int)category, Is.EqualTo(3));
    }

    [Test]
    public void Category_Canned_HasCorrectValue()
    {
        // Arrange & Act
        var category = Category.Canned;

        // Assert
        Assert.That(category, Is.EqualTo(Category.Canned));
        Assert.That((int)category, Is.EqualTo(4));
    }

    [Test]
    public void Category_Frozen_HasCorrectValue()
    {
        // Arrange & Act
        var category = Category.Frozen;

        // Assert
        Assert.That(category, Is.EqualTo(Category.Frozen));
        Assert.That((int)category, Is.EqualTo(5));
    }

    [Test]
    public void Category_Snacks_HasCorrectValue()
    {
        // Arrange & Act
        var category = Category.Snacks;

        // Assert
        Assert.That(category, Is.EqualTo(Category.Snacks));
        Assert.That((int)category, Is.EqualTo(6));
    }

    [Test]
    public void Category_Beverages_HasCorrectValue()
    {
        // Arrange & Act
        var category = Category.Beverages;

        // Assert
        Assert.That(category, Is.EqualTo(Category.Beverages));
        Assert.That((int)category, Is.EqualTo(7));
    }

    [Test]
    public void Category_Other_HasCorrectValue()
    {
        // Arrange & Act
        var category = Category.Other;

        // Assert
        Assert.That(category, Is.EqualTo(Category.Other));
        Assert.That((int)category, Is.EqualTo(8));
    }

    [Test]
    public void Category_AllValues_CanBeAssigned()
    {
        // Arrange & Act
        var categories = new[]
        {
            Category.Produce,
            Category.Dairy,
            Category.Meat,
            Category.Bakery,
            Category.Canned,
            Category.Frozen,
            Category.Snacks,
            Category.Beverages,
            Category.Other
        };

        // Assert
        Assert.That(categories, Has.Length.EqualTo(9));
    }

    [Test]
    public void Category_CanBeCompared()
    {
        // Arrange
        var category1 = Category.Produce;
        var category2 = Category.Produce;
        var category3 = Category.Dairy;

        // Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(category1, Is.EqualTo(category2));
            Assert.That(category1, Is.Not.EqualTo(category3));
        });
    }
}
