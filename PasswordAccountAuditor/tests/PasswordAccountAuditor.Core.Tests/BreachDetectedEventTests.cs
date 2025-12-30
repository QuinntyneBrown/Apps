// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PasswordAccountAuditor.Core.Tests;

public class BreachDetectedEventTests
{
    [Test]
    public void BreachDetectedEvent_CanBeCreated_WithAllProperties()
    {
        // Arrange
        var breachAlertId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        var detectedDate = DateTime.UtcNow;

        // Act
        var evt = new BreachDetectedEvent
        {
            BreachAlertId = breachAlertId,
            AccountId = accountId,
            Severity = BreachSeverity.Critical,
            Description = "Password exposed in data breach",
            DetectedDate = detectedDate
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(evt.BreachAlertId, Is.EqualTo(breachAlertId));
            Assert.That(evt.AccountId, Is.EqualTo(accountId));
            Assert.That(evt.Severity, Is.EqualTo(BreachSeverity.Critical));
            Assert.That(evt.Description, Is.EqualTo("Password exposed in data breach"));
            Assert.That(evt.DetectedDate, Is.EqualTo(detectedDate));
        });
    }
}
