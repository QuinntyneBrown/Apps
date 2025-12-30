// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PasswordAccountAuditor.Core.Tests;

public class TwoFactorAuthEnabledEventTests
{
    [Test]
    public void TwoFactorAuthEnabledEvent_CanBeCreated_WithAllProperties()
    {
        // Arrange
        var accountId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new TwoFactorAuthEnabledEvent
        {
            AccountId = accountId,
            NewSecurityLevel = SecurityLevel.High,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.AccountId, Is.EqualTo(accountId));
            Assert.That(evt.NewSecurityLevel, Is.EqualTo(SecurityLevel.High));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }
}
