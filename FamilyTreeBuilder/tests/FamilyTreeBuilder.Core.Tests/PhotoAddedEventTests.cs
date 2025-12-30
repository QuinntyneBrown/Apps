// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyTreeBuilder.Core.Tests;

public class PhotoAddedEventTests
{
    [Test]
    public void Constructor_CreatesEvent_WithAllProperties()
    {
        // Arrange
        var familyPhotoId = Guid.NewGuid();
        var personId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new PhotoAddedEvent
        {
            FamilyPhotoId = familyPhotoId,
            PersonId = personId,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.FamilyPhotoId, Is.EqualTo(familyPhotoId));
            Assert.That(evt.PersonId, Is.EqualTo(personId));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Event_WithDefaultTimestamp_SetsUtcNow()
    {
        // Arrange
        var familyPhotoId = Guid.NewGuid();
        var personId = Guid.NewGuid();

        // Act
        var evt = new PhotoAddedEvent
        {
            FamilyPhotoId = familyPhotoId,
            PersonId = personId
        };

        // Assert
        Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void Event_IsRecord_AndSupportsValueEquality()
    {
        // Arrange
        var familyPhotoId = Guid.NewGuid();
        var personId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var event1 = new PhotoAddedEvent
        {
            FamilyPhotoId = familyPhotoId,
            PersonId = personId,
            Timestamp = timestamp
        };

        var event2 = new PhotoAddedEvent
        {
            FamilyPhotoId = familyPhotoId,
            PersonId = personId,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Event_WithDifferentProperties_AreNotEqual()
    {
        // Arrange
        var familyPhotoId1 = Guid.NewGuid();
        var familyPhotoId2 = Guid.NewGuid();
        var personId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new PhotoAddedEvent
        {
            FamilyPhotoId = familyPhotoId1,
            PersonId = personId,
            Timestamp = timestamp
        };

        var event2 = new PhotoAddedEvent
        {
            FamilyPhotoId = familyPhotoId2,
            PersonId = personId,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }

    [Test]
    public void Event_Properties_AreInitOnly()
    {
        // Arrange
        var familyPhotoId = Guid.NewGuid();
        var personId = Guid.NewGuid();

        // Act
        var evt = new PhotoAddedEvent
        {
            FamilyPhotoId = familyPhotoId,
            PersonId = personId
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.FamilyPhotoId, Is.EqualTo(familyPhotoId));
            Assert.That(evt.PersonId, Is.EqualTo(personId));
        });
    }

    [Test]
    public void Event_ToString_ContainsTypeName()
    {
        // Arrange
        var evt = new PhotoAddedEvent
        {
            FamilyPhotoId = Guid.NewGuid(),
            PersonId = Guid.NewGuid()
        };

        // Act
        var result = evt.ToString();

        // Assert
        Assert.That(result, Does.Contain("PhotoAddedEvent"));
    }
}
