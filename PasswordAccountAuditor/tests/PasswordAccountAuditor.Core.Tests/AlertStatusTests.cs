// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PasswordAccountAuditor.Core.Tests;

public class AlertStatusTests
{
    [Test]
    public void AlertStatus_AllValues_CanBeAssigned()
    {
        // Arrange
        var statuses = new[]
        {
            AlertStatus.New,
            AlertStatus.Acknowledged,
            AlertStatus.Resolved,
            AlertStatus.Dismissed
        };

        // Act & Assert
        foreach (var status in statuses)
        {
            var alert = new BreachAlert { Status = status };
            Assert.That(alert.Status, Is.EqualTo(status));
        }
    }
}
