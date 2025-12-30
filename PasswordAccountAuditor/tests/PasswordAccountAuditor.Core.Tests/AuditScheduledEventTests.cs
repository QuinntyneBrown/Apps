// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PasswordAccountAuditor.Core.Tests;

public class AuditScheduledEventTests
{
    [Test]
    public void AuditScheduledEvent_CanBeCreated_WithAllProperties()
    {
        // Arrange
        var securityAuditId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        var auditDate = DateTime.UtcNow.AddDays(1);
        var timestamp = DateTime.UtcNow;

        // Act
        var evt = new AuditScheduledEvent
        {
            SecurityAuditId = securityAuditId,
            AccountId = accountId,
            AuditType = AuditType.Automated,
            AuditDate = auditDate,
            Timestamp = timestamp
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.SecurityAuditId, Is.EqualTo(securityAuditId));
            Assert.That(evt.AccountId, Is.EqualTo(accountId));
            Assert.That(evt.AuditType, Is.EqualTo(AuditType.Automated));
            Assert.That(evt.AuditDate, Is.EqualTo(auditDate));
            Assert.That(evt.Timestamp, Is.EqualTo(timestamp));
        });
    }
}
