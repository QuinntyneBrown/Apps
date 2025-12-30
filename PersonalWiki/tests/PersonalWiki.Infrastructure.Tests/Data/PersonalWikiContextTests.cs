// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalWiki.Infrastructure.Tests;

/// <summary>
/// Unit tests for the PersonalWikiContext.
/// </summary>
[TestFixture]
public class PersonalWikiContextTests
{
    private PersonalWikiContext _context = null!;

    /// <summary>
    /// Sets up the test context.
    /// </summary>
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<PersonalWikiContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new PersonalWikiContext(options);
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
    /// Tests that WikiPages can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Pages_CanAddAndRetrieve()
    {
        // Arrange
        var page = new WikiPage
        {
            WikiPageId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Page",
            Slug = "test-page",
            Content = "This is test content",
            Status = PageStatus.Published,
            Version = 1,
            ViewCount = 0,
            LastModifiedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Pages.Add(page);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Pages.FindAsync(page.WikiPageId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Title, Is.EqualTo("Test Page"));
        Assert.That(retrieved.Slug, Is.EqualTo("test-page"));
        Assert.That(retrieved.Status, Is.EqualTo(PageStatus.Published));
    }

    /// <summary>
    /// Tests that WikiCategories can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Categories_CanAddAndRetrieve()
    {
        // Arrange
        var category = new WikiCategory
        {
            WikiCategoryId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Name = "Test Category",
            Description = "This is a test category",
            Icon = "📚",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Categories.FindAsync(category.WikiCategoryId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Name, Is.EqualTo("Test Category"));
        Assert.That(retrieved.Icon, Is.EqualTo("📚"));
    }

    /// <summary>
    /// Tests that PageRevisions can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Revisions_CanAddAndRetrieve()
    {
        // Arrange
        var page = new WikiPage
        {
            WikiPageId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Page",
            Slug = "test-page",
            Content = "Current content",
            Status = PageStatus.Published,
            Version = 2,
            LastModifiedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        var revision = new PageRevision
        {
            PageRevisionId = Guid.NewGuid(),
            WikiPageId = page.WikiPageId,
            Version = 1,
            Content = "Old content",
            ChangeSummary = "Initial version",
            RevisedBy = "Test User",
            CreatedAt = DateTime.UtcNow.AddDays(-1),
        };

        // Act
        _context.Pages.Add(page);
        _context.Revisions.Add(revision);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Revisions.FindAsync(revision.PageRevisionId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.Version, Is.EqualTo(1));
        Assert.That(retrieved.Content, Is.EqualTo("Old content"));
    }

    /// <summary>
    /// Tests that PageLinks can be added and retrieved.
    /// </summary>
    [Test]
    public async Task Links_CanAddAndRetrieve()
    {
        // Arrange
        var page1 = new WikiPage
        {
            WikiPageId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Source Page",
            Slug = "source-page",
            Content = "Content",
            Status = PageStatus.Published,
            Version = 1,
            LastModifiedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        var page2 = new WikiPage
        {
            WikiPageId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Target Page",
            Slug = "target-page",
            Content = "Content",
            Status = PageStatus.Published,
            Version = 1,
            LastModifiedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        var link = new PageLink
        {
            PageLinkId = Guid.NewGuid(),
            SourcePageId = page1.WikiPageId,
            TargetPageId = page2.WikiPageId,
            AnchorText = "Link to target",
            CreatedAt = DateTime.UtcNow,
        };

        // Act
        _context.Pages.AddRange(page1, page2);
        _context.Links.Add(link);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Links.FindAsync(link.PageLinkId);

        // Assert
        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.SourcePageId, Is.EqualTo(page1.WikiPageId));
        Assert.That(retrieved.TargetPageId, Is.EqualTo(page2.WikiPageId));
    }

    /// <summary>
    /// Tests that cascade delete works for related entities.
    /// </summary>
    [Test]
    public async Task CascadeDelete_RemovesRelatedEntities()
    {
        // Arrange
        var page = new WikiPage
        {
            WikiPageId = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Title = "Test Page",
            Slug = "test-page",
            Content = "Content",
            Status = PageStatus.Published,
            Version = 1,
            LastModifiedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        var revision = new PageRevision
        {
            PageRevisionId = Guid.NewGuid(),
            WikiPageId = page.WikiPageId,
            Version = 1,
            Content = "Old content",
            CreatedAt = DateTime.UtcNow,
        };

        _context.Pages.Add(page);
        _context.Revisions.Add(revision);
        await _context.SaveChangesAsync();

        // Act
        _context.Pages.Remove(page);
        await _context.SaveChangesAsync();

        var retrievedRevision = await _context.Revisions.FindAsync(revision.PageRevisionId);

        // Assert
        Assert.That(retrievedRevision, Is.Null);
    }
}
