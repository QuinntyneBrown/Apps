// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace VehicleValueTracker.Core;

/// <summary>
/// Represents the overall condition grade of a vehicle.
/// </summary>
public enum ConditionGrade
{
    /// <summary>
    /// Excellent condition - like new, no visible wear.
    /// </summary>
    Excellent = 0,

    /// <summary>
    /// Very good condition - minor wear, well maintained.
    /// </summary>
    VeryGood = 1,

    /// <summary>
    /// Good condition - some wear, regularly maintained.
    /// </summary>
    Good = 2,

    /// <summary>
    /// Fair condition - visible wear, may need repairs.
    /// </summary>
    Fair = 3,

    /// <summary>
    /// Poor condition - significant wear, needs repairs.
    /// </summary>
    Poor = 4,
}
