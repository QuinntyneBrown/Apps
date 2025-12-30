namespace DigitalLegacyPlanner.Core.Tests;

public class TrustedContactTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesTrustedContact()
    {
        // Arrange
        var contactId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var fullName = "John Doe";
        var relationship = "Spouse";
        var email = "john.doe@example.com";
        var phoneNumber = "555-1234";
        var role = "Executor";
        var isPrimaryContact = true;
        var isNotified = false;
        var notes = "Primary contact for estate";

        // Act
        var contact = new TrustedContact
        {
            TrustedContactId = contactId,
            UserId = userId,
            FullName = fullName,
            Relationship = relationship,
            Email = email,
            PhoneNumber = phoneNumber,
            Role = role,
            IsPrimaryContact = isPrimaryContact,
            IsNotified = isNotified,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(contact.TrustedContactId, Is.EqualTo(contactId));
            Assert.That(contact.UserId, Is.EqualTo(userId));
            Assert.That(contact.FullName, Is.EqualTo(fullName));
            Assert.That(contact.Relationship, Is.EqualTo(relationship));
            Assert.That(contact.Email, Is.EqualTo(email));
            Assert.That(contact.PhoneNumber, Is.EqualTo(phoneNumber));
            Assert.That(contact.Role, Is.EqualTo(role));
            Assert.That(contact.IsPrimaryContact, Is.EqualTo(isPrimaryContact));
            Assert.That(contact.IsNotified, Is.EqualTo(isNotified));
            Assert.That(contact.Notes, Is.EqualTo(notes));
        });
    }

    [Test]
    public void DefaultValues_AreSetCorrectly()
    {
        // Arrange & Act
        var contact = new TrustedContact();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(contact.FullName, Is.EqualTo(string.Empty));
            Assert.That(contact.Relationship, Is.EqualTo(string.Empty));
            Assert.That(contact.Email, Is.EqualTo(string.Empty));
            Assert.That(contact.PhoneNumber, Is.Null);
            Assert.That(contact.Role, Is.Null);
            Assert.That(contact.IsPrimaryContact, Is.False);
            Assert.That(contact.IsNotified, Is.False);
            Assert.That(contact.Notes, Is.Null);
            Assert.That(contact.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void MarkAsNotified_UpdatesProperty()
    {
        // Arrange
        var contact = new TrustedContact
        {
            TrustedContactId = Guid.NewGuid(),
            IsNotified = false
        };

        // Act
        contact.MarkAsNotified();

        // Assert
        Assert.That(contact.IsNotified, Is.True);
    }

    [Test]
    public void MarkAsNotified_WhenAlreadyNotified_RemainsTrue()
    {
        // Arrange
        var contact = new TrustedContact
        {
            TrustedContactId = Guid.NewGuid(),
            IsNotified = true
        };

        // Act
        contact.MarkAsNotified();

        // Assert
        Assert.That(contact.IsNotified, Is.True);
    }

    [Test]
    public void SetAsPrimary_UpdatesProperty()
    {
        // Arrange
        var contact = new TrustedContact
        {
            TrustedContactId = Guid.NewGuid(),
            IsPrimaryContact = false
        };

        // Act
        contact.SetAsPrimary();

        // Assert
        Assert.That(contact.IsPrimaryContact, Is.True);
    }

    [Test]
    public void SetAsPrimary_WhenAlreadyPrimary_RemainsTrue()
    {
        // Arrange
        var contact = new TrustedContact
        {
            TrustedContactId = Guid.NewGuid(),
            IsPrimaryContact = true
        };

        // Act
        contact.SetAsPrimary();

        // Assert
        Assert.That(contact.IsPrimaryContact, Is.True);
    }

    [Test]
    public void IsPrimaryContact_CanBeSetToFalse()
    {
        // Arrange & Act
        var contact = new TrustedContact
        {
            IsPrimaryContact = false
        };

        // Assert
        Assert.That(contact.IsPrimaryContact, Is.False);
    }

    [Test]
    public void IsNotified_CanBeSetToFalse()
    {
        // Arrange & Act
        var contact = new TrustedContact
        {
            IsNotified = false
        };

        // Assert
        Assert.That(contact.IsNotified, Is.False);
    }

    [Test]
    public void PhoneNumber_CanBeSet()
    {
        // Arrange
        var phoneNumber = "+1-555-123-4567";
        var contact = new TrustedContact();

        // Act
        contact.PhoneNumber = phoneNumber;

        // Assert
        Assert.That(contact.PhoneNumber, Is.EqualTo(phoneNumber));
    }

    [Test]
    public void Role_CanBeSet()
    {
        // Arrange
        var role = "Digital Executor";
        var contact = new TrustedContact();

        // Act
        contact.Role = role;

        // Assert
        Assert.That(contact.Role, Is.EqualTo(role));
    }

    [Test]
    public void Notes_CanBeSet()
    {
        // Arrange
        var notes = "Has access to safe deposit box";
        var contact = new TrustedContact();

        // Act
        contact.Notes = notes;

        // Assert
        Assert.That(contact.Notes, Is.EqualTo(notes));
    }

    [Test]
    public void Email_CanBeSet()
    {
        // Arrange
        var email = "contact@example.com";
        var contact = new TrustedContact();

        // Act
        contact.Email = email;

        // Assert
        Assert.That(contact.Email, Is.EqualTo(email));
    }

    [Test]
    public void Relationship_CanBeSet()
    {
        // Arrange
        var relationship = "Sibling";
        var contact = new TrustedContact();

        // Act
        contact.Relationship = relationship;

        // Assert
        Assert.That(contact.Relationship, Is.EqualTo(relationship));
    }

    [Test]
    public void CreatedAt_IsSetOnCreation()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var contact = new TrustedContact();

        // Assert
        Assert.That(contact.CreatedAt, Is.GreaterThan(beforeCreation));
    }
}
