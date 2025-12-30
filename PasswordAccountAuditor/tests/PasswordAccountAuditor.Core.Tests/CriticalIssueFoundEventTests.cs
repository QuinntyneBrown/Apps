// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PasswordAccountAuditor.Core.Tests;

public class CriticalIssueFoundEventTests
{
    [Test]
    public void CriticalIssueFoundEvent_CanBeCreated_WithAllProperties()
    {
        // Arrange
        var securityAuditId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new CriticalIssueFoundEvent
        {
            SecurityAuditId = securityAuditId,
            AccountId = accountId,
            IssueDescription = "Weak password detected",
            SecurityScore = 25,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.SecurityAuditId, Is.EqualTo(securityAuditId));
            Assert.That(evt.AccountId, Is.EqualTo(accountId));
            Assert.That(evt.IssueDescription, Is.EqualTo("Weak password detected"));
            Assert.That(evt.SecurityScore, Is.EqualTo(25));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }
}
