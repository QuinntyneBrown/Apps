// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PasswordAccountAuditor.Core.Tests;

public class BreachSeverityTests
{
    [Test]
    public void BreachSeverity_AllValues_CanBeAssigned()
    {
        // Arrange
        var severities = new[]
        {
            BreachSeverity.Low,
            BreachSeverity.Medium,
            BreachSeverity.High,
            BreachSeverity.Critical
        };

        // Act & Assert
        foreach (var severity in severities)
        {
            var alert = new BreachAlert { Severity = severity };
            Assert.That(alert.Severity, Is.EqualTo(severity));
        }
    }
}
