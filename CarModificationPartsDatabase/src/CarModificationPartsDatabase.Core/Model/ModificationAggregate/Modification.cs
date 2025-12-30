// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CarModificationPartsDatabase.Core;

/// <summary>
/// Represents a vehicle modification or upgrade.
/// </summary>
public class Modification
{
    /// <summary>
    /// Gets or sets the unique identifier for the modification.
    /// </summary>
    public Guid ModificationId { get; set; }

    /// <summary>
    /// Gets or sets the modification name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the modification category.
    /// </summary>
    public ModCategory Category { get; set; }

    /// <summary>
    /// Gets or sets the detailed description of the modification.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the manufacturer or brand.
    /// </summary>
    public string? Manufacturer { get; set; }

    /// <summary>
    /// Gets or sets the estimated cost of the modification.
    /// </summary>
    public decimal? EstimatedCost { get; set; }

    /// <summary>
    /// Gets or sets the difficulty level (1-5).
    /// </summary>
    public int? DifficultyLevel { get; set; }

    /// <summary>
    /// Gets or sets the estimated installation time in hours.
    /// </summary>
    public decimal? EstimatedInstallationTime { get; set; }

    /// <summary>
    /// Gets or sets the performance gain description.
    /// </summary>
    public string? PerformanceGain { get; set; }

    /// <summary>
    /// Gets or sets the list of compatible vehicle models.
    /// </summary>
    public List<string> CompatibleVehicles { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets the list of required tools.
    /// </summary>
    public List<string> RequiredTools { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets additional notes.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the collection of installations using this modification.
    /// </summary>
    public List<Installation> Installations { get; set; } = new List<Installation>();

    /// <summary>
    /// Adds compatible vehicles to the modification.
    /// </summary>
    /// <param name="vehicles">The vehicle models to add.</param>
    public void AddCompatibleVehicles(IEnumerable<string> vehicles)
    {
        CompatibleVehicles.AddRange(vehicles);
    }

    /// <summary>
    /// Adds required tools to the modification.
    /// </summary>
    /// <param name="tools">The tools to add.</param>
    public void AddRequiredTools(IEnumerable<string> tools)
    {
        RequiredTools.AddRange(tools);
    }

    /// <summary>
    /// Sets the difficulty level.
    /// </summary>
    /// <param name="level">The difficulty level (1-5).</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when level is not between 1 and 5.</exception>
    public void SetDifficultyLevel(int level)
    {
        if (level < 1 || level > 5)
        {
            throw new ArgumentOutOfRangeException(nameof(level), "Difficulty level must be between 1 and 5.");
        }

        DifficultyLevel = level;
    }
}
