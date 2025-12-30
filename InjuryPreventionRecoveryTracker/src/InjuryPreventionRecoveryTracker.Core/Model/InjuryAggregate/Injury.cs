// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace InjuryPreventionRecoveryTracker.Core;

/// <summary>
/// Represents an injury being tracked.
/// </summary>
public class Injury
{
    /// <summary>
    /// Gets or sets the unique identifier for the injury.
    /// </summary>
    public Guid InjuryId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this injury record.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the injury type.
    /// </summary>
    public InjuryType InjuryType { get; set; }

    /// <summary>
    /// Gets or sets the injury severity.
    /// </summary>
    public InjurySeverity Severity { get; set; }

    /// <summary>
    /// Gets or sets the body part affected.
    /// </summary>
    public string BodyPart { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date when the injury occurred.
    /// </summary>
    public DateTime InjuryDate { get; set; }

    /// <summary>
    /// Gets or sets the description of how the injury occurred.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the diagnosis from medical professional.
    /// </summary>
    public string? Diagnosis { get; set; }

    /// <summary>
    /// Gets or sets the expected recovery time in days.
    /// </summary>
    public int? ExpectedRecoveryDays { get; set; }

    /// <summary>
    /// Gets or sets the current recovery status (e.g., Active, Recovering, Healed).
    /// </summary>
    public string Status { get; set; } = "Active";

    /// <summary>
    /// Gets or sets the recovery progress percentage (0-100).
    /// </summary>
    public int ProgressPercentage { get; set; } = 0;

    /// <summary>
    /// Gets or sets optional notes.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the collection of recovery exercises for this injury.
    /// </summary>
    public ICollection<RecoveryExercise> RecoveryExercises { get; set; } = new List<RecoveryExercise>();

    /// <summary>
    /// Gets or sets the collection of milestones for this injury.
    /// </summary>
    public ICollection<Milestone> Milestones { get; set; } = new List<Milestone>();

    /// <summary>
    /// Checks if the injury is fully healed.
    /// </summary>
    /// <returns>True if healed; otherwise, false.</returns>
    public bool IsHealed()
    {
        return Status.Equals("Healed", StringComparison.OrdinalIgnoreCase) || ProgressPercentage >= 100;
    }

    /// <summary>
    /// Updates the recovery progress.
    /// </summary>
    /// <param name="percentage">The new progress percentage.</param>
    public void UpdateProgress(int percentage)
    {
        ProgressPercentage = Math.Clamp(percentage, 0, 100);
        if (ProgressPercentage >= 100)
        {
            Status = "Healed";
        }
    }
}
