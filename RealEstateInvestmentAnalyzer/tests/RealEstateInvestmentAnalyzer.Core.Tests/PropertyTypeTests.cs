// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RealEstateInvestmentAnalyzer.Core.Tests;

public class PropertyTypeTests
{
    [Test]
    public void PropertyType_SingleFamily_CanBeAssigned()
    {
        // Arrange & Act
        var propertyType = PropertyType.SingleFamily;

        // Assert
        Assert.That(propertyType, Is.EqualTo(PropertyType.SingleFamily));
        Assert.That((int)propertyType, Is.EqualTo(0));
    }

    [Test]
    public void PropertyType_MultiFamily_CanBeAssigned()
    {
        // Arrange & Act
        var propertyType = PropertyType.MultiFamily;

        // Assert
        Assert.That(propertyType, Is.EqualTo(PropertyType.MultiFamily));
        Assert.That((int)propertyType, Is.EqualTo(1));
    }

    [Test]
    public void PropertyType_Condo_CanBeAssigned()
    {
        // Arrange & Act
        var propertyType = PropertyType.Condo;

        // Assert
        Assert.That(propertyType, Is.EqualTo(PropertyType.Condo));
        Assert.That((int)propertyType, Is.EqualTo(2));
    }

    [Test]
    public void PropertyType_Townhouse_CanBeAssigned()
    {
        // Arrange & Act
        var propertyType = PropertyType.Townhouse;

        // Assert
        Assert.That(propertyType, Is.EqualTo(PropertyType.Townhouse));
        Assert.That((int)propertyType, Is.EqualTo(3));
    }

    [Test]
    public void PropertyType_Commercial_CanBeAssigned()
    {
        // Arrange & Act
        var propertyType = PropertyType.Commercial;

        // Assert
        Assert.That(propertyType, Is.EqualTo(PropertyType.Commercial));
        Assert.That((int)propertyType, Is.EqualTo(4));
    }

    [Test]
    public void PropertyType_Land_CanBeAssigned()
    {
        // Arrange & Act
        var propertyType = PropertyType.Land;

        // Assert
        Assert.That(propertyType, Is.EqualTo(PropertyType.Land));
        Assert.That((int)propertyType, Is.EqualTo(5));
    }

    [Test]
    public void PropertyType_AllValues_AreUnique()
    {
        // Arrange
        var values = Enum.GetValues<PropertyType>();

        // Act
        var uniqueValues = values.Distinct().ToList();

        // Assert
        Assert.That(uniqueValues.Count, Is.EqualTo(values.Length));
    }

    [Test]
    public void PropertyType_HasExpectedNumberOfValues()
    {
        // Arrange & Act
        var values = Enum.GetValues<PropertyType>();

        // Assert
        Assert.That(values.Length, Is.EqualTo(6));
    }
}
