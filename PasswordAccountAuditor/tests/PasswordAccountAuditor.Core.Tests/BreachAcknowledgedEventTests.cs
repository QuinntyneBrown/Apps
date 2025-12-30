// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PasswordAccountAuditor.Core.Tests;

public class BreachAcknowledgedEventTests
{
    [Test]
    public void BreachAcknowledgedEvent_CanBeCreated_WithAllProperties()
    {
        // Arrange
        var breachAlertId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        var acknowledgedAt = DateTime.UtcNow;

        // Act
        var evt = new BreachAcknowledgedEvent
        {
            BreachAlertId = breachAlertId,
            AccountId = accountId,
            AcknowledgedAt = acknowledgedAt
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.BreachAlertId, Is.EqualTo(breachAlertId));
            Assert.That(evt.AccountId, Is.EqualTo(accountId));
            Assert.That(evt.AcknowledgedAt, Is.EqualTo(acknowledgedAt));
        });
    }
}
