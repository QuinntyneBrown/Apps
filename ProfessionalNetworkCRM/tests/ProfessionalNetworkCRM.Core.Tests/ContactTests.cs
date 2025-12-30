// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace ProfessionalNetworkCRM.Core.Tests;

public class ContactTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesContact()
    {
        // Arrange & Act
        var contact = new Contact();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(contact.ContactId, Is.EqualTo(Guid.Empty));
            Assert.That(contact.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(contact.FirstName, Is.EqualTo(string.Empty));
            Assert.That(contact.LastName, Is.EqualTo(string.Empty));
            Assert.That(contact.ContactType, Is.EqualTo(ContactType.Colleague));
            Assert.That(contact.Company, Is.Null);
            Assert.That(contact.JobTitle, Is.Null);
            Assert.That(contact.Email, Is.Null);
            Assert.That(contact.Phone, Is.Null);
            Assert.That(contact.LinkedInUrl, Is.Null);
            Assert.That(contact.Location, Is.Null);
            Assert.That(contact.Notes, Is.Null);
            Assert.That(contact.Tags, Is.Not.Null);
            Assert.That(contact.Tags, Is.Empty);
            Assert.That(contact.DateMet, Is.EqualTo(default(DateTime)));
            Assert.That(contact.LastContactedDate, Is.Null);
            Assert.That(contact.IsPriority, Is.False);
            Assert.That(contact.CreatedAt, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
            Assert.That(contact.UpdatedAt, Is.Null);
            Assert.That(contact.Interactions, Is.Not.Null);
            Assert.That(contact.Interactions, Is.Empty);
            Assert.That(contact.FollowUps, Is.Not.Null);
            Assert.That(contact.FollowUps, Is.Empty);
        });
    }

    [Test]
    public void Constructor_WithValidParameters_CreatesContact()
    {
        // Arrange
        var contactId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var firstName = "John";
        var lastName = "Doe";
        var company = "Acme Corp";
        var email = "john.doe@example.com";
        var dateMet = new DateTime(2024, 1, 15);

        // Act
        var contact = new Contact
        {
            ContactId = contactId,
            UserId = userId,
            FirstName = firstName,
            LastName = lastName,
            ContactType = ContactType.Client,
            Company = company,
            Email = email,
            DateMet = dateMet,
            IsPriority = true
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(contact.ContactId, Is.EqualTo(contactId));
            Assert.That(contact.UserId, Is.EqualTo(userId));
            Assert.That(contact.FirstName, Is.EqualTo(firstName));
            Assert.That(contact.LastName, Is.EqualTo(lastName));
            Assert.That(contact.ContactType, Is.EqualTo(ContactType.Client));
            Assert.That(contact.Company, Is.EqualTo(company));
            Assert.That(contact.Email, Is.EqualTo(email));
            Assert.That(contact.DateMet, Is.EqualTo(dateMet));
            Assert.That(contact.IsPriority, Is.True);
        });
    }

    [Test]
    public void FullName_WithFirstAndLastName_ReturnsCombinedName()
    {
        // Arrange
        var contact = new Contact
        {
            FirstName = "Jane",
            LastName = "Smith"
        };

        // Act
        var fullName = contact.FullName;

        // Assert
        Assert.That(fullName, Is.EqualTo("Jane Smith"));
    }

    [Test]
    public void FullName_WithOnlyFirstName_ReturnsFirstName()
    {
        // Arrange
        var contact = new Contact
        {
            FirstName = "Jane",
            LastName = ""
        };

        // Act
        var fullName = contact.FullName;

        // Assert
        Assert.That(fullName, Is.EqualTo("Jane"));
    }

    [Test]
    public void FullName_WithOnlyLastName_ReturnsLastName()
    {
        // Arrange
        var contact = new Contact
        {
            FirstName = "",
            LastName = "Smith"
        };

        // Act
        var fullName = contact.FullName;

        // Assert
        Assert.That(fullName, Is.EqualTo("Smith"));
    }

    [Test]
    public void UpdateLastContactedDate_ValidDate_UpdatesDateAndTimestamp()
    {
        // Arrange
        var contact = new Contact();
        var contactDate = new DateTime(2024, 6, 15, 10, 30, 0);
        var beforeUpdate = DateTime.UtcNow;

        // Act
        contact.UpdateLastContactedDate(contactDate);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(contact.LastContactedDate, Is.EqualTo(contactDate));
            Assert.That(contact.UpdatedAt, Is.Not.Null);
            Assert.That(contact.UpdatedAt.Value, Is.GreaterThanOrEqualTo(beforeUpdate));
            Assert.That(contact.UpdatedAt.Value, Is.LessThanOrEqualTo(DateTime.UtcNow));
        });
    }

    [Test]
    public void TogglePriority_FromFalseToTrue_SetsPriorityTrue()
    {
        // Arrange
        var contact = new Contact { IsPriority = false };

        // Act
        contact.TogglePriority();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(contact.IsPriority, Is.True);
            Assert.That(contact.UpdatedAt, Is.Not.Null);
        });
    }

    [Test]
    public void TogglePriority_FromTrueToFalse_SetsPriorityFalse()
    {
        // Arrange
        var contact = new Contact { IsPriority = true };

        // Act
        contact.TogglePriority();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(contact.IsPriority, Is.False);
            Assert.That(contact.UpdatedAt, Is.Not.Null);
        });
    }

    [Test]
    public void TogglePriority_MultipleTimes_TogglesCorrectly()
    {
        // Arrange
        var contact = new Contact { IsPriority = false };

        // Act & Assert
        contact.TogglePriority();
        Assert.That(contact.IsPriority, Is.True);

        contact.TogglePriority();
        Assert.That(contact.IsPriority, Is.False);

        contact.TogglePriority();
        Assert.That(contact.IsPriority, Is.True);
    }

    [Test]
    public void AddTag_NewTag_AddsTagToList()
    {
        // Arrange
        var contact = new Contact();
        var tag = "VIP";

        // Act
        contact.AddTag(tag);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(contact.Tags, Contains.Item(tag));
            Assert.That(contact.Tags.Count, Is.EqualTo(1));
            Assert.That(contact.UpdatedAt, Is.Not.Null);
        });
    }

    [Test]
    public void AddTag_DuplicateTag_DoesNotAddDuplicate()
    {
        // Arrange
        var contact = new Contact();
        var tag = "VIP";

        // Act
        contact.AddTag(tag);
        contact.AddTag(tag);

        // Assert
        Assert.That(contact.Tags.Count, Is.EqualTo(1));
    }

    [Test]
    public void AddTag_DuplicateTagDifferentCase_DoesNotAddDuplicate()
    {
        // Arrange
        var contact = new Contact();

        // Act
        contact.AddTag("VIP");
        contact.AddTag("vip");

        // Assert
        Assert.That(contact.Tags.Count, Is.EqualTo(1));
    }

    [Test]
    public void AddTag_MultipleTags_AddsAllUniqueTags()
    {
        // Arrange
        var contact = new Contact();

        // Act
        contact.AddTag("VIP");
        contact.AddTag("Client");
        contact.AddTag("High-Value");

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(contact.Tags.Count, Is.EqualTo(3));
            Assert.That(contact.Tags, Contains.Item("VIP"));
            Assert.That(contact.Tags, Contains.Item("Client"));
            Assert.That(contact.Tags, Contains.Item("High-Value"));
        });
    }

    [Test]
    public void Contact_WithAllOptionalProperties_SetsAllProperties()
    {
        // Arrange & Act
        var contact = new Contact
        {
            ContactId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            FirstName = "Alice",
            LastName = "Johnson",
            ContactType = ContactType.Mentor,
            Company = "Tech Solutions",
            JobTitle = "Senior Developer",
            Email = "alice@techsolutions.com",
            Phone = "+1-555-1234",
            LinkedInUrl = "https://linkedin.com/in/alicejohnson",
            Location = "New York, NY",
            Notes = "Met at tech conference",
            DateMet = DateTime.Today,
            IsPriority = true
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(contact.Company, Is.EqualTo("Tech Solutions"));
            Assert.That(contact.JobTitle, Is.EqualTo("Senior Developer"));
            Assert.That(contact.Email, Is.EqualTo("alice@techsolutions.com"));
            Assert.That(contact.Phone, Is.EqualTo("+1-555-1234"));
            Assert.That(contact.LinkedInUrl, Is.EqualTo("https://linkedin.com/in/alicejohnson"));
            Assert.That(contact.Location, Is.EqualTo("New York, NY"));
            Assert.That(contact.Notes, Is.EqualTo("Met at tech conference"));
        });
    }
}
