// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CarModificationPartsDatabase.Core;

namespace CarModificationPartsDatabase.Api;

/// <summary>
/// Data transfer object for Modification.
/// </summary>
public record ModificationDto
{
    /// <summary>
    /// Gets or sets the modification ID.
    /// </summary>
    public Guid ModificationId { get; init; }

    /// <summary>
    /// Gets or sets the modification name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the modification category.
    /// </summary>
    public ModCategory Category { get; init; }

    /// <summary>
    /// Gets or sets the detailed description of the modification.
    /// </summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the manufacturer or brand.
    /// </summary>
    public string? Manufacturer { get; init; }

    /// <summary>
    /// Gets or sets the estimated cost of the modification.
    /// </summary>
    public decimal? EstimatedCost { get; init; }

    /// <summary>
    /// Gets or sets the difficulty level (1-5).
    /// </summary>
    public int? DifficultyLevel { get; init; }

    /// <summary>
    /// Gets or sets the estimated installation time in hours.
    /// </summary>
    public decimal? EstimatedInstallationTime { get; init; }

    /// <summary>
    /// Gets or sets the performance gain description.
    /// </summary>
    public string? PerformanceGain { get; init; }

    /// <summary>
    /// Gets or sets the list of compatible vehicle models.
    /// </summary>
    public List<string> CompatibleVehicles { get; init; } = new List<string>();

    /// <summary>
    /// Gets or sets the list of required tools.
    /// </summary>
    public List<string> RequiredTools { get; init; } = new List<string>();

    /// <summary>
    /// Gets or sets additional notes.
    /// </summary>
    public string? Notes { get; init; }
}

/// <summary>
/// Extension methods for Modification.
/// </summary>
public static class ModificationExtensions
{
    /// <summary>
    /// Converts a Modification to a DTO.
    /// </summary>
    /// <param name="modification">The modification.</param>
    /// <returns>The DTO.</returns>
    public static ModificationDto ToDto(this Modification modification)
    {
        return new ModificationDto
        {
            ModificationId = modification.ModificationId,
            Name = modification.Name,
            Category = modification.Category,
            Description = modification.Description,
            Manufacturer = modification.Manufacturer,
            EstimatedCost = modification.EstimatedCost,
            DifficultyLevel = modification.DifficultyLevel,
            EstimatedInstallationTime = modification.EstimatedInstallationTime,
            PerformanceGain = modification.PerformanceGain,
            CompatibleVehicles = modification.CompatibleVehicles,
            RequiredTools = modification.RequiredTools,
            Notes = modification.Notes,
        };
    }
}
