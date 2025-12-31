// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalWiki.Core;

/// <summary>
/// Represents a link between two wiki pages.
/// </summary>
public class PageLink
{
    /// <summary>
    /// Gets or sets the unique identifier for the link.
    /// </summary>
    public Guid PageLinkId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the source page ID.
    /// </summary>
    public Guid SourcePageId { get; set; }

    /// <summary>
    /// Gets or sets the target page ID.
    /// </summary>
    public Guid TargetPageId { get; set; }

    /// <summary>
    /// Gets or sets the anchor text or link context.
    /// </summary>
    public string? AnchorText { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the navigation property to the source page.
    /// </summary>
    public WikiPage? SourcePage { get; set; }

    /// <summary>
    /// Gets or sets the navigation property to the target page.
    /// </summary>
    public WikiPage? TargetPage { get; set; }
}
