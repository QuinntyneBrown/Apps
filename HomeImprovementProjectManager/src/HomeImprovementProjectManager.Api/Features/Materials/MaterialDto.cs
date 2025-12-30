// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeImprovementProjectManager.Core;

namespace HomeImprovementProjectManager.Api;

/// <summary>
/// Data transfer object for Material.
/// </summary>
public record MaterialDto
{
    /// <summary>
    /// Gets or sets the material ID.
    /// </summary>
    public Guid MaterialId { get; init; }

    /// <summary>
    /// Gets or sets the project ID.
    /// </summary>
    public Guid ProjectId { get; init; }

    /// <summary>
    /// Gets or sets the material name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the quantity.
    /// </summary>
    public int Quantity { get; init; }

    /// <summary>
    /// Gets or sets the unit.
    /// </summary>
    public string? Unit { get; init; }

    /// <summary>
    /// Gets or sets the unit cost.
    /// </summary>
    public decimal? UnitCost { get; init; }

    /// <summary>
    /// Gets or sets the total cost.
    /// </summary>
    public decimal? TotalCost { get; init; }

    /// <summary>
    /// Gets or sets the supplier.
    /// </summary>
    public string? Supplier { get; init; }

    /// <summary>
    /// Gets or sets the created timestamp.
    /// </summary>
    public DateTime CreatedAt { get; init; }
}

/// <summary>
/// Extension methods for Material.
/// </summary>
public static class MaterialExtensions
{
    /// <summary>
    /// Converts a Material to a DTO.
    /// </summary>
    /// <param name="material">The material.</param>
    /// <returns>The DTO.</returns>
    public static MaterialDto ToDto(this Material material)
    {
        return new MaterialDto
        {
            MaterialId = material.MaterialId,
            ProjectId = material.ProjectId,
            Name = material.Name,
            Quantity = material.Quantity,
            Unit = material.Unit,
            UnitCost = material.UnitCost,
            TotalCost = material.TotalCost,
            Supplier = material.Supplier,
            CreatedAt = material.CreatedAt,
        };
    }
}
