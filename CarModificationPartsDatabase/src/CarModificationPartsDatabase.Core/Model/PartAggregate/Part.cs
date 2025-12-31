// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CarModificationPartsDatabase.Core;

/// <summary>
/// Represents a car modification part or component.
/// </summary>
public class Part
{
    /// <summary>
    /// Gets or sets the unique identifier for the part.
    /// </summary>
    public Guid PartId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the part name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the part number or SKU.
    /// </summary>
    public string? PartNumber { get; set; }

    /// <summary>
    /// Gets or sets the manufacturer or brand.
    /// </summary>
    public string Manufacturer { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the part description.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the price of the part.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the category of the part.
    /// </summary>
    public ModCategory Category { get; set; }

    /// <summary>
    /// Gets or sets the list of compatible vehicle models.
    /// </summary>
    public List<string> CompatibleVehicles { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets the warranty information.
    /// </summary>
    public string? WarrantyInfo { get; set; }

    /// <summary>
    /// Gets or sets the weight of the part in pounds.
    /// </summary>
    public decimal? Weight { get; set; }

    /// <summary>
    /// Gets or sets the dimensions of the part.
    /// </summary>
    public string? Dimensions { get; set; }

    /// <summary>
    /// Gets or sets whether the part is currently in stock.
    /// </summary>
    public bool InStock { get; set; } = true;

    /// <summary>
    /// Gets or sets the supplier or vendor name.
    /// </summary>
    public string? Supplier { get; set; }

    /// <summary>
    /// Gets or sets additional notes.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Updates the price of the part.
    /// </summary>
    /// <param name="newPrice">The new price.</param>
    /// <exception cref="ArgumentException">Thrown when price is negative.</exception>
    public void UpdatePrice(decimal newPrice)
    {
        if (newPrice < 0)
        {
            throw new ArgumentException("Price cannot be negative.", nameof(newPrice));
        }

        Price = newPrice;
    }

    /// <summary>
    /// Marks the part as out of stock.
    /// </summary>
    public void MarkOutOfStock()
    {
        InStock = false;
    }

    /// <summary>
    /// Marks the part as in stock.
    /// </summary>
    public void MarkInStock()
    {
        InStock = true;
    }

    /// <summary>
    /// Adds compatible vehicles to the part.
    /// </summary>
    /// <param name="vehicles">The vehicle models to add.</param>
    public void AddCompatibleVehicles(IEnumerable<string> vehicles)
    {
        CompatibleVehicles.AddRange(vehicles);
    }
}
