// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalWiki.Core;

/// <summary>
/// Represents a wiki page.
/// </summary>
public class WikiPage
{
    /// <summary>
    /// Gets or sets the unique identifier for the wiki page.
    /// </summary>
    public Guid WikiPageId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this page.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the category ID this page belongs to.
    /// </summary>
    public Guid? CategoryId { get; set; }

    /// <summary>
    /// Gets or sets the page title.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the page slug (URL-friendly identifier).
    /// </summary>
    public string Slug { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the current content of the page.
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the page status.
    /// </summary>
    public PageStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the version number.
    /// </summary>
    public int Version { get; set; } = 1;

    /// <summary>
    /// Gets or sets a value indicating whether the page is featured.
    /// </summary>
    public bool IsFeatured { get; set; }

    /// <summary>
    /// Gets or sets the view count.
    /// </summary>
    public int ViewCount { get; set; }

    /// <summary>
    /// Gets or sets the last modified timestamp.
    /// </summary>
    public DateTime LastModifiedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the navigation property to the category.
    /// </summary>
    public WikiCategory? Category { get; set; }

    /// <summary>
    /// Gets or sets the collection of revisions for this page.
    /// </summary>
    public ICollection<PageRevision> Revisions { get; set; } = new List<PageRevision>();

    /// <summary>
    /// Gets or sets the collection of outgoing links from this page.
    /// </summary>
    public ICollection<PageLink> OutgoingLinks { get; set; } = new List<PageLink>();

    /// <summary>
    /// Gets or sets the collection of incoming links to this page.
    /// </summary>
    public ICollection<PageLink> IncomingLinks { get; set; } = new List<PageLink>();

    /// <summary>
    /// Updates the page content and creates a new revision.
    /// </summary>
    /// <param name="newContent">The new content.</param>
    public void UpdateContent(string newContent)
    {
        Content = newContent;
        Version++;
        LastModifiedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Publishes the page.
    /// </summary>
    public void Publish()
    {
        Status = PageStatus.Published;
        LastModifiedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Archives the page.
    /// </summary>
    public void Archive()
    {
        Status = PageStatus.Archived;
        LastModifiedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Increments the view count.
    /// </summary>
    public void IncrementViewCount()
    {
        ViewCount++;
    }
}
