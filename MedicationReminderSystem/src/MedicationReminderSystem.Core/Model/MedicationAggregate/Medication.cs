// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MedicationReminderSystem.Core;

/// <summary>
/// Represents a medication in the system.
/// </summary>
public class Medication
{
    /// <summary>
    /// Gets or sets the unique identifier for the medication.
    /// </summary>
    public Guid MedicationId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this medication.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the name of the medication.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the medication type.
    /// </summary>
    public MedicationType MedicationType { get; set; }

    /// <summary>
    /// Gets or sets the dosage amount.
    /// </summary>
    public string Dosage { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the prescribing doctor.
    /// </summary>
    public string? PrescribingDoctor { get; set; }

    /// <summary>
    /// Gets or sets the prescription date.
    /// </summary>
    public DateTime? PrescriptionDate { get; set; }

    /// <summary>
    /// Gets or sets the purpose or condition being treated.
    /// </summary>
    public string? Purpose { get; set; }

    /// <summary>
    /// Gets or sets special instructions.
    /// </summary>
    public string? Instructions { get; set; }

    /// <summary>
    /// Gets or sets potential side effects.
    /// </summary>
    public string? SideEffects { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the medication is currently active.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the collection of dose schedules for this medication.
    /// </summary>
    public ICollection<DoseSchedule> DoseSchedules { get; set; } = new List<DoseSchedule>();

    /// <summary>
    /// Gets or sets the collection of refills for this medication.
    /// </summary>
    public ICollection<Refill> Refills { get; set; } = new List<Refill>();

    /// <summary>
    /// Checks if the medication requires refrigeration (based on type).
    /// </summary>
    /// <returns>True if likely requires refrigeration; otherwise, false.</returns>
    public bool RequiresRefrigeration()
    {
        return MedicationType == MedicationType.Injection || MedicationType == MedicationType.Liquid;
    }

    /// <summary>
    /// Toggles the active status of the medication.
    /// </summary>
    public void ToggleActive()
    {
        IsActive = !IsActive;
    }
}
