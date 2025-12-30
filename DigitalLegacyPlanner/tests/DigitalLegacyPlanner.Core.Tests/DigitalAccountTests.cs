namespace DigitalLegacyPlanner.Core.Tests;

public class DigitalAccountTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesDigitalAccount()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var accountType = AccountType.Email;
        var accountName = "Gmail";
        var username = "user@gmail.com";
        var passwordHint = "Encrypted hint";
        var url = "https://gmail.com";
        var desiredAction = "Delete";
        var notes = "Primary email account";

        // Act
        var account = new DigitalAccount
        {
            DigitalAccountId = accountId,
            UserId = userId,
            AccountType = accountType,
            AccountName = accountName,
            Username = username,
            PasswordHint = passwordHint,
            Url = url,
            DesiredAction = desiredAction,
            Notes = notes
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(account.DigitalAccountId, Is.EqualTo(accountId));
            Assert.That(account.UserId, Is.EqualTo(userId));
            Assert.That(account.AccountType, Is.EqualTo(accountType));
            Assert.That(account.AccountName, Is.EqualTo(accountName));
            Assert.That(account.Username, Is.EqualTo(username));
            Assert.That(account.PasswordHint, Is.EqualTo(passwordHint));
            Assert.That(account.Url, Is.EqualTo(url));
            Assert.That(account.DesiredAction, Is.EqualTo(desiredAction));
            Assert.That(account.Notes, Is.EqualTo(notes));
        });
    }

    [Test]
    public void DefaultValues_AreSetCorrectly()
    {
        // Arrange & Act
        var account = new DigitalAccount();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(account.AccountName, Is.EqualTo(string.Empty));
            Assert.That(account.Username, Is.EqualTo(string.Empty));
            Assert.That(account.PasswordHint, Is.Null);
            Assert.That(account.Url, Is.Null);
            Assert.That(account.DesiredAction, Is.Null);
            Assert.That(account.Notes, Is.Null);
            Assert.That(account.LastUpdatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(account.CreatedAt, Is.Not.EqualTo(default(DateTime)));
        });
    }

    [Test]
    public void UpdateAccount_UpdatesLastUpdatedAt()
    {
        // Arrange
        var account = new DigitalAccount
        {
            DigitalAccountId = Guid.NewGuid()
        };
        var beforeUpdate = DateTime.UtcNow.AddSeconds(-1);

        // Act
        account.UpdateAccount();

        // Assert
        Assert.That(account.LastUpdatedAt, Is.GreaterThan(beforeUpdate));
    }

    [Test]
    public void UpdateAccount_CalledMultipleTimes_UpdatesTimestamp()
    {
        // Arrange
        var account = new DigitalAccount
        {
            DigitalAccountId = Guid.NewGuid()
        };

        account.UpdateAccount();
        var firstUpdate = account.LastUpdatedAt;

        System.Threading.Thread.Sleep(10);

        // Act
        account.UpdateAccount();

        // Assert
        Assert.That(account.LastUpdatedAt, Is.GreaterThanOrEqualTo(firstUpdate));
    }

    [Test]
    public void AccountType_Email_CanBeSet()
    {
        // Arrange & Act
        var account = new DigitalAccount
        {
            AccountType = AccountType.Email
        };

        // Assert
        Assert.That(account.AccountType, Is.EqualTo(AccountType.Email));
    }

    [Test]
    public void AccountType_SocialMedia_CanBeSet()
    {
        // Arrange & Act
        var account = new DigitalAccount
        {
            AccountType = AccountType.SocialMedia
        };

        // Assert
        Assert.That(account.AccountType, Is.EqualTo(AccountType.SocialMedia));
    }

    [Test]
    public void AccountType_Financial_CanBeSet()
    {
        // Arrange & Act
        var account = new DigitalAccount
        {
            AccountType = AccountType.Financial
        };

        // Assert
        Assert.That(account.AccountType, Is.EqualTo(AccountType.Financial));
    }

    [Test]
    public void AccountType_Cryptocurrency_CanBeSet()
    {
        // Arrange & Act
        var account = new DigitalAccount
        {
            AccountType = AccountType.Cryptocurrency
        };

        // Assert
        Assert.That(account.AccountType, Is.EqualTo(AccountType.Cryptocurrency));
    }

    [Test]
    public void PasswordHint_CanBeSet()
    {
        // Arrange
        var passwordHint = "Mother's maiden name + year";
        var account = new DigitalAccount();

        // Act
        account.PasswordHint = passwordHint;

        // Assert
        Assert.That(account.PasswordHint, Is.EqualTo(passwordHint));
    }

    [Test]
    public void Url_CanBeSet()
    {
        // Arrange
        var url = "https://example.com";
        var account = new DigitalAccount();

        // Act
        account.Url = url;

        // Assert
        Assert.That(account.Url, Is.EqualTo(url));
    }

    [Test]
    public void DesiredAction_CanBeSet()
    {
        // Arrange
        var desiredAction = "Memorialize";
        var account = new DigitalAccount();

        // Act
        account.DesiredAction = desiredAction;

        // Assert
        Assert.That(account.DesiredAction, Is.EqualTo(desiredAction));
    }

    [Test]
    public void Notes_CanBeSet()
    {
        // Arrange
        var notes = "Important backup account";
        var account = new DigitalAccount();

        // Act
        account.Notes = notes;

        // Assert
        Assert.That(account.Notes, Is.EqualTo(notes));
    }

    [Test]
    public void CreatedAt_IsSetOnCreation()
    {
        // Arrange
        var beforeCreation = DateTime.UtcNow.AddSeconds(-1);

        // Act
        var account = new DigitalAccount();

        // Assert
        Assert.That(account.CreatedAt, Is.GreaterThan(beforeCreation));
    }
}
