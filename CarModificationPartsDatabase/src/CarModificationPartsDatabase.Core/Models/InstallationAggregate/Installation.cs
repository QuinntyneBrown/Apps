// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CarModificationPartsDatabase.Core;

/// <summary>
/// Represents an installation of a modification on a vehicle.
/// </summary>
public class Installation
{
    /// <summary>
    /// Gets or sets the unique identifier for the installation.
    /// </summary>
    public Guid InstallationId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the reference to the modification.
    /// </summary>
    public Guid ModificationId { get; set; }

    /// <summary>
    /// Gets or sets the vehicle information.
    /// </summary>
    public string VehicleInfo { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the installation date.
    /// </summary>
    public DateTime InstallationDate { get; set; }

    /// <summary>
    /// Gets or sets the installer name (shop or individual).
    /// </summary>
    public string? InstalledBy { get; set; }

    /// <summary>
    /// Gets or sets the actual installation cost.
    /// </summary>
    public decimal? InstallationCost { get; set; }

    /// <summary>
    /// Gets or sets the parts cost.
    /// </summary>
    public decimal? PartsCost { get; set; }

    /// <summary>
    /// Gets or sets the labor hours.
    /// </summary>
    public decimal? LaborHours { get; set; }

    /// <summary>
    /// Gets or sets the list of parts used in the installation.
    /// </summary>
    public List<string> PartsUsed { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets the installation notes or observations.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the difficulty rating (1-5).
    /// </summary>
    public int? DifficultyRating { get; set; }

    /// <summary>
    /// Gets or sets the satisfaction rating (1-5).
    /// </summary>
    public int? SatisfactionRating { get; set; }

    /// <summary>
    /// Gets or sets the list of photos URLs.
    /// </summary>
    public List<string> Photos { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets whether this installation is completed.
    /// </summary>
    public bool IsCompleted { get; set; } = false;

    /// <summary>
    /// Gets or sets the navigation property to the modification.
    /// </summary>
    public Modification? Modification { get; set; }

    /// <summary>
    /// Marks the installation as completed.
    /// </summary>
    /// <param name="completionDate">The completion date.</param>
    public void MarkAsCompleted(DateTime completionDate)
    {
        InstallationDate = completionDate;
        IsCompleted = true;
    }

    /// <summary>
    /// Adds parts to the installation.
    /// </summary>
    /// <param name="parts">The parts to add.</param>
    public void AddParts(IEnumerable<string> parts)
    {
        PartsUsed.AddRange(parts);
    }

    /// <summary>
    /// Sets the satisfaction rating.
    /// </summary>
    /// <param name="rating">The satisfaction rating (1-5).</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when rating is not between 1 and 5.</exception>
    public void SetSatisfactionRating(int rating)
    {
        if (rating < 1 || rating > 5)
        {
            throw new ArgumentOutOfRangeException(nameof(rating), "Rating must be between 1 and 5.");
        }

        SatisfactionRating = rating;
    }

    /// <summary>
    /// Adds photos to the installation.
    /// </summary>
    /// <param name="photoUrls">The photo URLs to add.</param>
    public void AddPhotos(IEnumerable<string> photoUrls)
    {
        Photos.AddRange(photoUrls);
    }

    /// <summary>
    /// Calculates the total cost of the installation.
    /// </summary>
    /// <returns>The total cost.</returns>
    public decimal GetTotalCost()
    {
        return (InstallationCost ?? 0) + (PartsCost ?? 0);
    }
}
