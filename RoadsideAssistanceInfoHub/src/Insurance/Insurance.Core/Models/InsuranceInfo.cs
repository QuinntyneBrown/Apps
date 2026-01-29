namespace Insurance.Core.Models;

public class InsuranceInfo
{
    public Guid InsuranceInfoId { get; set; }
    public Guid TenantId { get; set; }
    public Guid VehicleId { get; set; }
    public string InsuranceCompany { get; set; } = string.Empty;
    public string PolicyNumber { get; set; } = string.Empty;
    public string PolicyHolder { get; set; } = string.Empty;
    public DateTime PolicyStartDate { get; set; }
    public DateTime PolicyEndDate { get; set; }
    public string? AgentName { get; set; }
    public string? AgentPhone { get; set; }
    public string? CompanyPhone { get; set; }
    public string? ClaimsPhone { get; set; }
    public string? CoverageType { get; set; }
    public decimal? Deductible { get; set; }
    public decimal? Premium { get; set; }
    public bool IncludesRoadsideAssistance { get; set; }
    public string? Notes { get; set; }
}
