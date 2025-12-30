using RoadsideAssistanceInfoHub.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace RoadsideAssistanceInfoHub.Api.Features.Policies;

public record CreatePolicyCommand : IRequest<PolicyDto>
{
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

public class CreatePolicyCommandHandler : IRequestHandler<CreatePolicyCommand, PolicyDto>
{
    private readonly IRoadsideAssistanceInfoHubContext _context;
    private readonly ILogger<CreatePolicyCommandHandler> _logger;

    public CreatePolicyCommandHandler(
        IRoadsideAssistanceInfoHubContext context,
        ILogger<CreatePolicyCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PolicyDto> Handle(CreatePolicyCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating policy for vehicle {VehicleId}, provider: {Provider}",
            request.VehicleId,
            request.Provider);

        var policy = new Policy
        {
            PolicyId = Guid.NewGuid(),
            VehicleId = request.VehicleId,
            Provider = request.Provider,
            PolicyNumber = request.PolicyNumber,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            EmergencyPhone = request.EmergencyPhone,
            MaxTowingDistance = request.MaxTowingDistance,
            ServiceCallsPerYear = request.ServiceCallsPerYear,
            CoveredServices = request.CoveredServices,
            AnnualPremium = request.AnnualPremium,
            CoversBatteryService = request.CoversBatteryService,
            CoversFlatTire = request.CoversFlatTire,
            CoversFuelDelivery = request.CoversFuelDelivery,
            CoversLockout = request.CoversLockout,
            Notes = request.Notes,
        };

        _context.Policies.Add(policy);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created policy {PolicyId}",
            policy.PolicyId);

        return policy.ToDto();
    }
}
