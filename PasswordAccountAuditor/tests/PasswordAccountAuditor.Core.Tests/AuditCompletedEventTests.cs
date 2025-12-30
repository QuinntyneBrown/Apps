// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PasswordAccountAuditor.Core.Tests;

public class AuditCompletedEventTests
{
    [Test]
    public void AuditCompletedEvent_CanBeCreated_WithAllProperties()
    {
        // Arrange
        var securityAuditId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new AuditCompletedEvent
        {
            SecurityAuditId = securityAuditId,
            AccountId = accountId,
            SecurityScore = 85,
            HasCriticalIssues = false,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.SecurityAuditId, Is.EqualTo(securityAuditId));
            Assert.That(evt.AccountId, Is.EqualTo(accountId));
            Assert.That(evt.SecurityScore, Is.EqualTo(85));
            Assert.That(evt.HasCriticalIssues, Is.False);
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }
}
