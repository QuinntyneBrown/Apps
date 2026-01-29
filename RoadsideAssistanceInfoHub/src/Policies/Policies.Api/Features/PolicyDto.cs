using Policies.Core.Models;

namespace Policies.Api.Features;

public record PolicyDto(Guid PolicyId, Guid TenantId, Guid VehicleId, string Provider, string PolicyNumber, DateTime StartDate, DateTime EndDate, string EmergencyPhone, int? MaxTowingDistance, int? ServiceCallsPerYear, decimal? AnnualPremium, bool CoversBatteryService, bool CoversFlatTire, bool CoversFuelDelivery, bool CoversLockout, string? Notes);

public static class PolicyExtensions
{
    public static PolicyDto ToDto(this Policy p) => new(p.PolicyId, p.TenantId, p.VehicleId, p.Provider, p.PolicyNumber, p.StartDate, p.EndDate, p.EmergencyPhone, p.MaxTowingDistance, p.ServiceCallsPerYear, p.AnnualPremium, p.CoversBatteryService, p.CoversFlatTire, p.CoversFuelDelivery, p.CoversLockout, p.Notes);
}
