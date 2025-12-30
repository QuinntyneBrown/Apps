// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PasswordAccountAuditor.Core.Tests;

public class SecurityLevelTests
{
    [Test]
    public void SecurityLevel_AllValues_CanBeAssigned()
    {
        // Arrange
        var levels = new[]
        {
            SecurityLevel.Unknown,
            SecurityLevel.Low,
            SecurityLevel.Medium,
            SecurityLevel.High
        };

        // Act & Assert
        foreach (var level in levels)
        {
            var account = new Account { SecurityLevel = level };
            Assert.That(account.SecurityLevel, Is.EqualTo(level));
        }
    }
}
