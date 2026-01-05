// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalNetWorthDashboard.Core;

/// <summary>
/// Represents a point-in-time snapshot of net worth.
/// </summary>
public class NetWorthSnapshot
{
    /// <summary>
    /// Gets or sets the unique identifier for the snapshot.
    /// </summary>
    public Guid NetWorthSnapshotId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the date of the snapshot.
    /// </summary>
    public DateTime SnapshotDate { get; set; }

    /// <summary>
    /// Gets or sets the total assets value.
    /// </summary>
    public decimal TotalAssets { get; set; }

    /// <summary>
    /// Gets or sets the total liabilities value.
    /// </summary>
    public decimal TotalLiabilities { get; set; }

    /// <summary>
    /// Gets or sets the net worth (total assets - total liabilities).
    /// </summary>
    public decimal NetWorth { get; set; }

    /// <summary>
    /// Gets or sets notes about this snapshot.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the date when the snapshot was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Calculates the net worth from total assets and liabilities.
    /// </summary>
    public void CalculateNetWorth()
    {
        NetWorth = TotalAssets - TotalLiabilities;
    }

    /// <summary>
    /// Updates the snapshot values.
    /// </summary>
    /// <param name="totalAssets">The total assets value.</param>
    /// <param name="totalLiabilities">The total liabilities value.</param>
    public void UpdateValues(decimal totalAssets, decimal totalLiabilities)
    {
        if (totalAssets < 0)
        {
            throw new ArgumentException("Total assets cannot be negative.", nameof(totalAssets));
        }

        if (totalLiabilities < 0)
        {
            throw new ArgumentException("Total liabilities cannot be negative.", nameof(totalLiabilities));
        }

        TotalAssets = totalAssets;
        TotalLiabilities = totalLiabilities;
        CalculateNetWorth();
    }

    /// <summary>
    /// Calculates the change in net worth compared to a previous snapshot.
    /// </summary>
    /// <param name="previousSnapshot">The previous snapshot to compare with.</param>
    /// <returns>The change in net worth.</returns>
    public decimal CalculateNetWorthChange(NetWorthSnapshot previousSnapshot)
    {
        return NetWorth - previousSnapshot.NetWorth;
    }

    /// <summary>
    /// Calculates the percentage change in net worth compared to a previous snapshot.
    /// </summary>
    /// <param name="previousSnapshot">The previous snapshot to compare with.</param>
    /// <returns>The percentage change, or null if previous net worth was zero.</returns>
    public decimal? CalculatePercentageChange(NetWorthSnapshot previousSnapshot)
    {
        if (previousSnapshot.NetWorth == 0)
        {
            return null;
        }

        return ((NetWorth - previousSnapshot.NetWorth) / Math.Abs(previousSnapshot.NetWorth)) * 100;
    }
}
