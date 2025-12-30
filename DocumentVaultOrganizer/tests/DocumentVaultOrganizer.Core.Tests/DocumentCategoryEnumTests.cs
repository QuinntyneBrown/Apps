// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace DocumentVaultOrganizer.Core.Tests;

public class DocumentCategoryEnumTests
{
    [Test]
    public void Personal_HasValue_Zero()
    {
        // Arrange & Act
        var value = (int)DocumentCategoryEnum.Personal;

        // Assert
        Assert.That(value, Is.EqualTo(0));
    }

    [Test]
    public void Financial_HasValue_One()
    {
        // Arrange & Act
        var value = (int)DocumentCategoryEnum.Financial;

        // Assert
        Assert.That(value, Is.EqualTo(1));
    }

    [Test]
    public void Legal_HasValue_Two()
    {
        // Arrange & Act
        var value = (int)DocumentCategoryEnum.Legal;

        // Assert
        Assert.That(value, Is.EqualTo(2));
    }

    [Test]
    public void Medical_HasValue_Three()
    {
        // Arrange & Act
        var value = (int)DocumentCategoryEnum.Medical;

        // Assert
        Assert.That(value, Is.EqualTo(3));
    }

    [Test]
    public void Insurance_HasValue_Four()
    {
        // Arrange & Act
        var value = (int)DocumentCategoryEnum.Insurance;

        // Assert
        Assert.That(value, Is.EqualTo(4));
    }

    [Test]
    public void Tax_HasValue_Five()
    {
        // Arrange & Act
        var value = (int)DocumentCategoryEnum.Tax;

        // Assert
        Assert.That(value, Is.EqualTo(5));
    }

    [Test]
    public void Property_HasValue_Six()
    {
        // Arrange & Act
        var value = (int)DocumentCategoryEnum.Property;

        // Assert
        Assert.That(value, Is.EqualTo(6));
    }

    [Test]
    public void Other_HasValue_Seven()
    {
        // Arrange & Act
        var value = (int)DocumentCategoryEnum.Other;

        // Assert
        Assert.That(value, Is.EqualTo(7));
    }

    [Test]
    public void AllEnumValues_CanBeAssigned()
    {
        // Arrange & Act
        var personal = DocumentCategoryEnum.Personal;
        var financial = DocumentCategoryEnum.Financial;
        var legal = DocumentCategoryEnum.Legal;
        var medical = DocumentCategoryEnum.Medical;
        var insurance = DocumentCategoryEnum.Insurance;
        var tax = DocumentCategoryEnum.Tax;
        var property = DocumentCategoryEnum.Property;
        var other = DocumentCategoryEnum.Other;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(personal, Is.EqualTo(DocumentCategoryEnum.Personal));
            Assert.That(financial, Is.EqualTo(DocumentCategoryEnum.Financial));
            Assert.That(legal, Is.EqualTo(DocumentCategoryEnum.Legal));
            Assert.That(medical, Is.EqualTo(DocumentCategoryEnum.Medical));
            Assert.That(insurance, Is.EqualTo(DocumentCategoryEnum.Insurance));
            Assert.That(tax, Is.EqualTo(DocumentCategoryEnum.Tax));
            Assert.That(property, Is.EqualTo(DocumentCategoryEnum.Property));
            Assert.That(other, Is.EqualTo(DocumentCategoryEnum.Other));
        });
    }

    [Test]
    public void EnumValues_AreDistinct()
    {
        // Arrange
        var values = Enum.GetValues<DocumentCategoryEnum>();

        // Act
        var distinctValues = values.Distinct().ToList();

        // Assert
        Assert.That(distinctValues.Count, Is.EqualTo(values.Length));
    }
}
