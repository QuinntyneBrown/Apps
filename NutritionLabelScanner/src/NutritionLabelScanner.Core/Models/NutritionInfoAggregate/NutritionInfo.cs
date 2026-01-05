// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace NutritionLabelScanner.Core;

/// <summary>
/// Represents nutrition information for a product.
/// </summary>
public class NutritionInfo
{
    /// <summary>
    /// Gets or sets the unique identifier for the nutrition info.
    /// </summary>
    public Guid NutritionInfoId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the product ID.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the calories per serving.
    /// </summary>
    public int Calories { get; set; }

    /// <summary>
    /// Gets or sets the total fat in grams.
    /// </summary>
    public decimal TotalFat { get; set; }

    /// <summary>
    /// Gets or sets the saturated fat in grams.
    /// </summary>
    public decimal? SaturatedFat { get; set; }

    /// <summary>
    /// Gets or sets the trans fat in grams.
    /// </summary>
    public decimal? TransFat { get; set; }

    /// <summary>
    /// Gets or sets the cholesterol in milligrams.
    /// </summary>
    public decimal? Cholesterol { get; set; }

    /// <summary>
    /// Gets or sets the sodium in milligrams.
    /// </summary>
    public decimal Sodium { get; set; }

    /// <summary>
    /// Gets or sets the total carbohydrates in grams.
    /// </summary>
    public decimal TotalCarbohydrates { get; set; }

    /// <summary>
    /// Gets or sets the dietary fiber in grams.
    /// </summary>
    public decimal? DietaryFiber { get; set; }

    /// <summary>
    /// Gets or sets the total sugars in grams.
    /// </summary>
    public decimal? TotalSugars { get; set; }

    /// <summary>
    /// Gets or sets the protein in grams.
    /// </summary>
    public decimal Protein { get; set; }

    /// <summary>
    /// Gets or sets additional nutrients (JSON format).
    /// </summary>
    public string? AdditionalNutrients { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the associated product.
    /// </summary>
    public Product? Product { get; set; }

    /// <summary>
    /// Checks if the product is high in sodium (>600mg per serving).
    /// </summary>
    /// <returns>True if high sodium; otherwise, false.</returns>
    public bool IsHighSodium()
    {
        return Sodium > 600;
    }

    /// <summary>
    /// Checks if the product is high in sugar (>15g per serving).
    /// </summary>
    /// <returns>True if high sugar; otherwise, false.</returns>
    public bool IsHighSugar()
    {
        return TotalSugars.HasValue && TotalSugars.Value > 15;
    }
}
