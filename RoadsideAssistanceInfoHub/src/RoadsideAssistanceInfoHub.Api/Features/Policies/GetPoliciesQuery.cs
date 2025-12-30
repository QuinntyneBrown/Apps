using RoadsideAssistanceInfoHub.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RoadsideAssistanceInfoHub.Api.Features.Policies;

public record GetPoliciesQuery : IRequest<IEnumerable<PolicyDto>>
{
    public Guid? VehicleId { get; init; }
    public string? Provider { get; init; }
}

public class GetPoliciesQueryHandler : IRequestHandler<GetPoliciesQuery, IEnumerable<PolicyDto>>
{
    private readonly IRoadsideAssistanceInfoHubContext _context;
    private readonly ILogger<GetPoliciesQueryHandler> _logger;

    public GetPoliciesQueryHandler(
        IRoadsideAssistanceInfoHubContext context,
        ILogger<GetPoliciesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<PolicyDto>> Handle(GetPoliciesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting policies");

        var query = _context.Policies.AsNoTracking();

        if (request.VehicleId.HasValue)
        {
            query = query.Where(p => p.VehicleId == request.VehicleId.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Provider))
        {
            query = query.Where(p => p.Provider.Contains(request.Provider));
        }

        var policies = await query
            .OrderBy(p => p.Provider)
            .ThenByDescending(p => p.EndDate)
            .ToListAsync(cancellationToken);

        return policies.Select(p => p.ToDto());
    }
}
