// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BloodPressureMonitor.Core;

/// <summary>
/// Represents a blood pressure reading.
/// </summary>
public class Reading
{
    /// <summary>
    /// Gets or sets the unique identifier for the reading.
    /// </summary>
    public Guid ReadingId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this reading.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the tenant ID for multi-tenant isolation.
    /// </summary>
    public Guid TenantId { get; set; }

    /// <summary>
    /// Gets or sets the systolic pressure (top number).
    /// </summary>
    public int Systolic { get; set; }

    /// <summary>
    /// Gets or sets the diastolic pressure (bottom number).
    /// </summary>
    public int Diastolic { get; set; }

    /// <summary>
    /// Gets or sets the pulse/heart rate.
    /// </summary>
    public int? Pulse { get; set; }

    /// <summary>
    /// Gets or sets the blood pressure category.
    /// </summary>
    public BloodPressureCategory Category { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the reading was taken.
    /// </summary>
    public DateTime MeasuredAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the position when measured (e.g., Sitting, Standing, Lying Down).
    /// </summary>
    public string? Position { get; set; }

    /// <summary>
    /// Gets or sets the arm used (Left or Right).
    /// </summary>
    public string? Arm { get; set; }

    /// <summary>
    /// Gets or sets optional notes about the reading.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Determines the blood pressure category based on systolic and diastolic values.
    /// </summary>
    /// <returns>The blood pressure category.</returns>
    public BloodPressureCategory DetermineCategory()
    {
        if (Systolic > 180 || Diastolic > 120)
            return BloodPressureCategory.HypertensiveCrisis;

        if (Systolic >= 140 || Diastolic >= 90)
            return BloodPressureCategory.HypertensionStage2;

        if (Systolic >= 130 || Diastolic >= 80)
            return BloodPressureCategory.HypertensionStage1;

        if (Systolic >= 120 && Diastolic < 80)
            return BloodPressureCategory.Elevated;

        return BloodPressureCategory.Normal;
    }

    /// <summary>
    /// Checks if the reading indicates a critical condition.
    /// </summary>
    /// <returns>True if critical; otherwise, false.</returns>
    public bool IsCritical()
    {
        return Category == BloodPressureCategory.HypertensiveCrisis;
    }
}
