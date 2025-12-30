// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace PersonalHealthDashboard.Core;

/// <summary>
/// Represents the type of vital sign measurement.
/// </summary>
public enum VitalType
{
    /// <summary>
    /// Blood pressure measurement.
    /// </summary>
    BloodPressure = 0,

    /// <summary>
    /// Heart rate measurement.
    /// </summary>
    HeartRate = 1,

    /// <summary>
    /// Body temperature measurement.
    /// </summary>
    Temperature = 2,

    /// <summary>
    /// Blood glucose measurement.
    /// </summary>
    BloodGlucose = 3,

    /// <summary>
    /// Oxygen saturation measurement.
    /// </summary>
    OxygenSaturation = 4,

    /// <summary>
    /// Weight measurement.
    /// </summary>
    Weight = 5,

    /// <summary>
    /// Respiratory rate measurement.
    /// </summary>
    RespiratoryRate = 6,
}
