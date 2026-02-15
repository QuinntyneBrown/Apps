using Carpools.Core.Models;

namespace Carpools.Api.Features;

public record CarpoolDto(
    Guid CarpoolId,
    Guid UserId,
    Guid ScheduleId,
    string DriverName,
    string? DriverPhone,
    int AvailableSeats,
    string? PickupLocation,
    TimeSpan? PickupTime,
    string? Notes,
    bool IsConfirmed,
    DateTime CreatedAt);

public static class CarpoolExtensions
{
    public static CarpoolDto ToDto(this Carpool carpool)
    {
        return new CarpoolDto(
            carpool.CarpoolId,
            carpool.UserId,
            carpool.ScheduleId,
            carpool.DriverName,
            carpool.DriverPhone,
            carpool.AvailableSeats,
            carpool.PickupLocation,
            carpool.PickupTime,
            carpool.Notes,
            carpool.IsConfirmed,
            carpool.CreatedAt);
    }
}
