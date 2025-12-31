// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DigitalLegacyPlanner.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using DigitalLegacyPlanner.Core.Model.UserAggregate;
using DigitalLegacyPlanner.Core.Model.UserAggregate.Entities;
using DigitalLegacyPlanner.Core.Services;
namespace DigitalLegacyPlanner.Infrastructure;

/// <summary>
/// Provides seed data for the DigitalLegacyPlanner database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(DigitalLegacyPlannerContext context context, ILogger logger, IPasswordHasher passwordHasher)
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

    private static async Task SeedAccountsAsync(DigitalLegacyPlannerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var accounts = new List<DigitalAccount>
        {
            new DigitalAccount
            {
                DigitalAccountId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                AccountType = AccountType.Email,
                AccountName = "Primary Gmail",
                Username = "user@gmail.com",
                PasswordHint = "Stored in password manager",
                Url = "https://gmail.com",
                DesiredAction = "Transfer to trusted contact",
                Notes = "Main email account for personal communications",
                CreatedAt = DateTime.UtcNow,
            },
            new DigitalAccount
            {
                DigitalAccountId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserId = sampleUserId,
                AccountType = AccountType.SocialMedia,
                AccountName = "Facebook",
                Username = "user.profile",
                PasswordHint = "Stored in password manager",
                Url = "https://facebook.com",
                DesiredAction = "Memorialize",
                Notes = "Convert to memorial page",
                CreatedAt = DateTime.UtcNow,
            },
            new DigitalAccount
            {
                DigitalAccountId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserId = sampleUserId,
                AccountType = AccountType.Financial,
                AccountName = "Online Banking",
                Username = "customer123456",
                PasswordHint = "Contact bank directly",
                Url = "https://bank.com",
                DesiredAction = "Contact listed beneficiaries",
                Notes = "Primary checking and savings accounts",
                CreatedAt = DateTime.UtcNow,
            },
            new DigitalAccount
            {
                DigitalAccountId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                UserId = sampleUserId,
                AccountType = AccountType.CloudStorage,
                AccountName = "Google Drive",
                Username = "user@gmail.com",
                PasswordHint = "Stored in password manager",
                Url = "https://drive.google.com",
                DesiredAction = "Download and archive important files",
                Notes = "Contains family photos and important documents",
                CreatedAt = DateTime.UtcNow,
            },
            new DigitalAccount
            {
                DigitalAccountId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                UserId = sampleUserId,
                AccountType = AccountType.Cryptocurrency,
                AccountName = "Crypto Wallet",
                Username = "N/A",
                PasswordHint = "Recovery seed phrase in safe deposit box",
                Url = null,
                DesiredAction = "Transfer to beneficiary",
                Notes = "Hardware wallet - seed phrase location documented",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Accounts.AddRange(accounts);

        // Seed trusted contacts
        var contacts = new List<TrustedContact>
        {
            new TrustedContact
            {
                TrustedContactId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                FullName = "Jane Smith",
                Relationship = "Spouse",
                Email = "jane.smith@email.com",
                PhoneNumber = "+1-555-0101",
                Role = "Primary Executor",
                IsPrimaryContact = true,
                IsNotified = true,
                Notes = "Has power of attorney and access to safe deposit box",
                CreatedAt = DateTime.UtcNow,
            },
            new TrustedContact
            {
                TrustedContactId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                FullName = "John Doe Jr.",
                Relationship = "Son",
                Email = "john.jr@email.com",
                PhoneNumber = "+1-555-0102",
                Role = "Secondary Contact",
                IsPrimaryContact = false,
                IsNotified = true,
                Notes = "Tech-savvy, can handle digital accounts",
                CreatedAt = DateTime.UtcNow,
            },
            new TrustedContact
            {
                TrustedContactId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                FullName = "Robert Johnson",
                Relationship = "Attorney",
                Email = "r.johnson@lawfirm.com",
                PhoneNumber = "+1-555-0103",
                Role = "Legal Advisor",
                IsPrimaryContact = false,
                IsNotified = true,
                Notes = "Family attorney, handles estate matters",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Contacts.AddRange(contacts);

        // Seed documents
        var documents = new List<LegacyDocument>
        {
            new LegacyDocument
            {
                LegacyDocumentId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Title = "Last Will and Testament",
                DocumentType = "Legal - Will",
                FilePath = "/documents/will_2024.pdf",
                Description = "Most recent version of will",
                PhysicalLocation = "Safe deposit box at First National Bank",
                AccessGrantedTo = "Jane Smith, Robert Johnson",
                IsEncrypted = true,
                LastReviewedAt = DateTime.UtcNow.AddMonths(-3),
                CreatedAt = DateTime.UtcNow,
            },
            new LegacyDocument
            {
                LegacyDocumentId = Guid.Parse("55555555-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Title = "Power of Attorney",
                DocumentType = "Legal - POA",
                FilePath = "/documents/poa_2024.pdf",
                Description = "Durable power of attorney for healthcare and financial decisions",
                PhysicalLocation = "Safe deposit box at First National Bank",
                AccessGrantedTo = "Jane Smith",
                IsEncrypted = true,
                LastReviewedAt = DateTime.UtcNow.AddMonths(-3),
                CreatedAt = DateTime.UtcNow,
            },
            new LegacyDocument
            {
                LegacyDocumentId = Guid.Parse("66666666-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Title = "Life Insurance Policy",
                DocumentType = "Financial - Insurance",
                FilePath = "/documents/life_insurance.pdf",
                Description = "Term life insurance policy #LI-123456",
                PhysicalLocation = "Home filing cabinet",
                AccessGrantedTo = "Jane Smith",
                IsEncrypted = false,
                LastReviewedAt = DateTime.UtcNow.AddMonths(-1),
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Documents.AddRange(documents);

        // Seed instructions
        var instructions = new List<LegacyInstruction>
        {
            new LegacyInstruction
            {
                LegacyInstructionId = Guid.Parse("77777777-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Title = "Social Media Account Handling",
                Content = "Please memorialize my Facebook account and delete my Twitter/X account. Instagram can be kept active for 6 months to allow friends to save photos.",
                Category = "Digital Accounts",
                Priority = 2,
                AssignedTo = "John Doe Jr.",
                ExecutionTiming = "Within 2 weeks",
                CreatedAt = DateTime.UtcNow,
            },
            new LegacyInstruction
            {
                LegacyInstructionId = Guid.Parse("88888888-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Title = "Email Account Access",
                Content = "Access my primary Gmail account and forward important emails to Jane Smith. Look for any pending financial transactions or subscriptions that need to be cancelled.",
                Category = "Digital Accounts",
                Priority = 1,
                AssignedTo = "Jane Smith",
                ExecutionTiming = "Immediately",
                CreatedAt = DateTime.UtcNow,
            },
            new LegacyInstruction
            {
                LegacyInstructionId = Guid.Parse("99999999-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Title = "Cloud Storage Archive",
                Content = "Download all files from Google Drive, especially the 'Family Photos' and 'Important Documents' folders. Archive them and distribute copies to family members.",
                Category = "Data Preservation",
                Priority = 2,
                AssignedTo = "John Doe Jr.",
                ExecutionTiming = "Within 1 month",
                CreatedAt = DateTime.UtcNow,
            },
            new LegacyInstruction
            {
                LegacyInstructionId = Guid.Parse("aaaaaaaa-1111-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Title = "Cryptocurrency Transfer",
                Content = "Use the recovery seed phrase in the safe deposit box to access the hardware wallet. Transfer all cryptocurrency to Jane Smith's wallet address provided in separate documentation.",
                Category = "Financial",
                Priority = 1,
                AssignedTo = "Jane Smith",
                ExecutionTiming = "After legal consultation",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Instructions.AddRange(instructions);

        await context.SaveChangesAsync();
    }
}
