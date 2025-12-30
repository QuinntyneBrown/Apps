// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyTreeBuilder.Core.Tests;

public class GenderTests
{
    [Test]
    public void Male_HasValue_Zero()
    {
        // Arrange & Act
        var value = (int)Gender.Male;

        // Assert
        Assert.That(value, Is.EqualTo(0));
    }

    [Test]
    public void Female_HasValue_One()
    {
        // Arrange & Act
        var value = (int)Gender.Female;

        // Assert
        Assert.That(value, Is.EqualTo(1));
    }

    [Test]
    public void Other_HasValue_Two()
    {
        // Arrange & Act
        var value = (int)Gender.Other;

        // Assert
        Assert.That(value, Is.EqualTo(2));
    }

    [Test]
    public void AllEnumValues_CanBeAssigned()
    {
        // Arrange & Act
        var male = Gender.Male;
        var female = Gender.Female;
        var other = Gender.Other;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(male, Is.EqualTo(Gender.Male));
            Assert.That(female, Is.EqualTo(Gender.Female));
            Assert.That(other, Is.EqualTo(Gender.Other));
        });
    }

    [Test]
    public void EnumValues_AreDistinct()
    {
        // Arrange
        var values = Enum.GetValues<Gender>();

        // Act
        var distinctValues = values.Distinct().ToList();

        // Assert
        Assert.That(distinctValues.Count, Is.EqualTo(values.Length));
    }
}
