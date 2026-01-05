// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace NutritionLabelScanner.Core;

/// <summary>
/// Represents a scanned product.
/// </summary>
public class Product
{
    /// <summary>
    /// Gets or sets the unique identifier for the product.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who scanned this product.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the product name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the brand name.
    /// </summary>
    public string? Brand { get; set; }

    /// <summary>
    /// Gets or sets the barcode/UPC.
    /// </summary>
    public string? Barcode { get; set; }

    /// <summary>
    /// Gets or sets the category (e.g., Dairy, Snacks, Beverages).
    /// </summary>
    public string? Category { get; set; }

    /// <summary>
    /// Gets or sets the serving size.
    /// </summary>
    public string? ServingSize { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the product was scanned.
    /// </summary>
    public DateTime ScannedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets optional notes.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the nutrition information for this product.
    /// </summary>
    public NutritionInfo? NutritionInfo { get; set; }

    /// <summary>
    /// Checks if the product has nutrition information.
    /// </summary>
    /// <returns>True if has nutrition info; otherwise, false.</returns>
    public bool HasNutritionInfo()
    {
        return NutritionInfo != null;
    }
}
