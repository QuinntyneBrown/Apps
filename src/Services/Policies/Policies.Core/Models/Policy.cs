namespace Policies.Core.Models;

public class Policy
{
    public Guid PolicyId { get; set; }
    public Guid TenantId { get; set; }
    public Guid VehicleId { get; set; }
    public string Provider { get; set; } = string.Empty;
    public string PolicyNumber { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string EmergencyPhone { get; set; } = string.Empty;
    public int? MaxTowingDistance { get; set; }
    public int? ServiceCallsPerYear { get; set; }
    public decimal? AnnualPremium { get; set; }
    public bool CoversBatteryService { get; set; }
    public bool CoversFlatTire { get; set; }
    public bool CoversFuelDelivery { get; set; }
    public bool CoversLockout { get; set; }
    public string? Notes { get; set; }
}
