using RoadsideAssistanceInfoHub.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RoadsideAssistanceInfoHub.Api.Features.InsuranceInfos;

public record UpdateInsuranceInfoCommand : IRequest<InsuranceInfoDto?>
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

public class UpdateInsuranceInfoCommandHandler : IRequestHandler<UpdateInsuranceInfoCommand, InsuranceInfoDto?>
{
    private readonly IRoadsideAssistanceInfoHubContext _context;
    private readonly ILogger<UpdateInsuranceInfoCommandHandler> _logger;

    public UpdateInsuranceInfoCommandHandler(
        IRoadsideAssistanceInfoHubContext context,
        ILogger<UpdateInsuranceInfoCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<InsuranceInfoDto?> Handle(UpdateInsuranceInfoCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating insurance info {InsuranceInfoId}", request.InsuranceInfoId);

        var insuranceInfo = await _context.InsuranceInfos
            .FirstOrDefaultAsync(i => i.InsuranceInfoId == request.InsuranceInfoId, cancellationToken);

        if (insuranceInfo == null)
        {
            _logger.LogWarning("Insurance info {InsuranceInfoId} not found", request.InsuranceInfoId);
            return null;
        }

        insuranceInfo.VehicleId = request.VehicleId;
        insuranceInfo.InsuranceCompany = request.InsuranceCompany;
        insuranceInfo.PolicyNumber = request.PolicyNumber;
        insuranceInfo.PolicyHolder = request.PolicyHolder;
        insuranceInfo.PolicyStartDate = request.PolicyStartDate;
        insuranceInfo.PolicyEndDate = request.PolicyEndDate;
        insuranceInfo.AgentName = request.AgentName;
        insuranceInfo.AgentPhone = request.AgentPhone;
        insuranceInfo.CompanyPhone = request.CompanyPhone;
        insuranceInfo.ClaimsPhone = request.ClaimsPhone;
        insuranceInfo.CoverageType = request.CoverageType;
        insuranceInfo.Deductible = request.Deductible;
        insuranceInfo.Premium = request.Premium;
        insuranceInfo.IncludesRoadsideAssistance = request.IncludesRoadsideAssistance;
        insuranceInfo.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated insurance info {InsuranceInfoId}", request.InsuranceInfoId);

        return insuranceInfo.ToDto();
    }
}
