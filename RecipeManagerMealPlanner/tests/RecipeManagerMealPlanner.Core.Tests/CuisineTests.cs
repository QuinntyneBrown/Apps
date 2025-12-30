// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RecipeManagerMealPlanner.Core.Tests;

public class CuisineTests
{
    [Test]
    public void Cuisine_American_CanBeAssigned()
    {
        var cuisine = Cuisine.American;
        Assert.That(cuisine, Is.EqualTo(Cuisine.American));
        Assert.That((int)cuisine, Is.EqualTo(0));
    }

    [Test]
    public void Cuisine_Italian_CanBeAssigned()
    {
        var cuisine = Cuisine.Italian;
        Assert.That(cuisine, Is.EqualTo(Cuisine.Italian));
        Assert.That((int)cuisine, Is.EqualTo(1));
    }

    [Test]
    public void Cuisine_Mexican_CanBeAssigned()
    {
        var cuisine = Cuisine.Mexican;
        Assert.That(cuisine, Is.EqualTo(Cuisine.Mexican));
        Assert.That((int)cuisine, Is.EqualTo(2));
    }

    [Test]
    public void Cuisine_Chinese_CanBeAssigned()
    {
        var cuisine = Cuisine.Chinese;
        Assert.That(cuisine, Is.EqualTo(Cuisine.Chinese));
        Assert.That((int)cuisine, Is.EqualTo(3));
    }

    [Test]
    public void Cuisine_Japanese_CanBeAssigned()
    {
        var cuisine = Cuisine.Japanese;
        Assert.That(cuisine, Is.EqualTo(Cuisine.Japanese));
        Assert.That((int)cuisine, Is.EqualTo(4));
    }

    [Test]
    public void Cuisine_Indian_CanBeAssigned()
    {
        var cuisine = Cuisine.Indian;
        Assert.That(cuisine, Is.EqualTo(Cuisine.Indian));
        Assert.That((int)cuisine, Is.EqualTo(5));
    }

    [Test]
    public void Cuisine_French_CanBeAssigned()
    {
        var cuisine = Cuisine.French;
        Assert.That(cuisine, Is.EqualTo(Cuisine.French));
        Assert.That((int)cuisine, Is.EqualTo(6));
    }

    [Test]
    public void Cuisine_Thai_CanBeAssigned()
    {
        var cuisine = Cuisine.Thai;
        Assert.That(cuisine, Is.EqualTo(Cuisine.Thai));
        Assert.That((int)cuisine, Is.EqualTo(7));
    }

    [Test]
    public void Cuisine_Mediterranean_CanBeAssigned()
    {
        var cuisine = Cuisine.Mediterranean;
        Assert.That(cuisine, Is.EqualTo(Cuisine.Mediterranean));
        Assert.That((int)cuisine, Is.EqualTo(8));
    }

    [Test]
    public void Cuisine_Other_CanBeAssigned()
    {
        var cuisine = Cuisine.Other;
        Assert.That(cuisine, Is.EqualTo(Cuisine.Other));
        Assert.That((int)cuisine, Is.EqualTo(9));
    }

    [Test]
    public void Cuisine_AllValues_AreUnique()
    {
        var values = Enum.GetValues<Cuisine>();
        var uniqueValues = values.Distinct().ToList();
        Assert.That(uniqueValues.Count, Is.EqualTo(values.Length));
    }

    [Test]
    public void Cuisine_HasExpectedNumberOfValues()
    {
        var values = Enum.GetValues<Cuisine>();
        Assert.That(values.Length, Is.EqualTo(10));
    }
}
