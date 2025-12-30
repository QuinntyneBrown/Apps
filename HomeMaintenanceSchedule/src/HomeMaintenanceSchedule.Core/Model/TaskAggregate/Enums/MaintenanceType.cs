// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HomeMaintenanceSchedule.Core;

/// <summary>
/// Represents the type of maintenance.
/// </summary>
public enum MaintenanceType
{
    /// <summary>
    /// Routine preventive maintenance.
    /// </summary>
    Preventive = 0,

    /// <summary>
    /// Corrective or repair maintenance.
    /// </summary>
    Corrective = 1,

    /// <summary>
    /// Seasonal maintenance.
    /// </summary>
    Seasonal = 2,

    /// <summary>
    /// Emergency maintenance.
    /// </summary>
    Emergency = 3,

    /// <summary>
    /// Inspection or assessment.
    /// </summary>
    Inspection = 4,
}
