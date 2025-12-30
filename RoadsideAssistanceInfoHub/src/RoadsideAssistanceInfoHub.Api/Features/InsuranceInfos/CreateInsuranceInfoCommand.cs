using RoadsideAssistanceInfoHub.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace RoadsideAssistanceInfoHub.Api.Features.InsuranceInfos;

public record CreateInsuranceInfoCommand : IRequest<InsuranceInfoDto>
{
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

public class CreateInsuranceInfoCommandHandler : IRequestHandler<CreateInsuranceInfoCommand, InsuranceInfoDto>
{
    private readonly IRoadsideAssistanceInfoHubContext _context;
    private readonly ILogger<CreateInsuranceInfoCommandHandler> _logger;

    public CreateInsuranceInfoCommandHandler(
        IRoadsideAssistanceInfoHubContext context,
        ILogger<CreateInsuranceInfoCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<InsuranceInfoDto> Handle(CreateInsuranceInfoCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating insurance info for vehicle {VehicleId}",
            request.VehicleId);

        var insuranceInfo = new InsuranceInfo
        {
            InsuranceInfoId = Guid.NewGuid(),
            VehicleId = request.VehicleId,
            InsuranceCompany = request.InsuranceCompany,
            PolicyNumber = request.PolicyNumber,
            PolicyHolder = request.PolicyHolder,
            PolicyStartDate = request.PolicyStartDate,
            PolicyEndDate = request.PolicyEndDate,
            AgentName = request.AgentName,
            AgentPhone = request.AgentPhone,
            CompanyPhone = request.CompanyPhone,
            ClaimsPhone = request.ClaimsPhone,
            CoverageType = request.CoverageType,
            Deductible = request.Deductible,
            Premium = request.Premium,
            IncludesRoadsideAssistance = request.IncludesRoadsideAssistance,
            Notes = request.Notes,
        };

        _context.InsuranceInfos.Add(insuranceInfo);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created insurance info {InsuranceInfoId}",
            insuranceInfo.InsuranceInfoId);

        return insuranceInfo.ToDto();
    }
}
