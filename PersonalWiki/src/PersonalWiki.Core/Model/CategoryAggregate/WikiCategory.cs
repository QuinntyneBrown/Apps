// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalWiki.Core;

/// <summary>
/// Represents a category for organizing wiki pages.
/// </summary>
public class WikiCategory
{
    /// <summary>
    /// Gets or sets the unique identifier for the category.
    /// </summary>
    public Guid WikiCategoryId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this category.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the category name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the parent category ID for hierarchical organization.
    /// </summary>
    public Guid? ParentCategoryId { get; set; }

    /// <summary>
    /// Gets or sets the category icon or emoji.
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the navigation property to the parent category.
    /// </summary>
    public WikiCategory? ParentCategory { get; set; }

    /// <summary>
    /// Gets or sets the collection of child categories.
    /// </summary>
    public ICollection<WikiCategory> ChildCategories { get; set; } = new List<WikiCategory>();

    /// <summary>
    /// Gets or sets the collection of pages in this category.
    /// </summary>
    public ICollection<WikiPage> Pages { get; set; } = new List<WikiPage>();

    /// <summary>
    /// Gets the count of pages in this category.
    /// </summary>
    /// <returns>The number of pages.</returns>
    public int GetPageCount()
    {
        return Pages.Count;
    }
}
