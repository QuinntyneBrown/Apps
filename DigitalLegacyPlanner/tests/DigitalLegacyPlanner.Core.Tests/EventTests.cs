namespace DigitalLegacyPlanner.Core.Tests;

public class EventTests
{
    [Test]
    public void AccountAddedEvent_CanBeCreated()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var accountType = AccountType.Email;
        var accountName = "Gmail Account";

        // Act
        var evt = new AccountAddedEvent
        {
            DigitalAccountId = accountId,
            UserId = userId,
            AccountType = accountType,
            AccountName = accountName
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.DigitalAccountId, Is.EqualTo(accountId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.AccountType, Is.EqualTo(accountType));
            Assert.That(evt.AccountName, Is.EqualTo(accountName));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void ContactNotifiedEvent_CanBeCreated()
    {
        // Arrange
        var contactId = Guid.NewGuid();
        var fullName = "Jane Smith";
        var email = "jane.smith@example.com";

        // Act
        var evt = new ContactNotifiedEvent
        {
            TrustedContactId = contactId,
            FullName = fullName,
            Email = email
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.TrustedContactId, Is.EqualTo(contactId));
            Assert.That(evt.FullName, Is.EqualTo(fullName));
            Assert.That(evt.Email, Is.EqualTo(email));
            Assert.That(evt.Timestamp, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void AccountAddedEvent_AllAccountTypes_CanBeUsed()
    {
        // Arrange & Act
        var emailEvent = new AccountAddedEvent { AccountType = AccountType.Email };
        var socialEvent = new AccountAddedEvent { AccountType = AccountType.SocialMedia };
        var financialEvent = new AccountAddedEvent { AccountType = AccountType.Financial };
        var cloudEvent = new AccountAddedEvent { AccountType = AccountType.CloudStorage };
        var subscriptionEvent = new AccountAddedEvent { AccountType = AccountType.Subscription };
        var shoppingEvent = new AccountAddedEvent { AccountType = AccountType.Shopping };
        var gamingEvent = new AccountAddedEvent { AccountType = AccountType.Gaming };
        var professionalEvent = new AccountAddedEvent { AccountType = AccountType.Professional };
        var cryptoEvent = new AccountAddedEvent { AccountType = AccountType.Cryptocurrency };
        var otherEvent = new AccountAddedEvent { AccountType = AccountType.Other };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(emailEvent.AccountType, Is.EqualTo(AccountType.Email));
            Assert.That(socialEvent.AccountType, Is.EqualTo(AccountType.SocialMedia));
            Assert.That(financialEvent.AccountType, Is.EqualTo(AccountType.Financial));
            Assert.That(cloudEvent.AccountType, Is.EqualTo(AccountType.CloudStorage));
            Assert.That(subscriptionEvent.AccountType, Is.EqualTo(AccountType.Subscription));
            Assert.That(shoppingEvent.AccountType, Is.EqualTo(AccountType.Shopping));
            Assert.That(gamingEvent.AccountType, Is.EqualTo(AccountType.Gaming));
            Assert.That(professionalEvent.AccountType, Is.EqualTo(AccountType.Professional));
            Assert.That(cryptoEvent.AccountType, Is.EqualTo(AccountType.Cryptocurrency));
            Assert.That(otherEvent.AccountType, Is.EqualTo(AccountType.Other));
        });
    }

    [Test]
    public void Events_AreRecords()
    {
        // This ensures events are immutable and have value-based equality
        var accountId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var event1 = new AccountAddedEvent
        {
            DigitalAccountId = accountId,
            UserId = userId,
            AccountType = AccountType.Email,
            AccountName = "Test Account",
            Timestamp = new DateTime(2024, 1, 1)
        };

        var event2 = new AccountAddedEvent
        {
            DigitalAccountId = accountId,
            UserId = userId,
            AccountType = AccountType.Email,
            AccountName = "Test Account",
            Timestamp = new DateTime(2024, 1, 1)
        };

        // Assert
        Assert.That(event1, Is.EqualTo(event2));
    }

    [Test]
    public void ContactNotifiedEvent_WithEmptyStrings_CanBeCreated()
    {
        // Arrange & Act
        var evt = new ContactNotifiedEvent
        {
            TrustedContactId = Guid.NewGuid(),
            FullName = string.Empty,
            Email = string.Empty
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.FullName, Is.EqualTo(string.Empty));
            Assert.That(evt.Email, Is.EqualTo(string.Empty));
        });
    }

    [Test]
    public void AccountAddedEvent_WithEmptyAccountName_CanBeCreated()
    {
        // Arrange & Act
        var evt = new AccountAddedEvent
        {
            DigitalAccountId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            AccountType = AccountType.Other,
            AccountName = string.Empty
        };

        // Assert
        Assert.That(evt.AccountName, Is.EqualTo(string.Empty));
    }
}
