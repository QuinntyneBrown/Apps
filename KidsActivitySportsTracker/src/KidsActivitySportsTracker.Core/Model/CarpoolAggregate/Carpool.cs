// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace KidsActivitySportsTracker.Core;

/// <summary>
/// Represents a carpool arrangement for an activity.
/// </summary>
public class Carpool
{
    /// <summary>
    /// Gets or sets the unique identifier for the carpool.
    /// </summary>
    public Guid CarpoolId { get; set; }

    /// <summary>
    /// Gets or sets the user ID who owns this carpool.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the name of the carpool.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the driver's name.
    /// </summary>
    public string? DriverName { get; set; }

    /// <summary>
    /// Gets or sets the driver's contact information.
    /// </summary>
    public string? DriverContact { get; set; }

    /// <summary>
    /// Gets or sets the pickup time.
    /// </summary>
    public DateTime? PickupTime { get; set; }

    /// <summary>
    /// Gets or sets the pickup location.
    /// </summary>
    public string? PickupLocation { get; set; }

    /// <summary>
    /// Gets or sets the dropoff time.
    /// </summary>
    public DateTime? DropoffTime { get; set; }

    /// <summary>
    /// Gets or sets the dropoff location.
    /// </summary>
    public string? DropoffLocation { get; set; }

    /// <summary>
    /// Gets or sets the participants.
    /// </summary>
    public string? Participants { get; set; }

    /// <summary>
    /// Gets or sets notes about the carpool.
    /// </summary>
    public string? Notes { get; set; }

    /// <summary>
    /// Gets or sets the creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
