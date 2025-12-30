// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace WarrantyReturnPeriodTracker.Core;

/// <summary>
/// Represents a purchase of a product with warranty and return information.
/// </summary>
public class Purchase
{
    /// <summary>
    /// Gets or sets the unique identifier for the purchase.
    /// </summary>
    public Guid PurchaseId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who made the purchase.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the product name.
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the product category.
    /// </summary>
    public ProductCategory Category { get; set; }

    /// <summary>
    /// Gets or sets the store or retailer name.
    /// </summary>
    public string StoreName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the purchase date.
    /// </summary>
    public DateTime PurchaseDate { get; set; }

    /// <summary>
    /// Gets or sets the purchase price.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the purchase status.
    /// </summary>
    public PurchaseStatus Status { get; set; } = PurchaseStatus.Active;

    /// <summary>
    /// Gets or sets the product model or serial number.
    /// </summary>
    public string? ModelNumber { get; set; }

    /// <summary>
    /// Gets or sets optional notes about the purchase.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the collection of warranties for this purchase.
    /// </summary>
    public ICollection<Warranty> Warranties { get; set; } = new List<Warranty>();

    /// <summary>
    /// Gets or sets the collection of return windows for this purchase.
    /// </summary>
    public ICollection<ReturnWindow> ReturnWindows { get; set; } = new List<ReturnWindow>();

    /// <summary>
    /// Gets or sets the collection of receipts for this purchase.
    /// </summary>
    public ICollection<Receipt> Receipts { get; set; } = new List<Receipt>();

    /// <summary>
    /// Marks the purchase as returned.
    /// </summary>
    /// <param name="returnDate">The date when the product was returned.</param>
    public void MarkAsReturned(DateTime returnDate)
    {
        Status = PurchaseStatus.Returned;
        Notes = string.IsNullOrEmpty(Notes)
            ? $"Returned on {returnDate:yyyy-MM-dd}"
            : $"{Notes}\n\nReturned on {returnDate:yyyy-MM-dd}";
    }

    /// <summary>
    /// Marks the purchase as disposed or no longer owned.
    /// </summary>
    public void MarkAsDisposed()
    {
        Status = PurchaseStatus.Disposed;
    }

    /// <summary>
    /// Checks if the purchase has an active warranty.
    /// </summary>
    /// <returns>True if there is an active warranty; otherwise, false.</returns>
    public bool HasActiveWarranty()
    {
        return Warranties.Any(w => w.IsActive());
    }

    /// <summary>
    /// Checks if the purchase has an open return window.
    /// </summary>
    /// <returns>True if there is an open return window; otherwise, false.</returns>
    public bool CanBeReturned()
    {
        return ReturnWindows.Any(rw => rw.IsOpen());
    }

    /// <summary>
    /// Gets the number of days since purchase.
    /// </summary>
    /// <returns>The number of days since the purchase date.</returns>
    public int DaysSincePurchase()
    {
        return (DateTime.UtcNow - PurchaseDate).Days;
    }
}
