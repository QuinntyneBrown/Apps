// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace InjuryPreventionRecoveryTracker.Core;

/// <summary>
/// Represents the type of injury.
/// </summary>
public enum InjuryType
{
    /// <summary>
    /// Muscle strain or pull.
    /// </summary>
    Strain = 0,

    /// <summary>
    /// Ligament sprain.
    /// </summary>
    Sprain = 1,

    /// <summary>
    /// Bone fracture.
    /// </summary>
    Fracture = 2,

    /// <summary>
    /// Tendon injury.
    /// </summary>
    Tendonitis = 3,

    /// <summary>
    /// Cartilage damage.
    /// </summary>
    CartilageDamage = 4,

    /// <summary>
    /// Overuse injury.
    /// </summary>
    Overuse = 5,

    /// <summary>
    /// Other or custom injury type.
    /// </summary>
    Other = 6,
}
