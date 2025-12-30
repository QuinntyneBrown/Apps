// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace FamilyTreeBuilder.Core.Tests;

public class PersonTests
{
    [Test]
    public void Constructor_CreatesPerson_WithDefaultValues()
    {
        // Arrange & Act
        var person = new Person();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(person.PersonId, Is.EqualTo(Guid.Empty));
            Assert.That(person.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(person.FirstName, Is.EqualTo(string.Empty));
            Assert.That(person.LastName, Is.Null);
            Assert.That(person.Gender, Is.Null);
            Assert.That(person.DateOfBirth, Is.Null);
            Assert.That(person.DateOfDeath, Is.Null);
            Assert.That(person.BirthPlace, Is.Null);
            Assert.That(person.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
            Assert.That(person.Relationships, Is.Not.Null);
            Assert.That(person.Relationships, Is.Empty);
            Assert.That(person.Stories, Is.Not.Null);
            Assert.That(person.Stories, Is.Empty);
            Assert.That(person.FamilyPhotos, Is.Not.Null);
            Assert.That(person.FamilyPhotos, Is.Empty);
        });
    }

    [Test]
    public void PersonId_CanBeSet_AndRetrieved()
    {
        // Arrange
        var person = new Person();
        var expectedId = Guid.NewGuid();

        // Act
        person.PersonId = expectedId;

        // Assert
        Assert.That(person.PersonId, Is.EqualTo(expectedId));
    }

    [Test]
    public void FirstName_CanBeSet_AndRetrieved()
    {
        // Arrange
        var person = new Person();
        var expectedName = "John";

        // Act
        person.FirstName = expectedName;

        // Assert
        Assert.That(person.FirstName, Is.EqualTo(expectedName));
    }

    [Test]
    public void LastName_CanBeSet_AndRetrieved()
    {
        // Arrange
        var person = new Person();
        var expectedLastName = "Smith";

        // Act
        person.LastName = expectedLastName;

        // Assert
        Assert.That(person.LastName, Is.EqualTo(expectedLastName));
    }

    [Test]
    public void Gender_CanBeSet_ToMale()
    {
        // Arrange
        var person = new Person();

        // Act
        person.Gender = Gender.Male;

        // Assert
        Assert.That(person.Gender, Is.EqualTo(Gender.Male));
    }

    [Test]
    public void Gender_CanBeSet_ToFemale()
    {
        // Arrange
        var person = new Person();

        // Act
        person.Gender = Gender.Female;

        // Assert
        Assert.That(person.Gender, Is.EqualTo(Gender.Female));
    }

    [Test]
    public void Gender_CanBeNull()
    {
        // Arrange
        var person = new Person();

        // Act
        person.Gender = null;

        // Assert
        Assert.That(person.Gender, Is.Null);
    }

    [Test]
    public void DateOfBirth_CanBeSet_AndRetrieved()
    {
        // Arrange
        var person = new Person();
        var expectedDate = new DateTime(1990, 5, 15);

        // Act
        person.DateOfBirth = expectedDate;

        // Assert
        Assert.That(person.DateOfBirth, Is.EqualTo(expectedDate));
    }

    [Test]
    public void DateOfDeath_CanBeSet_AndRetrieved()
    {
        // Arrange
        var person = new Person();
        var expectedDate = new DateTime(2020, 10, 30);

        // Act
        person.DateOfDeath = expectedDate;

        // Assert
        Assert.That(person.DateOfDeath, Is.EqualTo(expectedDate));
    }

    [Test]
    public void BirthPlace_CanBeSet_AndRetrieved()
    {
        // Arrange
        var person = new Person();
        var expectedPlace = "New York, USA";

        // Act
        person.BirthPlace = expectedPlace;

        // Assert
        Assert.That(person.BirthPlace, Is.EqualTo(expectedPlace));
    }

    [Test]
    public void Relationships_CanBeAdded()
    {
        // Arrange
        var person = new Person();
        var relationship = new Relationship { RelationshipId = Guid.NewGuid() };

        // Act
        person.Relationships.Add(relationship);

        // Assert
        Assert.That(person.Relationships, Has.Count.EqualTo(1));
    }

    [Test]
    public void Stories_CanBeAdded()
    {
        // Arrange
        var person = new Person();
        var story = new Story { StoryId = Guid.NewGuid(), Title = "My Story" };

        // Act
        person.Stories.Add(story);

        // Assert
        Assert.That(person.Stories, Has.Count.EqualTo(1));
    }

    [Test]
    public void FamilyPhotos_CanBeAdded()
    {
        // Arrange
        var person = new Person();
        var photo = new FamilyPhoto { FamilyPhotoId = Guid.NewGuid() };

        // Act
        person.FamilyPhotos.Add(photo);

        // Assert
        Assert.That(person.FamilyPhotos, Has.Count.EqualTo(1));
    }

    [Test]
    public void Person_WithAllProperties_CanBeCreatedAndRetrieved()
    {
        // Arrange
        var personId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var firstName = "Jane";
        var lastName = "Doe";
        var gender = Gender.Female;
        var dateOfBirth = new DateTime(1985, 3, 20);
        var birthPlace = "London, UK";
        var createdAt = DateTime.UtcNow;

        // Act
        var person = new Person
        {
            PersonId = personId,
            UserId = userId,
            FirstName = firstName,
            LastName = lastName,
            Gender = gender,
            DateOfBirth = dateOfBirth,
            BirthPlace = birthPlace,
            CreatedAt = createdAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(person.PersonId, Is.EqualTo(personId));
            Assert.That(person.UserId, Is.EqualTo(userId));
            Assert.That(person.FirstName, Is.EqualTo(firstName));
            Assert.That(person.LastName, Is.EqualTo(lastName));
            Assert.That(person.Gender, Is.EqualTo(gender));
            Assert.That(person.DateOfBirth, Is.EqualTo(dateOfBirth));
            Assert.That(person.BirthPlace, Is.EqualTo(birthPlace));
            Assert.That(person.CreatedAt, Is.EqualTo(createdAt));
        });
    }
}
