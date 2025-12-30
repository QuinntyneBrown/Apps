// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace VehicleMaintenanceLogger.Core;

/// <summary>
/// Represents the type of vehicle.
/// </summary>
public enum VehicleType
{
    /// <summary>
    /// Sedan car.
    /// </summary>
    Sedan = 0,

    /// <summary>
    /// SUV (Sport Utility Vehicle).
    /// </summary>
    SUV = 1,

    /// <summary>
    /// Truck.
    /// </summary>
    Truck = 2,

    /// <summary>
    /// Motorcycle.
    /// </summary>
    Motorcycle = 3,

    /// <summary>
    /// Van.
    /// </summary>
    Van = 4,

    /// <summary>
    /// Coupe.
    /// </summary>
    Coupe = 5,

    /// <summary>
    /// Convertible.
    /// </summary>
    Convertible = 6,

    /// <summary>
    /// Hatchback.
    /// </summary>
    Hatchback = 7,

    /// <summary>
    /// Other vehicle type.
    /// </summary>
    Other = 8,
}
