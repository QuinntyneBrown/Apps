// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyTreeBuilder.Core.Tests;

public class RelationshipTests
{
    [Test]
    public void Constructor_CreatesRelationship_WithDefaultValues()
    {
        // Arrange & Act
        var relationship = new Relationship();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(relationship.RelationshipId, Is.EqualTo(Guid.Empty));
            Assert.That(relationship.PersonId, Is.EqualTo(Guid.Empty));
            Assert.That(relationship.Person, Is.Null);
            Assert.That(relationship.RelatedPersonId, Is.EqualTo(Guid.Empty));
            Assert.That(relationship.RelationshipType, Is.EqualTo(RelationshipType.Parent));
            Assert.That(relationship.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void RelationshipId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var relationship = new Relationship();
        var expectedId = Guid.NewGuid();

        // Act
        relationship.RelationshipId = expectedId;

        // Assert
        Assert.That(relationship.RelationshipId, Is.EqualTo(expectedId));
    }

    [Test]
    public void PersonId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var relationship = new Relationship();
        var expectedPersonId = Guid.NewGuid();

        // Act
        relationship.PersonId = expectedPersonId;

        // Assert
        Assert.That(relationship.PersonId, Is.EqualTo(expectedPersonId));
    }

    [Test]
    public void RelatedPersonId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var relationship = new Relationship();
        var expectedRelatedPersonId = Guid.NewGuid();

        // Act
        relationship.RelatedPersonId = expectedRelatedPersonId;

        // Assert
        Assert.That(relationship.RelatedPersonId, Is.EqualTo(expectedRelatedPersonId));
    }

    [Test]
    public void RelationshipType_CanBeSet_ToParent()
    {
        // Arrange
        var relationship = new Relationship();

        // Act
        relationship.RelationshipType = RelationshipType.Parent;

        // Assert
        Assert.That(relationship.RelationshipType, Is.EqualTo(RelationshipType.Parent));
    }

    [Test]
    public void RelationshipType_CanBeSet_ToChild()
    {
        // Arrange
        var relationship = new Relationship();

        // Act
        relationship.RelationshipType = RelationshipType.Child;

        // Assert
        Assert.That(relationship.RelationshipType, Is.EqualTo(RelationshipType.Child));
    }

    [Test]
    public void RelationshipType_CanBeSet_ToSpouse()
    {
        // Arrange
        var relationship = new Relationship();

        // Act
        relationship.RelationshipType = RelationshipType.Spouse;

        // Assert
        Assert.That(relationship.RelationshipType, Is.EqualTo(RelationshipType.Spouse));
    }

    [Test]
    public void RelationshipType_CanBeSet_ToSibling()
    {
        // Arrange
        var relationship = new Relationship();

        // Act
        relationship.RelationshipType = RelationshipType.Sibling;

        // Assert
        Assert.That(relationship.RelationshipType, Is.EqualTo(RelationshipType.Sibling));
    }

    [Test]
    public void Relationship_WithAllProperties_CanBeCreatedAndRetrieved()
    {
        // Arrange
        var relationshipId = Guid.NewGuid();
        var personId = Guid.NewGuid();
        var relatedPersonId = Guid.NewGuid();
        var relationshipType = RelationshipType.Spouse;
        var createdAt = DateTime.UtcNow;

        // Act
        var relationship = new Relationship
        {
            RelationshipId = relationshipId,
            PersonId = personId,
            RelatedPersonId = relatedPersonId,
            RelationshipType = relationshipType,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(relationship.RelationshipId, Is.EqualTo(relationshipId));
            Assert.That(relationship.PersonId, Is.EqualTo(personId));
            Assert.That(relationship.RelatedPersonId, Is.EqualTo(relatedPersonId));
            Assert.That(relationship.RelationshipType, Is.EqualTo(relationshipType));
            Assert.That(relationship.CreatedAt, Is.EqualTo(createdAt));
        });
    }
}
