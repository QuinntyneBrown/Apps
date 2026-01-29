using Policies.Core;
using Policies.Core.Models;
using MediatR;

namespace Policies.Api.Features;

public record CreatePolicyCommand(Guid TenantId, Guid VehicleId, string Provider, string PolicyNumber, DateTime StartDate, DateTime EndDate, string EmergencyPhone, int? MaxTowingDistance, int? ServiceCallsPerYear, decimal? AnnualPremium, bool CoversBatteryService, bool CoversFlatTire, bool CoversFuelDelivery, bool CoversLockout, string? Notes) : IRequest<PolicyDto>;

public class CreatePolicyCommandHandler : IRequestHandler<CreatePolicyCommand, PolicyDto>
{
    private readonly IPoliciesDbContext _context;
    public CreatePolicyCommandHandler(IPoliciesDbContext context) => _context = context;
    public async Task<PolicyDto> Handle(CreatePolicyCommand request, CancellationToken ct)
    {
        var policy = new Policy { PolicyId = Guid.NewGuid(), TenantId = request.TenantId, VehicleId = request.VehicleId, Provider = request.Provider, PolicyNumber = request.PolicyNumber, StartDate = request.StartDate, EndDate = request.EndDate, EmergencyPhone = request.EmergencyPhone, MaxTowingDistance = request.MaxTowingDistance, ServiceCallsPerYear = request.ServiceCallsPerYear, AnnualPremium = request.AnnualPremium, CoversBatteryService = request.CoversBatteryService, CoversFlatTire = request.CoversFlatTire, CoversFuelDelivery = request.CoversFuelDelivery, CoversLockout = request.CoversLockout, Notes = request.Notes };
        _context.Policies.Add(policy);
        await _context.SaveChangesAsync(ct);
        return policy.ToDto();
    }
}
