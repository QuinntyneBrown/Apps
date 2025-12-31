// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PasswordAccountAuditor.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using PasswordAccountAuditor.Core.Model.UserAggregate;
using PasswordAccountAuditor.Core.Model.UserAggregate.Entities;
using PasswordAccountAuditor.Core.Services;
namespace PasswordAccountAuditor.Infrastructure;

/// <summary>
/// Provides seed data for the PasswordAccountAuditor database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(PasswordAccountAuditorContext context context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Accounts.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedAccountsAsync(context);
                logger.LogInformation("Initial data seeded successfully.");
            }
            else
            {
                logger.LogInformation("Database already contains data. Skipping seed.");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    private static async Task SeedAccountsAsync(PasswordAccountAuditorContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var accounts = new List<Account>
        {
            new Account
            {
                AccountId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                AccountName = "Gmail",
                Username = "user@gmail.com",
                WebsiteUrl = "https://gmail.com",
                Category = AccountCategory.Email,
                SecurityLevel = SecurityLevel.High,
                HasTwoFactorAuth = true,
                LastPasswordChange = DateTime.UtcNow.AddDays(-45),
                LastAccessDate = DateTime.UtcNow.AddDays(-1),
                Notes = "Primary email account",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Account
            {
                AccountId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                AccountName = "Facebook",
                Username = "user@example.com",
                WebsiteUrl = "https://facebook.com",
                Category = AccountCategory.SocialMedia,
                SecurityLevel = SecurityLevel.Medium,
                HasTwoFactorAuth = true,
                LastPasswordChange = DateTime.UtcNow.AddDays(-120),
                LastAccessDate = DateTime.UtcNow.AddDays(-7),
                Notes = "Social media account",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Account
            {
                AccountId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                AccountName = "Bank of America",
                Username = "john.doe",
                WebsiteUrl = "https://bankofamerica.com",
                Category = AccountCategory.Banking,
                SecurityLevel = SecurityLevel.Low,
                HasTwoFactorAuth = false,
                LastPasswordChange = DateTime.UtcNow.AddDays(-200),
                LastAccessDate = DateTime.UtcNow.AddDays(-3),
                Notes = "Primary bank account - needs security upgrade",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Account
            {
                AccountId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                UserId = sampleUserId,
                AccountName = "Amazon",
                Username = "user@gmail.com",
                WebsiteUrl = "https://amazon.com",
                Category = AccountCategory.Shopping,
                SecurityLevel = SecurityLevel.High,
                HasTwoFactorAuth = true,
                LastPasswordChange = DateTime.UtcNow.AddDays(-30),
                LastAccessDate = DateTime.UtcNow,
                Notes = "Shopping account with payment info",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            },
            new Account
            {
                AccountId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                UserId = sampleUserId,
                AccountName = "LinkedIn",
                Username = "user@example.com",
                WebsiteUrl = "https://linkedin.com",
                Category = AccountCategory.Professional,
                SecurityLevel = SecurityLevel.Medium,
                HasTwoFactorAuth = false,
                LastPasswordChange = DateTime.UtcNow.AddDays(-150),
                LastAccessDate = DateTime.UtcNow.AddDays(-14),
                Notes = "Professional networking",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Accounts.AddRange(accounts);

        // Add security audit for the first account
        var securityAudit = new SecurityAudit
        {
            SecurityAuditId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            AccountId = accounts[0].AccountId,
            AuditType = AuditType.PasswordStrength,
            Status = AuditStatus.Completed,
            AuditDate = DateTime.UtcNow.AddDays(-7),
            Findings = "Password strength is excellent with proper complexity.",
            Recommendations = "Continue using strong passwords and enable regular password rotation.",
            SecurityScore = 92,
            Notes = "Automated security audit",
        };

        context.SecurityAudits.Add(securityAudit);

        // Add breach alert for the third account (bank)
        var breachAlert = new BreachAlert
        {
            BreachAlertId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            AccountId = accounts[2].AccountId,
            Severity = BreachSeverity.High,
            Status = AlertStatus.New,
            DetectedDate = DateTime.UtcNow.AddDays(-2),
            BreachDate = DateTime.UtcNow.AddDays(-30),
            Source = "HaveIBeenPwned",
            Description = "Your credentials were found in a data breach.",
            DataCompromised = "Email, Password",
            RecommendedActions = "Change your password immediately and enable two-factor authentication.",
            Notes = "Detected through automated breach monitoring",
        };

        context.BreachAlerts.Add(breachAlert);

        await context.SaveChangesAsync();
    }
}
