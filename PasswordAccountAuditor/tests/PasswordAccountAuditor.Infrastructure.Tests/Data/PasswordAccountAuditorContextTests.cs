// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PasswordAccountAuditor.Infrastructure.Tests;

/// <summary>
/// Unit tests for the PasswordAccountAuditorContext.
/// </summary>
[TestFixture]
public class PasswordAccountAuditorContextTests
{
    private PasswordAccountAuditorContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<PasswordAccountAuditorContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new PasswordAccountAuditorContext(options);
    }

    /// <summary>
    /// Tears down the test context.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    /// <summary>
    /// Tests that Accounts can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Accounts_CanAddAndRetrieve()
    {
        // Arrange
        var account = new Account
        {
            AccountId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            AccountName = "Test Account",
            Username = "testuser@example.com",
            WebsiteUrl = "https://test.com",
            Category = AccountCategory.Email,
            SecurityLevel = SecurityLevel.High,
            HasTwoFactorAuth = true,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Accounts.FindAsync(account.AccountId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.AccountName, Is.EqualTo("Test Account"));
        Assert.That(retrieved.Username, Is.EqualTo("testuser@example.com"));
        Assert.That(retrieved.Category, Is.EqualTo(AccountCategory.Email));
    }

    /// <summary>
    /// Tests that SecurityAudits can be added and retrieved.
    /// </summary>
    [Test]
    public async Task SecurityAudits_CanAddAndRetrieve()
    {
        // Arrange
        var account = new Account
        {
            AccountId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            AccountName = "Test Account",
            Username = "testuser@example.com",
            Category = AccountCategory.Banking,
            SecurityLevel = SecurityLevel.Medium,
            HasTwoFactorAuth = false,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        var securityAudit = new SecurityAudit
        {
            SecurityAuditId = Guid.NewGuid(),
            AccountId = account.AccountId,
            AuditType = AuditType.PasswordStrength,
            Status = AuditStatus.Completed,
            AuditDate = DateTime.UtcNow,
            Findings = "Password strength is weak",
            SecurityScore = 45,
        };

        // Act
        _context.Accounts.Add(account);
        _context.SecurityAudits.Add(securityAudit);
        await _context.SaveChangesAsync();

        var retrieved = await _context.SecurityAudits.FindAsync(securityAudit.SecurityAuditId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.AccountId, Is.EqualTo(account.AccountId));
        Assert.That(retrieved.AuditType, Is.EqualTo(AuditType.PasswordStrength));
        Assert.That(retrieved.SecurityScore, Is.EqualTo(45));
    }

    /// <summary>
    /// Tests that BreachAlerts can be added and retrieved.
    /// </summary>
    [Test]
    public async Task BreachAlerts_CanAddAndRetrieve()
    {
        // Arrange
        var account = new Account
        {
            AccountId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            AccountName = "Test Account",
            Username = "testuser@example.com",
            Category = AccountCategory.SocialMedia,
            SecurityLevel = SecurityLevel.Low,
            HasTwoFactorAuth = false,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        var breachAlert = new BreachAlert
        {
            BreachAlertId = Guid.NewGuid(),
            AccountId = account.AccountId,
            Severity = BreachSeverity.Critical,
            Status = AlertStatus.New,
            DetectedDate = DateTime.UtcNow,
            Description = "Critical breach detected",
            Source = "HaveIBeenPwned",
        };

        // Act
        _context.Accounts.Add(account);
        _context.BreachAlerts.Add(breachAlert);
        await _context.SaveChangesAsync();

        var retrieved = await _context.BreachAlerts.FindAsync(breachAlert.BreachAlertId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.AccountId, Is.EqualTo(account.AccountId));
        Assert.That(retrieved.Severity, Is.EqualTo(BreachSeverity.Critical));
        Assert.That(retrieved.Description, Is.EqualTo("Critical breach detected"));
    }

    /// <summary>
    /// Tests that cascade delete works for related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedEntities()
    {
        // Arrange
        var account = new Account
        {
            AccountId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            AccountName = "Test Account",
            Username = "testuser@example.com",
            Category = AccountCategory.Email,
            SecurityLevel = SecurityLevel.High,
            HasTwoFactorAuth = true,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
        };

        var securityAudit = new SecurityAudit
        {
            SecurityAuditId = Guid.NewGuid(),
            AccountId = account.AccountId,
            AuditType = AuditType.ComplianceCheck,
            Status = AuditStatus.Pending,
            AuditDate = DateTime.UtcNow,
            SecurityScore = 0,
        };

        _context.Accounts.Add(account);
        _context.SecurityAudits.Add(securityAudit);
        await _context.SaveChangesAsync();

        // Act
        _context.Accounts.Remove(account);
        await _context.SaveChangesAsync();

        var retrievedAudit = await _context.SecurityAudits.FindAsync(securityAudit.SecurityAuditId);

        // Assert
        Assert.That(retrievedAudit, Is.Null);
    }
}
