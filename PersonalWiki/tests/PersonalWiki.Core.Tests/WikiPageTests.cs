// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalWiki.Core.Tests;

public class WikiPageTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesWikiPage()
    {
        // Arrange & Act
        var page = new WikiPage();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(page.WikiPageId, Is.EqualTo(Guid.Empty));
            Assert.That(page.UserId, Is.EqualTo(Guid.Empty));
            Assert.That(page.CategoryId, Is.Null);
            Assert.That(page.Title, Is.EqualTo(string.Empty));
            Assert.That(page.Slug, Is.EqualTo(string.Empty));
            Assert.That(page.Content, Is.EqualTo(string.Empty));
            Assert.That(page.Status, Is.EqualTo(PageStatus.Draft));
            Assert.That(page.Version, Is.EqualTo(1));
            Assert.That(page.IsFeatured, Is.False);
            Assert.That(page.ViewCount, Is.EqualTo(0));
            Assert.That(page.LastModifiedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(page.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(page.Category, Is.Null);
            Assert.That(page.Revisions, Is.Not.Null);
            Assert.That(page.Revisions, Is.Empty);
            Assert.That(page.OutgoingLinks, Is.Not.Null);
            Assert.That(page.OutgoingLinks, Is.Empty);
            Assert.That(page.IncomingLinks, Is.Not.Null);
            Assert.That(page.IncomingLinks, Is.Empty);
        });
    }

    [Test]
    public void Constructor_WithProperties_SetsValuesCorrectly()
    {
        // Arrange
        var pageId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var categoryId = Guid.NewGuid();

        // Act
        var page = new WikiPage
        {
            WikiPageId = pageId,
            UserId = userId,
            CategoryId = categoryId,
            Title = "Test Page",
            Slug = "test-page",
            Content = "Test content",
            Status = PageStatus.Published,
            Version = 2,
            IsFeatured = true,
            ViewCount = 100
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(page.WikiPageId, Is.EqualTo(pageId));
            Assert.That(page.UserId, Is.EqualTo(userId));
            Assert.That(page.CategoryId, Is.EqualTo(categoryId));
            Assert.That(page.Title, Is.EqualTo("Test Page"));
            Assert.That(page.Slug, Is.EqualTo("test-page"));
            Assert.That(page.Content, Is.EqualTo("Test content"));
            Assert.That(page.Status, Is.EqualTo(PageStatus.Published));
            Assert.That(page.Version, Is.EqualTo(2));
            Assert.That(page.IsFeatured, Is.True);
            Assert.That(page.ViewCount, Is.EqualTo(100));
        });
    }

    [Test]
    public void UpdateContent_WhenCalled_UpdatesContentAndVersion()
    {
        // Arrange
        var page = new WikiPage
        {
            Content = "Old content",
            Version = 1
        };
        var newContent = "New content";

        // Act
        page.UpdateContent(newContent);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(page.Content, Is.EqualTo(newContent));
            Assert.That(page.Version, Is.EqualTo(2));
        });
    }

    [Test]
    public void UpdateContent_WhenCalled_UpdatesLastModifiedAt()
    {
        // Arrange
        var page = new WikiPage
        {
            LastModifiedAt = DateTime.UtcNow.AddHours(-1)
        };
        var beforeUpdate = DateTime.UtcNow;

        // Act
        page.UpdateContent("New content");

        // Assert
        var afterUpdate = DateTime.UtcNow;
        Assert.Multiple(() =>
        {
            Assert.That(page.LastModifiedAt, Is.GreaterThanOrEqualTo(beforeUpdate));
            Assert.That(page.LastModifiedAt, Is.LessThanOrEqualTo(afterUpdate));
        });
    }

    [Test]
    public void UpdateContent_CalledMultipleTimes_IncrementsVersionCorrectly()
    {
        // Arrange
        var page = new WikiPage
        {
            Version = 1
        };

        // Act
        page.UpdateContent("Content v2");
        page.UpdateContent("Content v3");
        page.UpdateContent("Content v4");

        // Assert
        Assert.That(page.Version, Is.EqualTo(4));
    }

    [Test]
    public void Publish_WhenCalled_SetsStatusToPublished()
    {
        // Arrange
        var page = new WikiPage
        {
            Status = PageStatus.Draft
        };

        // Act
        page.Publish();

        // Assert
        Assert.That(page.Status, Is.EqualTo(PageStatus.Published));
    }

    [Test]
    public void Publish_WhenCalled_UpdatesLastModifiedAt()
    {
        // Arrange
        var page = new WikiPage
        {
            LastModifiedAt = DateTime.UtcNow.AddHours(-1)
        };
        var beforePublish = DateTime.UtcNow;

        // Act
        page.Publish();

        // Assert
        var afterPublish = DateTime.UtcNow;
        Assert.Multiple(() =>
        {
            Assert.That(page.LastModifiedAt, Is.GreaterThanOrEqualTo(beforePublish));
            Assert.That(page.LastModifiedAt, Is.LessThanOrEqualTo(afterPublish));
        });
    }

    [Test]
    public void Archive_WhenCalled_SetsStatusToArchived()
    {
        // Arrange
        var page = new WikiPage
        {
            Status = PageStatus.Published
        };

        // Act
        page.Archive();

        // Assert
        Assert.That(page.Status, Is.EqualTo(PageStatus.Archived));
    }

    [Test]
    public void Archive_WhenCalled_UpdatesLastModifiedAt()
    {
        // Arrange
        var page = new WikiPage
        {
            LastModifiedAt = DateTime.UtcNow.AddHours(-1)
        };
        var beforeArchive = DateTime.UtcNow;

        // Act
        page.Archive();

        // Assert
        var afterArchive = DateTime.UtcNow;
        Assert.Multiple(() =>
        {
            Assert.That(page.LastModifiedAt, Is.GreaterThanOrEqualTo(beforeArchive));
            Assert.That(page.LastModifiedAt, Is.LessThanOrEqualTo(afterArchive));
        });
    }

    [Test]
    public void IncrementViewCount_WhenCalled_IncrementsCount()
    {
        // Arrange
        var page = new WikiPage
        {
            ViewCount = 0
        };

        // Act
        page.IncrementViewCount();

        // Assert
        Assert.That(page.ViewCount, Is.EqualTo(1));
    }

    [Test]
    public void IncrementViewCount_CalledMultipleTimes_IncrementsCorrectly()
    {
        // Arrange
        var page = new WikiPage
        {
            ViewCount = 5
        };

        // Act
        page.IncrementViewCount();
        page.IncrementViewCount();
        page.IncrementViewCount();

        // Assert
        Assert.That(page.ViewCount, Is.EqualTo(8));
    }

    [Test]
    public void Revisions_Collection_CanBeModified()
    {
        // Arrange
        var page = new WikiPage();
        var revision = new PageRevision
        {
            PageRevisionId = Guid.NewGuid(),
            Version = 1
        };

        // Act
        page.Revisions.Add(revision);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(page.Revisions.Count, Is.EqualTo(1));
            Assert.That(page.Revisions.First(), Is.EqualTo(revision));
        });
    }

    [Test]
    public void OutgoingLinks_Collection_CanBeModified()
    {
        // Arrange
        var page = new WikiPage();
        var link = new PageLink
        {
            PageLinkId = Guid.NewGuid(),
            SourcePageId = page.WikiPageId
        };

        // Act
        page.OutgoingLinks.Add(link);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(page.OutgoingLinks.Count, Is.EqualTo(1));
            Assert.That(page.OutgoingLinks.First(), Is.EqualTo(link));
        });
    }

    [Test]
    public void IncomingLinks_Collection_CanBeModified()
    {
        // Arrange
        var page = new WikiPage();
        var link = new PageLink
        {
            PageLinkId = Guid.NewGuid(),
            TargetPageId = page.WikiPageId
        };

        // Act
        page.IncomingLinks.Add(link);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(page.IncomingLinks.Count, Is.EqualTo(1));
            Assert.That(page.IncomingLinks.First(), Is.EqualTo(link));
        });
    }

    [Test]
    public void Category_NavigationProperty_CanBeSet()
    {
        // Arrange
        var page = new WikiPage();
        var category = new WikiCategory
        {
            WikiCategoryId = Guid.NewGuid(),
            Name = "Test Category"
        };

        // Act
        page.Category = category;

        // Assert
        Assert.That(page.Category, Is.EqualTo(category));
    }
}
