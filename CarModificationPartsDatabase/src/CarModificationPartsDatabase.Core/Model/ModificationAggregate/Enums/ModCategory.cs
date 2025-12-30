// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace CarModificationPartsDatabase.Core;

/// <summary>
/// Represents the category of a modification.
/// </summary>
public enum ModCategory
{
    /// <summary>
    /// Engine performance modifications.
    /// </summary>
    Engine = 0,

    /// <summary>
    /// Exhaust system modifications.
    /// </summary>
    Exhaust = 1,

    /// <summary>
    /// Suspension modifications.
    /// </summary>
    Suspension = 2,

    /// <summary>
    /// Brake system modifications.
    /// </summary>
    Brakes = 3,

    /// <summary>
    /// Wheel and tire modifications.
    /// </summary>
    WheelsAndTires = 4,

    /// <summary>
    /// Exterior body modifications.
    /// </summary>
    Exterior = 5,

    /// <summary>
    /// Interior modifications.
    /// </summary>
    Interior = 6,

    /// <summary>
    /// Audio and electronics modifications.
    /// </summary>
    AudioAndElectronics = 7,

    /// <summary>
    /// Lighting modifications.
    /// </summary>
    Lighting = 8,

    /// <summary>
    /// Aerodynamic modifications.
    /// </summary>
    Aerodynamics = 9,

    /// <summary>
    /// Forced induction (turbo/supercharger).
    /// </summary>
    ForcedInduction = 10,

    /// <summary>
    /// Other modification category.
    /// </summary>
    Other = 11,
}
