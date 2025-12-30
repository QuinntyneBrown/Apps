// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CarModificationPartsDatabase.Core;

namespace CarModificationPartsDatabase.Api;

/// <summary>
/// Data transfer object for Installation.
/// </summary>
public record InstallationDto
{
    /// <summary>
    /// Gets or sets the installation ID.
    /// </summary>
    public Guid InstallationId { get; init; }

    /// <summary>
    /// Gets or sets the modification ID.
    /// </summary>
    public Guid ModificationId { get; init; }

    /// <summary>
    /// Gets or sets the vehicle information.
    /// </summary>
    public string VehicleInfo { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the installation date.
    /// </summary>
    public DateTime InstallationDate { get; init; }

    /// <summary>
    /// Gets or sets the installer name (shop or individual).
    /// </summary>
    public string? InstalledBy { get; init; }

    /// <summary>
    /// Gets or sets the actual installation cost.
    /// </summary>
    public decimal? InstallationCost { get; init; }

    /// <summary>
    /// Gets or sets the parts cost.
    /// </summary>
    public decimal? PartsCost { get; init; }

    /// <summary>
    /// Gets or sets the labor hours.
    /// </summary>
    public decimal? LaborHours { get; init; }

    /// <summary>
    /// Gets or sets the list of parts used in the installation.
    /// </summary>
    public List<string> PartsUsed { get; init; } = new List<string>();

    /// <summary>
    /// Gets or sets the installation notes or observations.
    /// </summary>
    public string? Notes { get; init; }

    /// <summary>
    /// Gets or sets the difficulty rating (1-5).
    /// </summary>
    public int? DifficultyRating { get; init; }

    /// <summary>
    /// Gets or sets the satisfaction rating (1-5).
    /// </summary>
    public int? SatisfactionRating { get; init; }

    /// <summary>
    /// Gets or sets the list of photos URLs.
    /// </summary>
    public List<string> Photos { get; init; } = new List<string>();

    /// <summary>
    /// Gets or sets whether this installation is completed.
    /// </summary>
    public bool IsCompleted { get; init; }

    /// <summary>
    /// Gets or sets the total cost.
    /// </summary>
    public decimal TotalCost { get; init; }
}

/// <summary>
/// Extension methods for Installation.
/// </summary>
public static class InstallationExtensions
{
    /// <summary>
    /// Converts an Installation to a DTO.
    /// </summary>
    /// <param name="installation">The installation.</param>
    /// <returns>The DTO.</returns>
    public static InstallationDto ToDto(this Installation installation)
    {
        return new InstallationDto
        {
            InstallationId = installation.InstallationId,
            ModificationId = installation.ModificationId,
            VehicleInfo = installation.VehicleInfo,
            InstallationDate = installation.InstallationDate,
            InstalledBy = installation.InstalledBy,
            InstallationCost = installation.InstallationCost,
            PartsCost = installation.PartsCost,
            LaborHours = installation.LaborHours,
            PartsUsed = installation.PartsUsed,
            Notes = installation.Notes,
            DifficultyRating = installation.DifficultyRating,
            SatisfactionRating = installation.SatisfactionRating,
            Photos = installation.Photos,
            IsCompleted = installation.IsCompleted,
            TotalCost = installation.GetTotalCost(),
        };
    }
}
