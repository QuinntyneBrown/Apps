// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ConversationStarterApp.Core.Tests;

public class CategoryTests
{
    [Test]
    public void Category_Icebreaker_HasCorrectValue()
    {
        // Arrange & Act
        var category = Category.Icebreaker;

        // Assert
        Assert.That((int)category, Is.EqualTo(0));
    }

    [Test]
    public void Category_Deep_HasCorrectValue()
    {
        // Arrange & Act
        var category = Category.Deep;

        // Assert
        Assert.That((int)category, Is.EqualTo(1));
    }

    [Test]
    public void Category_Fun_HasCorrectValue()
    {
        // Arrange & Act
        var category = Category.Fun;

        // Assert
        Assert.That((int)category, Is.EqualTo(2));
    }

    [Test]
    public void Category_Relationship_HasCorrectValue()
    {
        // Arrange & Act
        var category = Category.Relationship;

        // Assert
        Assert.That((int)category, Is.EqualTo(3));
    }

    [Test]
    public void Category_Reflective_HasCorrectValue()
    {
        // Arrange & Act
        var category = Category.Reflective;

        // Assert
        Assert.That((int)category, Is.EqualTo(4));
    }

    [Test]
    public void Category_Hypothetical_HasCorrectValue()
    {
        // Arrange & Act
        var category = Category.Hypothetical;

        // Assert
        Assert.That((int)category, Is.EqualTo(5));
    }

    [Test]
    public void Category_ValuesAndBeliefs_HasCorrectValue()
    {
        // Arrange & Act
        var category = Category.ValuesAndBeliefs;

        // Assert
        Assert.That((int)category, Is.EqualTo(6));
    }

    [Test]
    public void Category_DreamsAndAspirations_HasCorrectValue()
    {
        // Arrange & Act
        var category = Category.DreamsAndAspirations;

        // Assert
        Assert.That((int)category, Is.EqualTo(7));
    }

    [Test]
    public void Category_PastExperiences_HasCorrectValue()
    {
        // Arrange & Act
        var category = Category.PastExperiences;

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
        var icebreaker = Category.Icebreaker;
        var deep = Category.Deep;
        var fun = Category.Fun;
        var relationship = Category.Relationship;
        var reflective = Category.Reflective;
        var hypothetical = Category.Hypothetical;
        var valuesAndBeliefs = Category.ValuesAndBeliefs;
        var dreamsAndAspirations = Category.DreamsAndAspirations;
        var pastExperiences = Category.PastExperiences;
        var other = Category.Other;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(icebreaker, Is.EqualTo(Category.Icebreaker));
            Assert.That(deep, Is.EqualTo(Category.Deep));
            Assert.That(fun, Is.EqualTo(Category.Fun));
            Assert.That(relationship, Is.EqualTo(Category.Relationship));
            Assert.That(reflective, Is.EqualTo(Category.Reflective));
            Assert.That(hypothetical, Is.EqualTo(Category.Hypothetical));
            Assert.That(valuesAndBeliefs, Is.EqualTo(Category.ValuesAndBeliefs));
            Assert.That(dreamsAndAspirations, Is.EqualTo(Category.DreamsAndAspirations));
            Assert.That(pastExperiences, Is.EqualTo(Category.PastExperiences));
            Assert.That(other, Is.EqualTo(Category.Other));
        });
    }
}
