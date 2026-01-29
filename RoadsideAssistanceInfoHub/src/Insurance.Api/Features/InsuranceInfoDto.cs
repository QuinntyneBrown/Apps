using Insurance.Core.Models;

namespace Insurance.Api.Features;

public record InsuranceInfoDto(Guid InsuranceInfoId, Guid TenantId, Guid VehicleId, string InsuranceCompany, string PolicyNumber, string PolicyHolder, DateTime PolicyStartDate, DateTime PolicyEndDate, string? AgentName, string? AgentPhone, string? CompanyPhone, string? ClaimsPhone, string? CoverageType, decimal? Deductible, decimal? Premium, bool IncludesRoadsideAssistance, string? Notes);

public static class InsuranceInfoExtensions
{
    public static InsuranceInfoDto ToDto(this InsuranceInfo i) => new(i.InsuranceInfoId, i.TenantId, i.VehicleId, i.InsuranceCompany, i.PolicyNumber, i.PolicyHolder, i.PolicyStartDate, i.PolicyEndDate, i.AgentName, i.AgentPhone, i.CompanyPhone, i.ClaimsPhone, i.CoverageType, i.Deductible, i.Premium, i.IncludesRoadsideAssistance, i.Notes);
}
