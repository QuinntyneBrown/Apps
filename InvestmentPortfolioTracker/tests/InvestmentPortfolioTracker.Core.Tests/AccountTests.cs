// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace InvestmentPortfolioTracker.Core.Tests;

public class AccountTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesAccount()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var name = "My Brokerage";
        var accountType = AccountType.Taxable;
        var institution = "Vanguard";
        var accountNumber = "123456789";
        var currentBalance = 10000m;
        var openedDate = new DateTime(2025, 1, 1);

        // Act
        var account = new Account
        {
            AccountId = accountId,
            Name = name,
            AccountType = accountType,
            Institution = institution,
            AccountNumber = accountNumber,
            CurrentBalance = currentBalance,
            OpenedDate = openedDate
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(account.AccountId, Is.EqualTo(accountId));
            Assert.That(account.Name, Is.EqualTo(name));
            Assert.That(account.AccountType, Is.EqualTo(accountType));
            Assert.That(account.Institution, Is.EqualTo(institution));
            Assert.That(account.AccountNumber, Is.EqualTo(accountNumber));
            Assert.That(account.CurrentBalance, Is.EqualTo(currentBalance));
            Assert.That(account.OpenedDate, Is.EqualTo(openedDate));
            Assert.That(account.IsActive, Is.True);
        });
    }

    [Test]
    public void DefaultConstructor_SetsDefaultValues()
    {
        // Act
        var account = new Account();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(account.AccountId, Is.EqualTo(Guid.Empty));
            Assert.That(account.Name, Is.EqualTo(string.Empty));
            Assert.That(account.Institution, Is.EqualTo(string.Empty));
            Assert.That(account.CurrentBalance, Is.EqualTo(0m));
            Assert.That(account.IsActive, Is.True);
        });
    }

    [Test]
    public void UpdateBalance_ValidAmount_UpdatesCurrentBalance()
    {
        // Arrange
        var account = new Account { CurrentBalance = 1000m };

        // Act
        account.UpdateBalance(2500m);

        // Assert
        Assert.That(account.CurrentBalance, Is.EqualTo(2500m));
    }

    [Test]
    public void UpdateBalance_ZeroAmount_SetsBalanceToZero()
    {
        // Arrange
        var account = new Account { CurrentBalance = 1000m };

        // Act
        account.UpdateBalance(0m);

        // Assert
        Assert.That(account.CurrentBalance, Is.EqualTo(0m));
    }

    [Test]
    public void Close_SetsIsActiveToFalse()
    {
        // Arrange
        var account = new Account { IsActive = true };

        // Act
        account.Close();

        // Assert
        Assert.That(account.IsActive, Is.False);
    }

    [Test]
    public void Close_AlreadyClosed_RemainsInactive()
    {
        // Arrange
        var account = new Account { IsActive = false };

        // Act
        account.Close();

        // Assert
        Assert.That(account.IsActive, Is.False);
    }

    [Test]
    public void AccountType_AllTypes_CanBeAssigned()
    {
        var account = new Account();
        Assert.DoesNotThrow(() => account.AccountType = AccountType.Taxable);
        Assert.DoesNotThrow(() => account.AccountType = AccountType.TraditionalIRA);
        Assert.DoesNotThrow(() => account.AccountType = AccountType.RothIRA);
        Assert.DoesNotThrow(() => account.AccountType = AccountType.FourZeroOneK);
        Assert.DoesNotThrow(() => account.AccountType = AccountType.FourZeroThreeB);
        Assert.DoesNotThrow(() => account.AccountType = AccountType.HSA);
        Assert.DoesNotThrow(() => account.AccountType = AccountType.FiveTwoNine);
    }

    [Test]
    public void IsActive_DefaultValue_IsTrue()
    {
        var account = new Account();
        Assert.That(account.IsActive, Is.True);
    }

    [Test]
    public void AccountNumber_OptionalField_CanBeNull()
    {
        var account = new Account { AccountNumber = null };
        Assert.That(account.AccountNumber, Is.Null);
    }

    [Test]
    public void Notes_OptionalField_CanBeNull()
    {
        var account = new Account { Notes = null };
        Assert.That(account.Notes, Is.Null);
    }
}
