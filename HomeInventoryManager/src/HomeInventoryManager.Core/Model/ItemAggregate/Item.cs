// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeInventoryManager.Core;

/// <summary>
/// Represents an item in the home inventory.
/// </summary>
public class Item
{
    /// <summary>
    /// Gets or sets the unique identifier for the item.
    /// </summary>
    public Guid ItemId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this item.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the name of the item.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the item.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the category of the item.
    /// </summary>
    public Category Category { get; set; }

    /// <summary>
    /// Gets or sets the room where the item is located.
    /// </summary>
    public Room Room { get; set; }

    /// <summary>
    /// Gets or sets the brand or manufacturer.
    /// </summary>
    public string? Brand { get; set; }

    /// <summary>
    /// Gets or sets the model number.
    /// </summary>
    public string? ModelNumber { get; set; }

    /// <summary>
    /// Gets or sets the serial number.
    /// </summary>
    public string? SerialNumber { get; set; }

    /// <summary>
    /// Gets or sets the purchase date.
    /// </summary>
    public DateTime? PurchaseDate { get; set; }

    /// <summary>
    /// Gets or sets the purchase price.
    /// </summary>
    public decimal? PurchasePrice { get; set; }

    /// <summary>
    /// Gets or sets the current estimated value.
    /// </summary>
    public decimal? CurrentValue { get; set; }

    /// <summary>
    /// Gets or sets the quantity.
    /// </summary>
    public int Quantity { get; set; } = 1;

    /// <summary>
    /// Gets or sets the photo URL or path.
    /// </summary>
    public string? PhotoUrl { get; set; }

    /// <summary>
    /// Gets or sets receipt or documentation URLs.
    /// </summary>
    public string? ReceiptUrl { get; set; }

    /// <summary>
    /// Gets or sets notes about the item.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the last updated timestamp.
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the collection of value estimates for this item.
    /// </summary>
    public ICollection<ValueEstimate> ValueEstimates { get; set; } = new List<ValueEstimate>();

    /// <summary>
    /// Calculates the depreciation rate based on purchase date and current value.
    /// </summary>
    /// <returns>The depreciation rate as a percentage.</returns>
    public double? CalculateDepreciation()
    {
        if (PurchasePrice.HasValue && CurrentValue.HasValue && PurchasePrice > 0)
        {
            return (double)((PurchasePrice.Value - CurrentValue.Value) / PurchasePrice.Value * 100);
        }

        return null;
    }
}
