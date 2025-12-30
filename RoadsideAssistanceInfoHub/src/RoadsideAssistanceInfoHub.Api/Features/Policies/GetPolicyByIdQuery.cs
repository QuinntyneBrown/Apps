using RoadsideAssistanceInfoHub.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RoadsideAssistanceInfoHub.Api.Features.Policies;

public record GetPolicyByIdQuery : IRequest<PolicyDto?>
{
    public Guid PolicyId { get; init; }
}

public class GetPolicyByIdQueryHandler : IRequestHandler<GetPolicyByIdQuery, PolicyDto?>
{
    private readonly IRoadsideAssistanceInfoHubContext _context;
    private readonly ILogger<GetPolicyByIdQueryHandler> _logger;

    public GetPolicyByIdQueryHandler(
        IRoadsideAssistanceInfoHubContext context,
        ILogger<GetPolicyByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PolicyDto?> Handle(GetPolicyByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting policy {PolicyId}", request.PolicyId);

        var policy = await _context.Policies
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.PolicyId == request.PolicyId, cancellationToken);

        if (policy == null)
        {
            _logger.LogWarning("Policy {PolicyId} not found", request.PolicyId);
            return null;
        }

        return policy.ToDto();
    }
}
