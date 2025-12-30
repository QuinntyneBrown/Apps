using RoadsideAssistanceInfoHub.Core;

namespace RoadsideAssistanceInfoHub.Api.Features.InsuranceInfos;

public record InsuranceInfoDto
{
    public Guid InsuranceInfoId { get; init; }
    public Guid VehicleId { get; init; }
    public string InsuranceCompany { get; init; } = string.Empty;
    public string PolicyNumber { get; init; } = string.Empty;
    public string PolicyHolder { get; init; } = string.Empty;
    public DateTime PolicyStartDate { get; init; }
    public DateTime PolicyEndDate { get; init; }
    public string? AgentName { get; init; }
    public string? AgentPhone { get; init; }
    public string? CompanyPhone { get; init; }
    public string? ClaimsPhone { get; init; }
    public string? CoverageType { get; init; }
    public decimal? Deductible { get; init; }
    public decimal? Premium { get; init; }
    public bool IncludesRoadsideAssistance { get; init; }
    public string? Notes { get; init; }
}

public static class InsuranceInfoExtensions
{
    public static InsuranceInfoDto ToDto(this InsuranceInfo insuranceInfo)
    {
        return new InsuranceInfoDto
        {
            InsuranceInfoId = insuranceInfo.InsuranceInfoId,
            VehicleId = insuranceInfo.VehicleId,
            InsuranceCompany = insuranceInfo.InsuranceCompany,
            PolicyNumber = insuranceInfo.PolicyNumber,
            PolicyHolder = insuranceInfo.PolicyHolder,
            PolicyStartDate = insuranceInfo.PolicyStartDate,
            PolicyEndDate = insuranceInfo.PolicyEndDate,
            AgentName = insuranceInfo.AgentName,
            AgentPhone = insuranceInfo.AgentPhone,
            CompanyPhone = insuranceInfo.CompanyPhone,
            ClaimsPhone = insuranceInfo.ClaimsPhone,
            CoverageType = insuranceInfo.CoverageType,
            Deductible = insuranceInfo.Deductible,
            Premium = insuranceInfo.Premium,
            IncludesRoadsideAssistance = insuranceInfo.IncludesRoadsideAssistance,
            Notes = insuranceInfo.Notes,
        };
    }
}
