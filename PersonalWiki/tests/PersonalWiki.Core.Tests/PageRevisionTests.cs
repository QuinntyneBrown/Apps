// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalWiki.Core.Tests;

public class PageRevisionTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesPageRevision()
    {
        // Arrange & Act
        var revision = new PageRevision();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(revision.PageRevisionId, Is.EqualTo(Guid.Empty));
            Assert.That(revision.WikiPageId, Is.EqualTo(Guid.Empty));
            Assert.That(revision.Version, Is.EqualTo(0));
            Assert.That(revision.Content, Is.EqualTo(string.Empty));
            Assert.That(revision.ChangeSummary, Is.Null);
            Assert.That(revision.RevisedBy, Is.Null);
            Assert.That(revision.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(revision.Page, Is.Null);
        });
    }

    [Test]
    public void Constructor_WithProperties_SetsValuesCorrectly()
    {
        // Arrange
        var revisionId = Guid.NewGuid();
        var pageId = Guid.NewGuid();

        // Act
        var revision = new PageRevision
        {
            PageRevisionId = revisionId,
            WikiPageId = pageId,
            Version = 2,
            Content = "Revised content",
            ChangeSummary = "Updated introduction",
            RevisedBy = "user@example.com"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(revision.PageRevisionId, Is.EqualTo(revisionId));
            Assert.That(revision.WikiPageId, Is.EqualTo(pageId));
            Assert.That(revision.Version, Is.EqualTo(2));
            Assert.That(revision.Content, Is.EqualTo("Revised content"));
            Assert.That(revision.ChangeSummary, Is.EqualTo("Updated introduction"));
            Assert.That(revision.RevisedBy, Is.EqualTo("user@example.com"));
        });
    }

    [Test]
    public void Page_NavigationProperty_CanBeSet()
    {
        // Arrange
        var revision = new PageRevision();
        var page = new WikiPage
        {
            WikiPageId = Guid.NewGuid(),
            Title = "Test Page"
        };

        // Act
        revision.Page = page;

        // Assert
        Assert.That(revision.Page, Is.EqualTo(page));
    }

    [Test]
    public void ChangeSummary_CanBeNull()
    {
        // Arrange & Act
        var revision = new PageRevision
        {
            ChangeSummary = null
        };

        // Assert
        Assert.That(revision.ChangeSummary, Is.Null);
    }

    [Test]
    public void RevisedBy_CanBeNull()
    {
        // Arrange & Act
        var revision = new PageRevision
        {
            RevisedBy = null
        };

        // Assert
        Assert.That(revision.RevisedBy, Is.Null);
    }

    [Test]
    public void Version_CanBeSetToAnyInteger()
    {
        // Arrange & Act
        var revision = new PageRevision
        {
            Version = 100
        };

        // Assert
        Assert.That(revision.Version, Is.EqualTo(100));
    }
}
