// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using DocumentVaultOrganizer.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DocumentVaultOrganizer.Infrastructure;

/// <summary>
/// Provides seed data for the DocumentVaultOrganizer database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(DocumentVaultOrganizerContext context, ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Documents.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedDocumentsAsync(context);
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

    private static async Task SeedDocumentsAsync(DocumentVaultOrganizerContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        var categories = new List<DocumentCategory>
        {
            new DocumentCategory
            {
                DocumentCategoryId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Name = "Tax Documents",
                Description = "Annual tax returns and related documents",
                CreatedAt = DateTime.UtcNow,
            },
            new DocumentCategory
            {
                DocumentCategoryId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                Name = "Insurance Policies",
                Description = "Health, auto, and home insurance documents",
                CreatedAt = DateTime.UtcNow,
            },
            new DocumentCategory
            {
                DocumentCategoryId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                Name = "Legal Documents",
                Description = "Contracts, wills, and legal agreements",
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.DocumentCategories.AddRange(categories);

        var documents = new List<Document>
        {
            new Document
            {
                DocumentId = Guid.Parse("11111111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "2024 Tax Return",
                Category = DocumentCategoryEnum.Tax,
                FileUrl = "https://example.com/documents/tax-2024.pdf",
                ExpirationDate = new DateTime(2031, 4, 15),
                CreatedAt = DateTime.UtcNow,
            },
            new Document
            {
                DocumentId = Guid.Parse("22222222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Health Insurance Policy",
                Category = DocumentCategoryEnum.Insurance,
                FileUrl = "https://example.com/documents/health-insurance.pdf",
                ExpirationDate = DateTime.UtcNow.AddYears(1),
                CreatedAt = DateTime.UtcNow,
            },
            new Document
            {
                DocumentId = Guid.Parse("33333333-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Passport",
                Category = DocumentCategoryEnum.Legal,
                FileUrl = "https://example.com/documents/passport.pdf",
                ExpirationDate = DateTime.UtcNow.AddYears(5),
                CreatedAt = DateTime.UtcNow,
            },
            new Document
            {
                DocumentId = Guid.Parse("44444444-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Driver's License",
                Category = DocumentCategoryEnum.Legal,
                FileUrl = "https://example.com/documents/drivers-license.pdf",
                ExpirationDate = DateTime.UtcNow.AddYears(4),
                CreatedAt = DateTime.UtcNow,
            },
            new Document
            {
                DocumentId = Guid.Parse("55555555-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserId = sampleUserId,
                Name = "Medical Records",
                Category = DocumentCategoryEnum.Medical,
                FileUrl = "https://example.com/documents/medical-records.pdf",
                ExpirationDate = null,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.Documents.AddRange(documents);

        var alerts = new List<ExpirationAlert>
        {
            new ExpirationAlert
            {
                ExpirationAlertId = Guid.Parse("aaa11111-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                DocumentId = documents[1].DocumentId,
                AlertDate = documents[1].ExpirationDate!.Value.AddDays(-30),
                IsAcknowledged = false,
                CreatedAt = DateTime.UtcNow,
            },
            new ExpirationAlert
            {
                ExpirationAlertId = Guid.Parse("bbb22222-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                DocumentId = documents[2].DocumentId,
                AlertDate = documents[2].ExpirationDate!.Value.AddDays(-60),
                IsAcknowledged = false,
                CreatedAt = DateTime.UtcNow,
            },
        };

        context.ExpirationAlerts.AddRange(alerts);

        await context.SaveChangesAsync();
    }
}
