namespace RoadsideAssistanceInfoHub.Core.Tests;

public class EmergencyContactTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesEmergencyContact()
    {
        // Arrange & Act
        var contact = new EmergencyContact
        {
            EmergencyContactId = Guid.NewGuid(),
            Name = "John Doe",
            Relationship = "Spouse",
            PhoneNumber = "555-1234",
            AlternatePhone = "555-5678",
            Email = "john@example.com",
            Address = "123 Main St",
            IsPrimaryContact = true,
            ContactType = "Personal",
            ServiceArea = "Metro Area",
            Notes = "Available 24/7",
            IsActive = true
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(contact.EmergencyContactId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(contact.Name, Is.EqualTo("John Doe"));
            Assert.That(contact.Relationship, Is.EqualTo("Spouse"));
            Assert.That(contact.PhoneNumber, Is.EqualTo("555-1234"));
            Assert.That(contact.AlternatePhone, Is.EqualTo("555-5678"));
            Assert.That(contact.Email, Is.EqualTo("john@example.com"));
            Assert.That(contact.IsPrimaryContact, Is.True);
            Assert.That(contact.IsActive, Is.True);
        });
    }

    [Test]
    public void SetAsPrimary_Contact_SetsIsPrimaryContactToTrue()
    {
        // Arrange
        var contact = new EmergencyContact
        {
            IsPrimaryContact = false
        };

        // Act
        contact.SetAsPrimary();

        // Assert
        Assert.That(contact.IsPrimaryContact, Is.True);
    }

    [Test]
    public void SetAsPrimary_AlreadyPrimary_RemainsTrue()
    {
        // Arrange
        var contact = new EmergencyContact
        {
            IsPrimaryContact = true
        };

        // Act
        contact.SetAsPrimary();

        // Assert
        Assert.That(contact.IsPrimaryContact, Is.True);
    }

    [Test]
    public void RemovePrimary_PrimaryContact_SetsIsPrimaryContactToFalse()
    {
        // Arrange
        var contact = new EmergencyContact
        {
            IsPrimaryContact = true
        };

        // Act
        contact.RemovePrimary();

        // Assert
        Assert.That(contact.IsPrimaryContact, Is.False);
    }

    [Test]
    public void RemovePrimary_NotPrimary_RemainsFalse()
    {
        // Arrange
        var contact = new EmergencyContact
        {
            IsPrimaryContact = false
        };

        // Act
        contact.RemovePrimary();

        // Assert
        Assert.That(contact.IsPrimaryContact, Is.False);
    }

    [Test]
    public void Deactivate_ActiveContact_SetsIsActiveToFalse()
    {
        // Arrange
        var contact = new EmergencyContact
        {
            IsActive = true
        };

        // Act
        contact.Deactivate();

        // Assert
        Assert.That(contact.IsActive, Is.False);
    }

    [Test]
    public void Deactivate_AlreadyInactive_RemainsFalse()
    {
        // Arrange
        var contact = new EmergencyContact
        {
            IsActive = false
        };

        // Act
        contact.Deactivate();

        // Assert
        Assert.That(contact.IsActive, Is.False);
    }

    [Test]
    public void Reactivate_InactiveContact_SetsIsActiveToTrue()
    {
        // Arrange
        var contact = new EmergencyContact
        {
            IsActive = false
        };

        // Act
        contact.Reactivate();

        // Assert
        Assert.That(contact.IsActive, Is.True);
    }

    [Test]
    public void Reactivate_AlreadyActive_RemainsTrue()
    {
        // Arrange
        var contact = new EmergencyContact
        {
            IsActive = true
        };

        // Act
        contact.Reactivate();

        // Assert
        Assert.That(contact.IsActive, Is.True);
    }

    [Test]
    public void UpdatePhoneNumber_NewNumber_UpdatesPhoneNumber()
    {
        // Arrange
        var contact = new EmergencyContact
        {
            PhoneNumber = "555-1234"
        };

        // Act
        contact.UpdatePhoneNumber("555-9999");

        // Assert
        Assert.That(contact.PhoneNumber, Is.EqualTo("555-9999"));
    }

    [Test]
    public void UpdatePhoneNumber_EmptyString_SetsToEmptyString()
    {
        // Arrange
        var contact = new EmergencyContact
        {
            PhoneNumber = "555-1234"
        };

        // Act
        contact.UpdatePhoneNumber(string.Empty);

        // Assert
        Assert.That(contact.PhoneNumber, Is.EqualTo(string.Empty));
    }

    [Test]
    public void Name_DefaultValue_IsEmptyString()
    {
        // Arrange & Act
        var contact = new EmergencyContact();

        // Assert
        Assert.That(contact.Name, Is.EqualTo(string.Empty));
    }

    [Test]
    public void PhoneNumber_DefaultValue_IsEmptyString()
    {
        // Arrange & Act
        var contact = new EmergencyContact();

        // Assert
        Assert.That(contact.PhoneNumber, Is.EqualTo(string.Empty));
    }

    [Test]
    public void IsPrimaryContact_DefaultValue_IsFalse()
    {
        // Arrange & Act
        var contact = new EmergencyContact();

        // Assert
        Assert.That(contact.IsPrimaryContact, Is.False);
    }

    [Test]
    public void IsActive_DefaultValue_IsTrue()
    {
        // Arrange & Act
        var contact = new EmergencyContact();

        // Assert
        Assert.That(contact.IsActive, Is.True);
    }

    [Test]
    public void Relationship_CanBeNull()
    {
        // Arrange & Act
        var contact = new EmergencyContact
        {
            Relationship = null
        };

        // Assert
        Assert.That(contact.Relationship, Is.Null);
    }

    [Test]
    public void Email_CanBeNull()
    {
        // Arrange & Act
        var contact = new EmergencyContact
        {
            Email = null
        };

        // Assert
        Assert.That(contact.Email, Is.Null);
    }

    [Test]
    public void ServiceArea_CanBeNull()
    {
        // Arrange & Act
        var contact = new EmergencyContact
        {
            ServiceArea = null
        };

        // Assert
        Assert.That(contact.ServiceArea, Is.Null);
    }
}
