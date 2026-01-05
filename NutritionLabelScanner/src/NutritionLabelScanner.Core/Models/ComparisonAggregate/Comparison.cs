// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace NutritionLabelScanner.Core;

/// <summary>
/// Represents a comparison between multiple products.
/// </summary>
public class Comparison
{
    /// <summary>
    /// Gets or sets the unique identifier for the comparison.
    /// </summary>
    public Guid ComparisonId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who created this comparison.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the comparison name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product IDs being compared (JSON array).
    /// </summary>
    public string ProductIds { get; set; } = "[]";

    /// <summary>
    /// Gets or sets the comparison notes or results.
    /// </summary>
    public string? Results { get; set; }

    /// <summary>
    /// Gets or sets the winner product ID if applicable.
    /// </summary>
    public Guid? WinnerProductId { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Checks if a winner has been selected.
    /// </summary>
    /// <returns>True if winner selected; otherwise, false.</returns>
    public bool HasWinner()
    {
        return WinnerProductId.HasValue;
    }
}
