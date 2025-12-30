// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace RoadsideAssistanceInfoHub.Core;

/// <summary>
/// Event raised when insurance information is added.
/// </summary>
public record InsuranceInfoAddedEvent
{
    /// <summary>
    /// Gets the insurance info ID.
    /// </summary>
    public Guid InsuranceInfoId { get; init; }

    /// <summary>
    /// Gets the vehicle ID.
    /// </summary>
    public Guid VehicleId { get; init; }

    /// <summary>
    /// Gets the insurance company.
    /// </summary>
    public string InsuranceCompany { get; init; } = string.Empty;

    /// <summary>
    /// Gets the policy number.
    /// </summary>
    public string PolicyNumber { get; init; } = string.Empty;

    /// <summary>
    /// Gets the timestamp when the event occurred.
    /// </summary>
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}
