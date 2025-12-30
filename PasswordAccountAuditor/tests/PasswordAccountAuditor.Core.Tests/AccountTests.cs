// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PasswordAccountAuditor.Core.Tests;

public class AccountTests
{
    [Test]
    public void Constructor_CreatesAccount_WithDefaultValues()
    {
        // Arrange & Act
        var account = new Account();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(account.AccountId, Is.EqualTo(Guid.Empty));
            Assert.That(account.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(account.AccountName, Is.EqualTo(string.Empty));
            Assert.That(account.Username, Is.EqualTo(string.Empty));
            Assert.That(account.WebsiteUrl, Is.Null);
            Assert.That(account.Category, Is.EqualTo(AccountCategory.SocialMedia));
            Assert.That(account.SecurityLevel, Is.EqualTo(SecurityLevel.Unknown));
            Assert.That(account.HasTwoFactorAuth, Is.False);
            Assert.That(account.LastPasswordChange, Is.Null);
            Assert.That(account.LastAccessDate, Is.Null);
            Assert.That(account.Notes, Is.Null);
            Assert.That(account.IsActive, Is.True);
            Assert.That(account.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(account.SecurityAudits, Is.Not.Null);
            Assert.That(account.SecurityAudits, Is.Empty);
            Assert.That(account.BreachAlerts, Is.Not.Null);
            Assert.That(account.BreachAlerts, Is.Empty);
        });
    }

    [Test]
    public void RecordPasswordChange_UpdatesLastPasswordChange()
    {
        // Arrange
        var account = new Account();
        var beforeChange = DateTime.UtcNow;

        // Act
        account.RecordPasswordChange();
        var afterChange = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(account.LastPasswordChange, Is.Not.Null);
            Assert.That(account.LastPasswordChange!.Value, Is.GreaterThanOrEqualTo(beforeChange));
            Assert.That(account.LastPasswordChange!.Value, Is.LessThanOrEqualTo(afterChange));
        });
    }

    [Test]
    public void RecordPasswordChange_UpdatesSecurityLevel()
    {
        // Arrange
        var account = new Account();

        // Act
        account.RecordPasswordChange();

        // Assert
        Assert.That(account.SecurityLevel, Is.Not.EqualTo(SecurityLevel.Unknown));
    }

    [Test]
    public void EnableTwoFactorAuth_SetsTwoFactorAuthToTrue()
    {
        // Arrange
        var account = new Account { HasTwoFactorAuth = false };

        // Act
        account.EnableTwoFactorAuth();

        // Assert
        Assert.That(account.HasTwoFactorAuth, Is.True);
    }

    [Test]
    public void EnableTwoFactorAuth_UpdatesSecurityLevel()
    {
        // Arrange
        var account = new Account();

        // Act
        account.EnableTwoFactorAuth();

        // Assert
        Assert.That(account.SecurityLevel, Is.Not.EqualTo(SecurityLevel.Unknown));
    }

    [Test]
    public void DisableTwoFactorAuth_SetsTwoFactorAuthToFalse()
    {
        // Arrange
        var account = new Account { HasTwoFactorAuth = true };

        // Act
        account.DisableTwoFactorAuth();

        // Assert
        Assert.That(account.HasTwoFactorAuth, Is.False);
    }

    [Test]
    public void DisableTwoFactorAuth_UpdatesSecurityLevel()
    {
        // Arrange
        var account = new Account();
        account.EnableTwoFactorAuth();
        var levelWithTwoFactor = account.SecurityLevel;

        // Act
        account.DisableTwoFactorAuth();

        // Assert
        Assert.That(account.SecurityLevel, Is.LessThan(levelWithTwoFactor));
    }

    [Test]
    public void RecordAccess_UpdatesLastAccessDate()
    {
        // Arrange
        var account = new Account();
        var beforeAccess = DateTime.UtcNow;

        // Act
        account.RecordAccess();
        var afterAccess = DateTime.UtcNow;

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(account.LastAccessDate, Is.Not.Null);
            Assert.That(account.LastAccessDate!.Value, Is.GreaterThanOrEqualTo(beforeAccess));
            Assert.That(account.LastAccessDate!.Value, Is.LessThanOrEqualTo(afterAccess));
        });
    }

    [Test]
    public void ToggleActive_TogglesIsActiveFromTrueToFalse()
    {
        // Arrange
        var account = new Account { IsActive = true };

        // Act
        account.ToggleActive();

        // Assert
        Assert.That(account.IsActive, Is.False);
    }

    [Test]
    public void ToggleActive_TogglesIsActiveFromFalseToTrue()
    {
        // Arrange
        var account = new Account { IsActive = false };

        // Act
        account.ToggleActive();

        // Assert
        Assert.That(account.IsActive, Is.True);
    }

    [Test]
    public void NeedsPasswordChange_ReturnsTrue_WhenLastPasswordChangeIsNull()
    {
        // Arrange
        var account = new Account { LastPasswordChange = null };

        // Act
        var result = account.NeedsPasswordChange();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void NeedsPasswordChange_ReturnsTrue_WhenPasswordIsOlderThan90Days()
    {
        // Arrange
        var account = new Account
        {
            LastPasswordChange = DateTime.UtcNow.AddDays(-91)
        };

        // Act
        var result = account.NeedsPasswordChange();

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void NeedsPasswordChange_ReturnsFalse_WhenPasswordIsExactly90DaysOld()
    {
        // Arrange
        var account = new Account
        {
            LastPasswordChange = DateTime.UtcNow.AddDays(-90)
        };

        // Act
        var result = account.NeedsPasswordChange();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void NeedsPasswordChange_ReturnsFalse_WhenPasswordIsRecent()
    {
        // Arrange
        var account = new Account
        {
            LastPasswordChange = DateTime.UtcNow.AddDays(-30)
        };

        // Act
        var result = account.NeedsPasswordChange();

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void Account_WithTwoFactorAndRecentPassword_HasHighSecurity()
    {
        // Arrange
        var account = new Account();

        // Act
        account.RecordPasswordChange();
        account.EnableTwoFactorAuth();

        // Assert
        Assert.That(account.SecurityLevel, Is.EqualTo(SecurityLevel.High));
    }

    [Test]
    public void Account_WithoutTwoFactor_HasLowSecurity()
    {
        // Arrange
        var account = new Account();

        // Act
        account.RecordPasswordChange();
        account.DisableTwoFactorAuth();

        // Assert
        Assert.That(account.SecurityLevel, Is.EqualTo(SecurityLevel.Low));
    }
}
