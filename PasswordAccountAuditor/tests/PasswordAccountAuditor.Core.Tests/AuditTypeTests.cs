// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PasswordAccountAuditor.Core.Tests;

public class AuditTypeTests
{
    [Test]
    public void AuditType_AllValues_CanBeAssigned()
    {
        // Arrange
        var types = new[]
        {
            AuditType.Manual,
            AuditType.Automated,
            AuditType.PasswordStrength,
            AuditType.TwoFactorCheck,
            AuditType.BreachDetection,
            AuditType.Compliance
        };

        // Act & Assert
        foreach (var type in types)
        {
            var audit = new SecurityAudit { AuditType = type };
            Assert.That(audit.AuditType, Is.EqualTo(type));
        }
    }
}
