using RoadsideAssistanceInfoHub.Core;

namespace RoadsideAssistanceInfoHub.Api.Features.Policies;

public record PolicyDto
{
    public Guid PolicyId { get; init; }
    public Guid VehicleId { get; init; }
    public string Provider { get; init; } = string.Empty;
    public string PolicyNumber { get; init; } = string.Empty;
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public string EmergencyPhone { get; init; } = string.Empty;
    public int? MaxTowingDistance { get; init; }
    public int? ServiceCallsPerYear { get; init; }
    public List<string> CoveredServices { get; init; } = new List<string>();
    public decimal? AnnualPremium { get; init; }
    public bool CoversBatteryService { get; init; }
    public bool CoversFlatTire { get; init; }
    public bool CoversFuelDelivery { get; init; }
    public bool CoversLockout { get; init; }
    public string? Notes { get; init; }
}

public static class PolicyExtensions
{
    public static PolicyDto ToDto(this Policy policy)
    {
        return new PolicyDto
        {
            PolicyId = policy.PolicyId,
            VehicleId = policy.VehicleId,
            Provider = policy.Provider,
            PolicyNumber = policy.PolicyNumber,
            StartDate = policy.StartDate,
            EndDate = policy.EndDate,
            EmergencyPhone = policy.EmergencyPhone,
            MaxTowingDistance = policy.MaxTowingDistance,
            ServiceCallsPerYear = policy.ServiceCallsPerYear,
            CoveredServices = policy.CoveredServices,
            AnnualPremium = policy.AnnualPremium,
            CoversBatteryService = policy.CoversBatteryService,
            CoversFlatTire = policy.CoversFlatTire,
            CoversFuelDelivery = policy.CoversFuelDelivery,
            CoversLockout = policy.CoversLockout,
            Notes = policy.Notes,
        };
    }
}
