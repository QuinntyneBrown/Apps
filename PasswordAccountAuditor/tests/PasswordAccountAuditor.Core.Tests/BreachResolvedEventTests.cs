// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PasswordAccountAuditor.Core.Tests;

public class BreachResolvedEventTests
{
    [Test]
    public void BreachResolvedEvent_CanBeCreated_WithAllProperties()
    {
        // Arrange
        var breachAlertId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        var resolvedAt = DateTime.UtcNow;

        // Act
        var evt = new BreachResolvedEvent
        {
            BreachAlertId = breachAlertId,
            AccountId = accountId,
            ResolvedAt = resolvedAt,
            ResolutionNotes = "Password changed successfully"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.BreachAlertId, Is.EqualTo(breachAlertId));
            Assert.That(evt.AccountId, Is.EqualTo(accountId));
            Assert.That(evt.ResolvedAt, Is.EqualTo(resolvedAt));
            Assert.That(evt.ResolutionNotes, Is.EqualTo("Password changed successfully"));
        });
    }
}
