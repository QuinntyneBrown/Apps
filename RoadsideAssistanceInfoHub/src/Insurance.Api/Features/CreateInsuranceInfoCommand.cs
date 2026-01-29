using Insurance.Core;
using Insurance.Core.Models;
using MediatR;

namespace Insurance.Api.Features;

public record CreateInsuranceInfoCommand(Guid TenantId, Guid VehicleId, string InsuranceCompany, string PolicyNumber, string PolicyHolder, DateTime PolicyStartDate, DateTime PolicyEndDate, string? AgentName, string? AgentPhone, string? CompanyPhone, string? ClaimsPhone, string? CoverageType, decimal? Deductible, decimal? Premium, bool IncludesRoadsideAssistance, string? Notes) : IRequest<InsuranceInfoDto>;

public class CreateInsuranceInfoCommandHandler : IRequestHandler<CreateInsuranceInfoCommand, InsuranceInfoDto>
{
    private readonly IInsuranceDbContext _context;
    public CreateInsuranceInfoCommandHandler(IInsuranceDbContext context) => _context = context;
    public async Task<InsuranceInfoDto> Handle(CreateInsuranceInfoCommand request, CancellationToken ct)
    {
        var insuranceInfo = new InsuranceInfo { InsuranceInfoId = Guid.NewGuid(), TenantId = request.TenantId, VehicleId = request.VehicleId, InsuranceCompany = request.InsuranceCompany, PolicyNumber = request.PolicyNumber, PolicyHolder = request.PolicyHolder, PolicyStartDate = request.PolicyStartDate, PolicyEndDate = request.PolicyEndDate, AgentName = request.AgentName, AgentPhone = request.AgentPhone, CompanyPhone = request.CompanyPhone, ClaimsPhone = request.ClaimsPhone, CoverageType = request.CoverageType, Deductible = request.Deductible, Premium = request.Premium, IncludesRoadsideAssistance = request.IncludesRoadsideAssistance, Notes = request.Notes };
        _context.InsuranceInfos.Add(insuranceInfo);
        await _context.SaveChangesAsync(ct);
        return insuranceInfo.ToDto();
    }
}
