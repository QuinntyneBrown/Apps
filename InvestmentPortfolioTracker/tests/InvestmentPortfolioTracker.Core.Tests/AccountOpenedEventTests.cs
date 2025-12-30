// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace InvestmentPortfolioTracker.Core.Tests;

public class AccountOpenedEventTests
{
    [Test]
    public void Constructor_ValidParameters_CreatesAccountOpenedEvent()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var name = "Brokerage Account";
        var accountType = AccountType.Taxable;
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new AccountOpenedEvent
        {
            AccountId = accountId,
            Name = name,
            AccountType = accountType,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.AccountId, Is.EqualTo(accountId));
            Assert.That(evt.Name, Is.EqualTo(name));
            Assert.That(evt.AccountType, Is.EqualTo(accountType));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void DefaultConstructor_SetsDefaultTimestamp()
    {
        var evt = new AccountOpenedEvent();
        Assert.Multiple(() =>
        {
            Assert.That(evt.AccountId, Is.EqualTo(Guid.Empty));
            Assert.That(evt.Name, Is.EqualTo(string.Empty));
            Assert.That(evt.Timestamp, Is.EqualTo(DateTime.UtcNow).Within(TimeSpan.FromSeconds(1)));
        });
    }

    [Test]
    public void Record_Equality_WorksCorrectly()
    {
        var accountId = Guid.NewGuid();
        var timestamp = new DateTime(2025, 1, 1);

        var evt1 = new AccountOpenedEvent
        {
            AccountId = accountId,
            Name = "Test Account",
            AccountType = AccountType.RothIRA,
            Timestamp = timestamp
        };

        var evt2 = new AccountOpenedEvent
        {
            AccountId = accountId,
            Name = "Test Account",
            AccountType = AccountType.RothIRA,
            Timestamp = timestamp
        };

        Assert.That(evt1, Is.EqualTo(evt2));
    }

    [Test]
    public void With_Expression_CreatesNewInstance()
    {
        var original = new AccountOpenedEvent
        {
            AccountId = Guid.NewGuid(),
            Name = "Original",
            AccountType = AccountType.Taxable,
            Timestamp = DateTime.UtcNow
        };

        var modified = original with { Name = "Modified" };

        Assert.Multiple(() =>
        {
            Assert.That(modified.AccountId, Is.EqualTo(original.AccountId));
            Assert.That(modified.Name, Is.EqualTo("Modified"));
            Assert.That(modified, Is.Not.SameAs(original));
        });
    }
}
