// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace KidsActivitySportsTracker.Api.Features.Carpool;

/// <summary>
/// Data transfer object for Carpool.
/// </summary>
public record CarpoolDto
{
    public Guid CarpoolId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? DriverName { get; init; }
    public string? DriverContact { get; init; }
    public DateTime? PickupTime { get; init; }
    public string? PickupLocation { get; init; }
    public DateTime? DropoffTime { get; init; }
    public string? DropoffLocation { get; init; }
    public string? Participants { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
}

/// <summary>
/// Extension methods for Carpool entity.
/// </summary>
public static class CarpoolExtensions
{
    /// <summary>
    /// Converts a Carpool entity to CarpoolDto.
    /// </summary>
    public static CarpoolDto ToDto(this Core.Carpool carpool)
    {
        return new CarpoolDto
        {
            CarpoolId = carpool.CarpoolId,
            UserId = carpool.UserId,
            Name = carpool.Name,
            DriverName = carpool.DriverName,
            DriverContact = carpool.DriverContact,
            PickupTime = carpool.PickupTime,
            PickupLocation = carpool.PickupLocation,
            DropoffTime = carpool.DropoffTime,
            DropoffLocation = carpool.DropoffLocation,
            Participants = carpool.Participants,
            Notes = carpool.Notes,
            CreatedAt = carpool.CreatedAt
        };
    }
}
