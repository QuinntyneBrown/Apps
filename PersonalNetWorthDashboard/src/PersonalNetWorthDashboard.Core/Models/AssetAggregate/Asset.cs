// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalNetWorthDashboard.Core;

/// <summary>
/// Represents an asset in the net worth dashboard.
/// </summary>
public class Asset
{
    /// <summary>
    /// Gets or sets the unique identifier for the asset.
    /// </summary>
    public Guid AssetId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the name of the asset.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the type of the asset.
    /// </summary>
    public AssetType AssetType { get; set; }

    /// <summary>
    /// Gets or sets the current value of the asset.
    /// </summary>
    public decimal CurrentValue { get; set; }

    /// <summary>
    /// Gets or sets the purchase price of the asset.
    /// </summary>
    public decimal? PurchasePrice { get; set; }

    /// <summary>
    /// Gets or sets the purchase date of the asset.
    /// </summary>
    public DateTime? PurchaseDate { get; set; }

    /// <summary>
    /// Gets or sets the institution holding the asset.
    /// </summary>
    public string? Institution { get; set; }

    /// <summary>
    /// Gets or sets the account number.
    /// </summary>
    public string? AccountNumber { get; set; }

    /// <summary>
    /// Gets or sets notes about the asset.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the date when the value was last updated.
    /// </summary>
    public DateTime LastUpdated { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets whether the asset is currently active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Updates the current value of the asset.
    /// </summary>
    /// <param name="newValue">The new value.</param>
    public void UpdateValue(decimal newValue)
    {
        if (newValue < 0)
        {
            throw new ArgumentException("Asset value cannot be negative.", nameof(newValue));
        }

        CurrentValue = newValue;
        LastUpdated = DateTime.UtcNow;
    }

    /// <summary>
    /// Calculates the gain or loss on the asset.
    /// </summary>
    /// <returns>The gain or loss amount, or null if purchase price is not set.</returns>
    public decimal? CalculateGainLoss()
    {
        return PurchasePrice.HasValue ? CurrentValue - PurchasePrice.Value : null;
    }

    /// <summary>
    /// Deactivates the asset.
    /// </summary>
    public void Deactivate()
    {
        IsActive = false;
    }

    /// <summary>
    /// Reactivates the asset.
    /// </summary>
    public void Reactivate()
    {
        IsActive = true;
    }
}
