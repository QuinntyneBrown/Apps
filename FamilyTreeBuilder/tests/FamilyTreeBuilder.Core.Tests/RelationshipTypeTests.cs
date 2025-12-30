// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyTreeBuilder.Core.Tests;

public class RelationshipTypeTests
{
    [Test]
    public void Parent_HasValue_Zero()
    {
        // Arrange & Act
        var value = (int)RelationshipType.Parent;

        // Assert
        Assert.That(value, Is.EqualTo(0));
    }

    [Test]
    public void Child_HasValue_One()
    {
        // Arrange & Act
        var value = (int)RelationshipType.Child;

        // Assert
        Assert.That(value, Is.EqualTo(1));
    }

    [Test]
    public void Spouse_HasValue_Two()
    {
        // Arrange & Act
        var value = (int)RelationshipType.Spouse;

        // Assert
        Assert.That(value, Is.EqualTo(2));
    }

    [Test]
    public void Sibling_HasValue_Three()
    {
        // Arrange & Act
        var value = (int)RelationshipType.Sibling;

        // Assert
        Assert.That(value, Is.EqualTo(3));
    }

    [Test]
    public void Other_HasValue_Four()
    {
        // Arrange & Act
        var value = (int)RelationshipType.Other;

        // Assert
        Assert.That(value, Is.EqualTo(4));
    }

    [Test]
    public void AllEnumValues_CanBeAssigned()
    {
        // Arrange & Act
        var parent = RelationshipType.Parent;
        var child = RelationshipType.Child;
        var spouse = RelationshipType.Spouse;
        var sibling = RelationshipType.Sibling;
        var other = RelationshipType.Other;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(parent, Is.EqualTo(RelationshipType.Parent));
            Assert.That(child, Is.EqualTo(RelationshipType.Child));
            Assert.That(spouse, Is.EqualTo(RelationshipType.Spouse));
            Assert.That(sibling, Is.EqualTo(RelationshipType.Sibling));
            Assert.That(other, Is.EqualTo(RelationshipType.Other));
        });
    }

    [Test]
    public void EnumValues_AreDistinct()
    {
        // Arrange
        var values = Enum.GetValues<RelationshipType>();

        // Act
        var distinctValues = values.Distinct().ToList();

        // Assert
        Assert.That(distinctValues.Count, Is.EqualTo(values.Length));
    }
}
