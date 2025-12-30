// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PasswordAccountAuditor.Core.Tests;

public class PasswordChangedEventTests
{
    [Test]
    public void PasswordChangedEvent_CanBeCreated_WithAllProperties()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var changedAt = DateTime.UtcNow;

        // Act
        var evt = new PasswordChangedEvent
        {
            AccountId = accountId,
            NewSecurityLevel = SecurityLevel.High,
            ChangedAt = changedAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.AccountId, Is.EqualTo(accountId));
            Assert.That(evt.NewSecurityLevel, Is.EqualTo(SecurityLevel.High));
            Assert.That(evt.ChangedAt, Is.EqualTo(changedAt));
        });
    }
}
