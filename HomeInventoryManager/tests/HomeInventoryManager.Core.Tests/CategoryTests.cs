// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeInventoryManager.Core.Tests;

public class CategoryTests
{
    [Test]
    public void Category_Electronics_HasCorrectValue()
    {
        // Arrange & Act
        var category = Category.Electronics;

        // Assert
        Assert.That((int)category, Is.EqualTo(0));
    }

    [Test]
    public void Category_Furniture_HasCorrectValue()
    {
        // Arrange & Act
        var category = Category.Furniture;

        // Assert
        Assert.That((int)category, Is.EqualTo(1));
    }

    [Test]
    public void Category_Appliances_HasCorrectValue()
    {
        // Arrange & Act
        var category = Category.Appliances;

        // Assert
        Assert.That((int)category, Is.EqualTo(2));
    }

    [Test]
    public void Category_Jewelry_HasCorrectValue()
    {
        // Arrange & Act
        var category = Category.Jewelry;

        // Assert
        Assert.That((int)category, Is.EqualTo(3));
    }

    [Test]
    public void Category_Collectibles_HasCorrectValue()
    {
        // Arrange & Act
        var category = Category.Collectibles;

        // Assert
        Assert.That((int)category, Is.EqualTo(4));
    }

    [Test]
    public void Category_Tools_HasCorrectValue()
    {
        // Arrange & Act
        var category = Category.Tools;

        // Assert
        Assert.That((int)category, Is.EqualTo(5));
    }

    [Test]
    public void Category_Clothing_HasCorrectValue()
    {
        // Arrange & Act
        var category = Category.Clothing;

        // Assert
        Assert.That((int)category, Is.EqualTo(6));
    }

    [Test]
    public void Category_Books_HasCorrectValue()
    {
        // Arrange & Act
        var category = Category.Books;

        // Assert
        Assert.That((int)category, Is.EqualTo(7));
    }

    [Test]
    public void Category_Sports_HasCorrectValue()
    {
        // Arrange & Act
        var category = Category.Sports;

        // Assert
        Assert.That((int)category, Is.EqualTo(8));
    }

    [Test]
    public void Category_Other_HasCorrectValue()
    {
        // Arrange & Act
        var category = Category.Other;

        // Assert
        Assert.That((int)category, Is.EqualTo(9));
    }

    [Test]
    public void Category_AllValues_CanBeAssigned()
    {
        // Arrange & Act
        var electronics = Category.Electronics;
        var furniture = Category.Furniture;
        var appliances = Category.Appliances;
        var jewelry = Category.Jewelry;
        var collectibles = Category.Collectibles;
        var tools = Category.Tools;
        var clothing = Category.Clothing;
        var books = Category.Books;
        var sports = Category.Sports;
        var other = Category.Other;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(electronics, Is.EqualTo(Category.Electronics));
            Assert.That(furniture, Is.EqualTo(Category.Furniture));
            Assert.That(appliances, Is.EqualTo(Category.Appliances));
            Assert.That(jewelry, Is.EqualTo(Category.Jewelry));
            Assert.That(collectibles, Is.EqualTo(Category.Collectibles));
            Assert.That(tools, Is.EqualTo(Category.Tools));
            Assert.That(clothing, Is.EqualTo(Category.Clothing));
            Assert.That(books, Is.EqualTo(Category.Books));
            Assert.That(sports, Is.EqualTo(Category.Sports));
            Assert.That(other, Is.EqualTo(Category.Other));
        });
    }

    [Test]
    public void Category_ToString_ReturnsCorrectName()
    {
        // Arrange & Act
        var names = new[]
        {
            Category.Electronics.ToString(),
            Category.Furniture.ToString(),
            Category.Appliances.ToString(),
            Category.Jewelry.ToString(),
            Category.Collectibles.ToString(),
            Category.Tools.ToString(),
            Category.Clothing.ToString(),
            Category.Books.ToString(),
            Category.Sports.ToString(),
            Category.Other.ToString()
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(names[0], Is.EqualTo("Electronics"));
            Assert.That(names[1], Is.EqualTo("Furniture"));
            Assert.That(names[2], Is.EqualTo("Appliances"));
            Assert.That(names[3], Is.EqualTo("Jewelry"));
            Assert.That(names[4], Is.EqualTo("Collectibles"));
            Assert.That(names[5], Is.EqualTo("Tools"));
            Assert.That(names[6], Is.EqualTo("Clothing"));
            Assert.That(names[7], Is.EqualTo("Books"));
            Assert.That(names[8], Is.EqualTo("Sports"));
            Assert.That(names[9], Is.EqualTo("Other"));
        });
    }

    [Test]
    public void Category_CanBeCompared()
    {
        // Arrange
        var category1 = Category.Electronics;
        var category2 = Category.Electronics;
        var category3 = Category.Furniture;

        // Act & Assert
        Assert.Multiple(() =>
        {
            Assert.That(category1, Is.EqualTo(category2));
            Assert.That(category1, Is.Not.EqualTo(category3));
        });
    }

    [Test]
    public void Category_CanBeUsedInSwitch()
    {
        // Arrange
        var category = Category.Jewelry;
        string result;

        // Act
        result = category switch
        {
            Category.Electronics => "Electronics",
            Category.Furniture => "Furniture",
            Category.Appliances => "Appliances",
            Category.Jewelry => "Jewelry",
            Category.Collectibles => "Collectibles",
            Category.Tools => "Tools",
            Category.Clothing => "Clothing",
            Category.Books => "Books",
            Category.Sports => "Sports",
            Category.Other => "Other",
            _ => "Unknown"
        };

        // Assert
        Assert.That(result, Is.EqualTo("Jewelry"));
    }

    [Test]
    public void Category_EnumParse_WorksCorrectly()
    {
        // Arrange
        var categoryName = "Collectibles";

        // Act
        var parsed = Enum.Parse<Category>(categoryName);

        // Assert
        Assert.That(parsed, Is.EqualTo(Category.Collectibles));
    }
}
