namespace DigitalLegacyPlanner.Core.Tests;

public class AccountTypeTests
{
    [Test]
    public void AccountType_Email_HasCorrectValue()
    {
        // Arrange & Act
        var accountType = AccountType.Email;

        // Assert
        Assert.That((int)accountType, Is.EqualTo(0));
    }

    [Test]
    public void AccountType_SocialMedia_HasCorrectValue()
    {
        // Arrange & Act
        var accountType = AccountType.SocialMedia;

        // Assert
        Assert.That((int)accountType, Is.EqualTo(1));
    }

    [Test]
    public void AccountType_Financial_HasCorrectValue()
    {
        // Arrange & Act
        var accountType = AccountType.Financial;

        // Assert
        Assert.That((int)accountType, Is.EqualTo(2));
    }

    [Test]
    public void AccountType_CloudStorage_HasCorrectValue()
    {
        // Arrange & Act
        var accountType = AccountType.CloudStorage;

        // Assert
        Assert.That((int)accountType, Is.EqualTo(3));
    }

    [Test]
    public void AccountType_Subscription_HasCorrectValue()
    {
        // Arrange & Act
        var accountType = AccountType.Subscription;

        // Assert
        Assert.That((int)accountType, Is.EqualTo(4));
    }

    [Test]
    public void AccountType_Shopping_HasCorrectValue()
    {
        // Arrange & Act
        var accountType = AccountType.Shopping;

        // Assert
        Assert.That((int)accountType, Is.EqualTo(5));
    }

    [Test]
    public void AccountType_Gaming_HasCorrectValue()
    {
        // Arrange & Act
        var accountType = AccountType.Gaming;

        // Assert
        Assert.That((int)accountType, Is.EqualTo(6));
    }

    [Test]
    public void AccountType_Professional_HasCorrectValue()
    {
        // Arrange & Act
        var accountType = AccountType.Professional;

        // Assert
        Assert.That((int)accountType, Is.EqualTo(7));
    }

    [Test]
    public void AccountType_Cryptocurrency_HasCorrectValue()
    {
        // Arrange & Act
        var accountType = AccountType.Cryptocurrency;

        // Assert
        Assert.That((int)accountType, Is.EqualTo(8));
    }

    [Test]
    public void AccountType_Other_HasCorrectValue()
    {
        // Arrange & Act
        var accountType = AccountType.Other;

        // Assert
        Assert.That((int)accountType, Is.EqualTo(9));
    }

    [Test]
    public void AccountType_CanBeAssignedToProperty()
    {
        // Arrange
        var account = new DigitalAccount();

        // Act
        account.AccountType = AccountType.Financial;

        // Assert
        Assert.That(account.AccountType, Is.EqualTo(AccountType.Financial));
    }

    [Test]
    public void AccountType_AllValuesAreUnique()
    {
        // Arrange
        var values = Enum.GetValues<AccountType>();

        // Act
        var uniqueValues = values.Distinct().ToList();

        // Assert
        Assert.That(uniqueValues.Count, Is.EqualTo(values.Length));
    }
}
