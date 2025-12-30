// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PasswordAccountAuditor.Core.Tests;

public class AuditStatusTests
{
    [Test]
    public void AuditStatus_AllValues_CanBeAssigned()
    {
        // Arrange
        var statuses = new[]
        {
            AuditStatus.Pending,
            AuditStatus.InProgress,
            AuditStatus.Completed,
            AuditStatus.Failed
        };

        // Act & Assert
        foreach (var status in statuses)
        {
            var audit = new SecurityAudit { Status = status };
            Assert.That(audit.Status, Is.EqualTo(status));
        }
    }
}
