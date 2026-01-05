// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalWiki.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using PersonalWiki.Core.Models.UserAggregate;
using PersonalWiki.Core.Models.UserAggregate.Entities;
using PersonalWiki.Core.Services;
namespace PersonalWiki.Infrastructure;

/// <summary>
/// Provides seed data for the PersonalWiki database.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="logger">The logger.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SeedAsync(PersonalWikiContext context, ILogger logger, IPasswordHasher passwordHasher)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(passwordHasher);

        try
        {
            await context.Database.MigrateAsync();

            if (!await context.Categories.AnyAsync())
            {
                logger.LogInformation("Seeding initial data...");
                await SeedWikiDataAsync(context);
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

    private static async Task SeedWikiDataAsync(PersonalWikiContext context)
    {
        var sampleUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

        // Add categories
        var techCategory = new WikiCategory
        {
            WikiCategoryId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
            UserId = sampleUserId,
            Name = "Technology",
            Description = "Pages about technology and programming",
            Icon = "💻",
            CreatedAt = DateTime.UtcNow,
        };

        var programmingCategory = new WikiCategory
        {
            WikiCategoryId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
            UserId = sampleUserId,
            ParentCategoryId = techCategory.WikiCategoryId,
            Name = "Programming",
            Description = "Programming languages and concepts",
            Icon = "⚡",
            CreatedAt = DateTime.UtcNow,
        };

        var personalCategory = new WikiCategory
        {
            WikiCategoryId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
            UserId = sampleUserId,
            Name = "Personal",
            Description = "Personal notes and reflections",
            Icon = "📝",
            CreatedAt = DateTime.UtcNow,
        };

        context.Categories.AddRange(techCategory, programmingCategory, personalCategory);

        // Add pages
        var page1 = new WikiPage
        {
            WikiPageId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
            UserId = sampleUserId,
            CategoryId = programmingCategory.WikiCategoryId,
            Title = "C# Best Practices",
            Slug = "csharp-best-practices",
            Content = "# C# Best Practices\n\nA collection of best practices for C# development.\n\n## Naming Conventions\n- Use PascalCase for classes and methods\n- Use camelCase for local variables",
            Status = PageStatus.Published,
            Version = 1,
            IsFeatured = true,
            ViewCount = 42,
            LastModifiedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow.AddDays(-30),
        };

        var page2 = new WikiPage
        {
            WikiPageId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
            UserId = sampleUserId,
            CategoryId = techCategory.WikiCategoryId,
            Title = "Git Workflow",
            Slug = "git-workflow",
            Content = "# Git Workflow\n\nMy personal Git workflow for projects.\n\n## Branching Strategy\n- main: production-ready code\n- develop: integration branch\n- feature/*: new features",
            Status = PageStatus.Published,
            Version = 2,
            IsFeatured = false,
            ViewCount = 15,
            LastModifiedAt = DateTime.UtcNow.AddDays(-5),
            CreatedAt = DateTime.UtcNow.AddDays(-20),
        };

        var page3 = new WikiPage
        {
            WikiPageId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
            UserId = sampleUserId,
            CategoryId = personalCategory.WikiCategoryId,
            Title = "Daily Routines",
            Slug = "daily-routines",
            Content = "# Daily Routines\n\nMy morning and evening routines.\n\n## Morning\n- 6:00 AM - Wake up\n- 6:15 AM - Exercise\n- 7:00 AM - Breakfast",
            Status = PageStatus.Draft,
            Version = 1,
            IsFeatured = false,
            ViewCount = 5,
            LastModifiedAt = DateTime.UtcNow.AddDays(-2),
            CreatedAt = DateTime.UtcNow.AddDays(-2),
        };

        context.Pages.AddRange(page1, page2, page3);

        // Add a revision for page2
        var revision = new PageRevision
        {
            PageRevisionId = Guid.Parse("11111111-2222-3333-4444-555555555555"),
            WikiPageId = page2.WikiPageId,
            Version = 1,
            Content = "# Git Workflow\n\nMy personal Git workflow for projects.\n\n## Basic Commands\n- git add\n- git commit\n- git push",
            ChangeSummary = "Initial version",
            RevisedBy = "User",
            CreatedAt = DateTime.UtcNow.AddDays(-20),
        };

        context.Revisions.Add(revision);

        // Add links between pages
        var link = new PageLink
        {
            PageLinkId = Guid.Parse("22222222-3333-4444-5555-666666666666"),
            SourcePageId = page1.WikiPageId,
            TargetPageId = page2.WikiPageId,
            AnchorText = "Git Workflow",
            CreatedAt = DateTime.UtcNow,
        };

        context.Links.Add(link);

        await context.SaveChangesAsync();
    }
}
