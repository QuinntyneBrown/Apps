// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalWiki.Core;

/// <summary>
/// Represents a revision of a wiki page.
/// </summary>
public class PageRevision
{
    /// <summary>
    /// Gets or sets the unique identifier for the revision.
    /// </summary>
    public Guid PageRevisionId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the wiki page ID this revision belongs to.
    /// </summary>
    public Guid WikiPageId { get; set; }

    /// <summary>
    /// Gets or sets the version number.
    /// </summary>
    public int Version { get; set; }

    /// <summary>
    /// Gets or sets the content at this revision.
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the change summary or commit message.
    /// </summary>
    public string? ChangeSummary { get; set; }

    /// <summary>
    /// Gets or sets who made the revision.
    /// </summary>
    public string? RevisedBy { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the navigation property to the page.
    /// </summary>
    public WikiPage? Page { get; set; }
}
