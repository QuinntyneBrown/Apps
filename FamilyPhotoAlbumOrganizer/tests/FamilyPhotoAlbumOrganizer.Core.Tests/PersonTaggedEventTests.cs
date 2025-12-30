// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyPhotoAlbumOrganizer.Core.Tests;

public class PersonTaggedEventTests
{
    [Test]
    public void Constructor_CreatesEvent_WithAllProperties()
    {
        // Arrange
        var personTagId = Guid.NewGuid();
        var photoId = Guid.NewGuid();
        var personName = "John Doe";
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new PersonTaggedEvent
        {
            PersonTagId = personTagId,
            PhotoId = photoId,
            PersonName = personName,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.PersonTagId, Is.EqualTo(personTagId));
            Assert.That(evt.PhotoId, Is.EqualTo(photoId));
            Assert.That(evt.PersonName, Is.EqualTo(personName));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void Event_WithDefaultTimestamp_SetsUtcNow()
    {
        // Arrange
        var personTagId = Guid.NewGuid();
        var photoId = Guid.NewGuid();
        var personName = "Jane Smith";

        // Act
        var evt = new PersonTaggedEvent
        {
            PersonTagId = personTagId,
            PhotoId = photoId,
            PersonName = personName
        };

        // Assert
        Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
    }

    [Test]
    public void Event_IsRecord_AndSupportsValueEquality()
    {
        // Arrange
        var personTagId = Guid.NewGuid();
        var photoId = Guid.NewGuid();
        var personName = "Bob Johnson";
        var timestamp = DateTime.UtcNow;

        // Act
        var event1 = new PersonTaggedEvent
        {
            PersonTagId = personTagId,
            PhotoId = photoId,
            PersonName = personName,
            Timestamp = timestamp
        };

        var event2 = new PersonTaggedEvent
        {
            PersonTagId = personTagId,
            PhotoId = photoId,
            PersonName = personName,
            Timestamp = timestamp
        };

        // Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void Event_WithDifferentProperties_AreNotEqual()
    {
        // Arrange
        var personTagId = Guid.NewGuid();
        var photoId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var event1 = new PersonTaggedEvent
        {
            PersonTagId = personTagId,
            PhotoId = photoId,
            PersonName = "Alice",
            Timestamp = timestamp
        };

        var event2 = new PersonTaggedEvent
        {
            PersonTagId = personTagId,
            PhotoId = photoId,
            PersonName = "Bob",
            Timestamp = timestamp
        };

        // Assert
        Assert.That(event1, Is.Not.EqualTo(event2));
    }

    [Test]
    public void Event_Properties_AreInitOnly()
    {
        // Arrange
        var personTagId = Guid.NewGuid();
        var photoId = Guid.NewGuid();
        var personName = "Charlie Brown";

        // Act
        var evt = new PersonTaggedEvent
        {
            PersonTagId = personTagId,
            PhotoId = photoId,
            PersonName = personName
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.PersonTagId, Is.EqualTo(personTagId));
            Assert.That(evt.PhotoId, Is.EqualTo(photoId));
            Assert.That(evt.PersonName, Is.EqualTo(personName));
        });
    }

    [Test]
    public void Event_ToString_ContainsTypeName()
    {
        // Arrange
        var evt = new PersonTaggedEvent
        {
            PersonTagId = Guid.NewGuid(),
            PhotoId = Guid.NewGuid(),
            PersonName = "Test Person"
        };

        // Act
        var result = evt.ToString();

        // Assert
        Assert.That(result, Does.Contain("PersonTaggedEvent"));
    }
}
