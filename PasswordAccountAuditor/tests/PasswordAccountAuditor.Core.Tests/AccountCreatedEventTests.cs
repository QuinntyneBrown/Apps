// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PasswordAccountAuditor.Core.Tests;

public class AccountCreatedEventTests
{
    [Test]
    public void AccountCreatedEvent_CanBeCreated_WithAllProperties()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new AccountCreatedEvent
        {
            AccountId = accountId,
            UserId = userId,
            AccountName = "Gmail",
            Username = "user@example.com",
            Category = AccountCategory.Email,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.AccountId, Is.EqualTo(accountId));
            Assert.That(evt.UserId, Is.EqualTo(userId));
            Assert.That(evt.AccountName, Is.EqualTo("Gmail"));
            Assert.That(evt.Username, Is.EqualTo("user@example.com"));
            Assert.That(evt.Category, Is.EqualTo(AccountCategory.Email));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }

    [Test]
    public void AccountCreatedEvent_IsRecord_SupportsValueEquality()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        var evt1 = new AccountCreatedEvent
        {
            AccountId = accountId,
            UserId = userId,
            AccountName = "Test",
            Username = "user@test.com",
            Category = AccountCategory.Work,
            Timestamp = timestamp
        };

        var evt2 = new AccountCreatedEvent
        {
            AccountId = accountId,
            UserId = userId,
            AccountName = "Test",
            Username = "user@test.com",
            Category = AccountCategory.Work,
            Timestamp = timestamp
        };

        // Act & Assert
        Assert.That(evt1, Is.EqualTo(evt2));
    }
}
