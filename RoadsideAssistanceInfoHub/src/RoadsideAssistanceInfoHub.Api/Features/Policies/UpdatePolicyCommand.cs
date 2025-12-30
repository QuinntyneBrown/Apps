using RoadsideAssistanceInfoHub.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RoadsideAssistanceInfoHub.Api.Features.Policies;

public record UpdatePolicyCommand : IRequest<PolicyDto?>
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

public class UpdatePolicyCommandHandler : IRequestHandler<UpdatePolicyCommand, PolicyDto?>
{
    private readonly IRoadsideAssistanceInfoHubContext _context;
    private readonly ILogger<UpdatePolicyCommandHandler> _logger;

    public UpdatePolicyCommandHandler(
        IRoadsideAssistanceInfoHubContext context,
        ILogger<UpdatePolicyCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PolicyDto?> Handle(UpdatePolicyCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating policy {PolicyId}", request.PolicyId);

        var policy = await _context.Policies
            .FirstOrDefaultAsync(p => p.PolicyId == request.PolicyId, cancellationToken);

        if (policy == null)
        {
            _logger.LogWarning("Policy {PolicyId} not found", request.PolicyId);
            return null;
        }

        policy.VehicleId = request.VehicleId;
        policy.Provider = request.Provider;
        policy.PolicyNumber = request.PolicyNumber;
        policy.StartDate = request.StartDate;
        policy.EndDate = request.EndDate;
        policy.EmergencyPhone = request.EmergencyPhone;
        policy.MaxTowingDistance = request.MaxTowingDistance;
        policy.ServiceCallsPerYear = request.ServiceCallsPerYear;
        policy.CoveredServices = request.CoveredServices;
        policy.AnnualPremium = request.AnnualPremium;
        policy.CoversBatteryService = request.CoversBatteryService;
        policy.CoversFlatTire = request.CoversFlatTire;
        policy.CoversFuelDelivery = request.CoversFuelDelivery;
        policy.CoversLockout = request.CoversLockout;
        policy.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated policy {PolicyId}", request.PolicyId);

        return policy.ToDto();
    }
}
