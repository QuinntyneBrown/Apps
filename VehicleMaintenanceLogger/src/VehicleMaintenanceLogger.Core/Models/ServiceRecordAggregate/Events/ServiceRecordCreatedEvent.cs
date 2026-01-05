// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace VehicleMaintenanceLogger.Core;

/// <summary>
/// Event raised when a new service record is created.
/// </summary>
public record ServiceRecordCreatedEvent
{
    /// <summary>
    /// Gets the service record ID.
    /// </summary>
    public Guid ServiceRecordId { get; init; }

    /// <summary>
    /// Gets the vehicle ID.
    /// </summary>
    public Guid VehicleId { get; init; }

    /// <summary>
    /// Gets the service type.
    /// </summary>
    public ServiceType ServiceType { get; init; }

    /// <summary>
    /// Gets the service date.
    /// </summary>
    public DateTime ServiceDate { get; init; }

    /// <summary>
    /// Gets the mileage at service.
    /// </summary>
    public decimal MileageAtService { get; init; }

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
