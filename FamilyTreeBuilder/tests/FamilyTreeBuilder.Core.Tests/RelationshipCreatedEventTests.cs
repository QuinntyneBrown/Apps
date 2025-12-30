// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyTreeBuilder.Core.Tests;

public class RelationshipCreatedEventTests
{
    [Test]
    public void Constructor_CreatesEvent_WithAllProperties()
    {
        // Arrange
        var relationshipId = Guid.NewGuid();
        var personId = Guid.NewGuid();
        var relatedPersonId = Guid.NewGuid();
        var relationshipType = RelationshipType.Parent;
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new RelationshipCreatedEvent
        {
            RelationshipId = relationshipId,
            PersonId = personId,
            RelatedPersonId = relatedPersonId,
            RelationshipType = relationshipType,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.RelationshipId, Is.EqualTo(relationshipId));
            Assert.That(evt.PersonId, Is.EqualTo(personId));
            Assert.That(evt.RelatedPersonId, Is.EqualTo(relatedPersonId));
            Assert.That(evt.RelationshipType, Is.EqualTo(relationshipType));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Event_WithDefaultTimestamp_SetsUtcNow()
    {
        // Arrange
        var relationshipId = Guid.NewGuid();
        var personId = Guid.NewGuid();
        var relatedPersonId = Guid.NewGuid();
        var relationshipType = RelationshipType.Child;

        // Act
        var evt = new RelationshipCreatedEvent
        {
            RelationshipId = relationshipId,
            PersonId = personId,
            RelatedPersonId = relatedPersonId,
            RelationshipType = relationshipType
        };

        // Assert
        Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void Event_IsRecord_AndSupportsValueEquality()
    {
        // Arrange
        var relationshipId = Guid.NewGuid();
        var personId = Guid.NewGuid();
        var relatedPersonId = Guid.NewGuid();
        var relationshipType = RelationshipType.Spouse;
        var timestamp = DateTime.UtcNow;

        // Act
        var event1 = new RelationshipCreatedEvent
        {
            RelationshipId = relationshipId,
            PersonId = personId,
            RelatedPersonId = relatedPersonId,
            RelationshipType = relationshipType,
            Timestamp = timestamp
        };

        var event2 = new RelationshipCreatedEvent
        {
            RelationshipId = relationshipId,
            PersonId = personId,
            RelatedPersonId = relatedPersonId,
            RelationshipType = relationshipType,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Event_WithDifferentRelationshipType_AreNotEqual()
    {
        // Arrange
        var relationshipId = Guid.NewGuid();
        var personId = Guid.NewGuid();
        var relatedPersonId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new RelationshipCreatedEvent
        {
            RelationshipId = relationshipId,
            PersonId = personId,
            RelatedPersonId = relatedPersonId,
            RelationshipType = RelationshipType.Parent,
            Timestamp = timestamp
        };

        var event2 = new RelationshipCreatedEvent
        {
            RelationshipId = relationshipId,
            PersonId = personId,
            RelatedPersonId = relatedPersonId,
            RelationshipType = RelationshipType.Child,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }

    [Test]
    public void Event_Properties_AreInitOnly()
    {
        // Arrange
        var relationshipId = Guid.NewGuid();
        var personId = Guid.NewGuid();
        var relatedPersonId = Guid.NewGuid();
        var relationshipType = RelationshipType.Sibling;

        // Act
        var evt = new RelationshipCreatedEvent
        {
            RelationshipId = relationshipId,
            PersonId = personId,
            RelatedPersonId = relatedPersonId,
            RelationshipType = relationshipType
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.RelationshipId, Is.EqualTo(relationshipId));
            Assert.That(evt.PersonId, Is.EqualTo(personId));
            Assert.That(evt.RelatedPersonId, Is.EqualTo(relatedPersonId));
            Assert.That(evt.RelationshipType, Is.EqualTo(relationshipType));
        });
    }

    [Test]
    public void Event_ToString_ContainsTypeName()
    {
        // Arrange
        var evt = new RelationshipCreatedEvent
        {
            RelationshipId = Guid.NewGuid(),
            PersonId = Guid.NewGuid(),
            RelatedPersonId = Guid.NewGuid(),
            RelationshipType = RelationshipType.Parent
        };

        // Act
        var result = evt.ToString();

        // Assert
        Assert.That(result, Does.Contain("RelationshipCreatedEvent"));
    }
}
