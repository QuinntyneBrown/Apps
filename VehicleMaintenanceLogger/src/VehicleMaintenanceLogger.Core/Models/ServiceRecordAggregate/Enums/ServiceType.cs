// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace VehicleMaintenanceLogger.Core;

/// <summary>
/// Represents the type of service performed.
/// </summary>
public enum ServiceType
{
    /// <summary>
    /// Oil change service.
    /// </summary>
    OilChange = 0,

    /// <summary>
    /// Tire rotation or replacement.
    /// </summary>
    TireService = 1,

    /// <summary>
    /// Brake inspection or repair.
    /// </summary>
    BrakeService = 2,

    /// <summary>
    /// Engine repair or maintenance.
    /// </summary>
    EngineService = 3,

    /// <summary>
    /// Transmission service.
    /// </summary>
    TransmissionService = 4,

    /// <summary>
    /// Battery replacement.
    /// </summary>
    BatteryReplacement = 5,

    /// <summary>
    /// Air filter replacement.
    /// </summary>
    AirFilterReplacement = 6,

    /// <summary>
    /// Inspection or emissions test.
    /// </summary>
    Inspection = 7,

    /// <summary>
    /// Alignment service.
    /// </summary>
    Alignment = 8,

    /// <summary>
    /// Coolant flush.
    /// </summary>
    CoolantFlush = 9,

    /// <summary>
    /// Diagnostic service.
    /// </summary>
    Diagnostic = 10,

    /// <summary>
    /// Other service type.
    /// </summary>
    Other = 11,
}
