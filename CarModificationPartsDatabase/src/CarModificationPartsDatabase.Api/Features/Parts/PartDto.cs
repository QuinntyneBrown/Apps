// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CarModificationPartsDatabase.Core;

namespace CarModificationPartsDatabase.Api;

/// <summary>
/// Data transfer object for Part.
/// </summary>
public record PartDto
{
    /// <summary>
    /// Gets or sets the part ID.
    /// </summary>
    public Guid PartId { get; init; }

    /// <summary>
    /// Gets or sets the part name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the part number or SKU.
    /// </summary>
    public string? PartNumber { get; init; }

    /// <summary>
    /// Gets or sets the manufacturer or brand.
    /// </summary>
    public string Manufacturer { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the part description.
    /// </summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the price of the part.
    /// </summary>
    public decimal Price { get; init; }

    /// <summary>
    /// Gets or sets the category of the part.
    /// </summary>
    public ModCategory Category { get; init; }

    /// <summary>
    /// Gets or sets the list of compatible vehicle models.
    /// </summary>
    public List<string> CompatibleVehicles { get; init; } = new List<string>();

    /// <summary>
    /// Gets or sets the warranty information.
    /// </summary>
    public string? WarrantyInfo { get; init; }

    /// <summary>
    /// Gets or sets the weight of the part in pounds.
    /// </summary>
    public decimal? Weight { get; init; }

    /// <summary>
    /// Gets or sets the dimensions of the part.
    /// </summary>
    public string? Dimensions { get; init; }

    /// <summary>
    /// Gets or sets whether the part is currently in stock.
    /// </summary>
    public bool InStock { get; init; }

    /// <summary>
    /// Gets or sets the supplier or vendor name.
    /// </summary>
    public string? Supplier { get; init; }

    /// <summary>
    /// Gets or sets additional notes.
    /// </summary>
    public string? Notes { get; init; }
}

/// <summary>
/// Extension methods for Part.
/// </summary>
public static class PartExtensions
{
    /// <summary>
    /// Converts a Part to a DTO.
    /// </summary>
    /// <param name="part">The part.</param>
    /// <returns>The DTO.</returns>
    public static PartDto ToDto(this Part part)
    {
        return new PartDto
        {
            PartId = part.PartId,
            Name = part.Name,
            PartNumber = part.PartNumber,
            Manufacturer = part.Manufacturer,
            Description = part.Description,
            Price = part.Price,
            Category = part.Category,
            CompatibleVehicles = part.CompatibleVehicles,
            WarrantyInfo = part.WarrantyInfo,
            Weight = part.Weight,
            Dimensions = part.Dimensions,
            InStock = part.InStock,
            Supplier = part.Supplier,
            Notes = part.Notes,
        };
    }
}
