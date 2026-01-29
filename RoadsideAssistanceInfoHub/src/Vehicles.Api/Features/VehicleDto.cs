using Vehicles.Core.Models;

namespace Vehicles.Api.Features;

public record VehicleDto(Guid VehicleId, Guid TenantId, string Make, string Model, int Year, string? VIN, string? LicensePlate, string? Color, decimal? CurrentMileage, string? OwnerName, string? Notes, bool IsActive);

public static class VehicleExtensions
{
    public static VehicleDto ToDto(this Vehicle v) => new(v.VehicleId, v.TenantId, v.Make, v.Model, v.Year, v.VIN, v.LicensePlate, v.Color, v.CurrentMileage, v.OwnerName, v.Notes, v.IsActive);
}
