using RoadsideAssistanceInfoHub.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RoadsideAssistanceInfoHub.Api.Features.Policies;

public record DeletePolicyCommand : IRequest<bool>
{
    public Guid PolicyId { get; init; }
}

public class DeletePolicyCommandHandler : IRequestHandler<DeletePolicyCommand, bool>
{
    private readonly IRoadsideAssistanceInfoHubContext _context;
    private readonly ILogger<DeletePolicyCommandHandler> _logger;

    public DeletePolicyCommandHandler(
        IRoadsideAssistanceInfoHubContext context,
        ILogger<DeletePolicyCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeletePolicyCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting policy {PolicyId}", request.PolicyId);

        var policy = await _context.Policies
            .FirstOrDefaultAsync(p => p.PolicyId == request.PolicyId, cancellationToken);

        if (policy == null)
        {
            _logger.LogWarning("Policy {PolicyId} not found", request.PolicyId);
            return false;
        }

        _context.Policies.Remove(policy);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted policy {PolicyId}", request.PolicyId);

        return true;
    }
}
