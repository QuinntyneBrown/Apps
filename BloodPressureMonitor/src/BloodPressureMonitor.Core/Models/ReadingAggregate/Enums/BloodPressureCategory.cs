// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace BloodPressureMonitor.Core;

/// <summary>
/// Represents the blood pressure category based on AHA guidelines.
/// </summary>
public enum BloodPressureCategory
{
    /// <summary>
    /// Normal blood pressure (less than 120/80).
    /// </summary>
    Normal = 0,

    /// <summary>
    /// Elevated blood pressure (120-129 systolic and less than 80 diastolic).
    /// </summary>
    Elevated = 1,

    /// <summary>
    /// Hypertension Stage 1 (130-139 systolic or 80-89 diastolic).
    /// </summary>
    HypertensionStage1 = 2,

    /// <summary>
    /// Hypertension Stage 2 (140 or higher systolic or 90 or higher diastolic).
    /// </summary>
    HypertensionStage2 = 3,

    /// <summary>
    /// Hypertensive crisis (higher than 180 systolic and/or higher than 120 diastolic).
    /// </summary>
    HypertensiveCrisis = 4,
}
