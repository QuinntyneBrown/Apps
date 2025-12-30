// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalWiki.Core.Tests;

public class PageLinkTests
{
    [Test]
    public void Constructor_DefaultValues_CreatesPageLink()
    {
        // Arrange & Act
        var link = new PageLink();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(link.PageLinkId, Is.EqualTo(Guid.Empty));
            Assert.That(link.SourcePageId, Is.EqualTo(Guid.Empty));
            Assert.That(link.TargetPageId, Is.EqualTo(Guid.Empty));
            Assert.That(link.AnchorText, Is.Null);
            Assert.That(link.CreatedAt, Is.Not.EqualTo(default(DateTime)));
            Assert.That(link.SourcePage, Is.Null);
            Assert.That(link.TargetPage, Is.Null);
        });
    }

    [Test]
    public void Constructor_WithProperties_SetsValuesCorrectly()
    {
        // Arrange
        var linkId = Guid.NewGuid();
        var sourcePageId = Guid.NewGuid();
        var targetPageId = Guid.NewGuid();

        // Act
        var link = new PageLink
        {
            PageLinkId = linkId,
            SourcePageId = sourcePageId,
            TargetPageId = targetPageId,
            AnchorText = "Click here"
        };

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(link.PageLinkId, Is.EqualTo(linkId));
            Assert.That(link.SourcePageId, Is.EqualTo(sourcePageId));
            Assert.That(link.TargetPageId, Is.EqualTo(targetPageId));
            Assert.That(link.AnchorText, Is.EqualTo("Click here"));
        });
    }

    [Test]
    public void SourcePage_NavigationProperty_CanBeSet()
    {
        // Arrange
        var link = new PageLink();
        var sourcePage = new WikiPage
        {
            WikiPageId = Guid.NewGuid(),
            Title = "Source Page"
        };

        // Act
        link.SourcePage = sourcePage;

        // Assert
        Assert.That(link.SourcePage, Is.EqualTo(sourcePage));
    }

    [Test]
    public void TargetPage_NavigationProperty_CanBeSet()
    {
        // Arrange
        var link = new PageLink();
        var targetPage = new WikiPage
        {
            WikiPageId = Guid.NewGuid(),
            Title = "Target Page"
        };

        // Act
        link.TargetPage = targetPage;

        // Assert
        Assert.That(link.TargetPage, Is.EqualTo(targetPage));
    }

    [Test]
    public void AnchorText_CanBeNull()
    {
        // Arrange & Act
        var link = new PageLink
        {
            AnchorText = null
        };

        // Assert
        Assert.That(link.AnchorText, Is.Null);
    }

    [Test]
    public void AnchorText_CanBeEmptyString()
    {
        // Arrange & Act
        var link = new PageLink
        {
            AnchorText = string.Empty
        };

        // Assert
        Assert.That(link.AnchorText, Is.EqualTo(string.Empty));
    }
}
