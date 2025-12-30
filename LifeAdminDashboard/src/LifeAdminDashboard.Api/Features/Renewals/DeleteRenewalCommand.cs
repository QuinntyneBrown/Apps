using LifeAdminDashboard.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LifeAdminDashboard.Api.Features.Renewals;

public record DeleteRenewalCommand : IRequest<bool>
{
    public Guid RenewalId { get; init; }
}

public class DeleteRenewalCommandHandler : IRequestHandler<DeleteRenewalCommand, bool>
{
    private readonly ILifeAdminDashboardContext _context;
    private readonly ILogger<DeleteRenewalCommandHandler> _logger;

    public DeleteRenewalCommandHandler(
        ILifeAdminDashboardContext context,
        ILogger<DeleteRenewalCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteRenewalCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting renewal {RenewalId}", request.RenewalId);

        var renewal = await _context.Renewals
            .FirstOrDefaultAsync(r => r.RenewalId == request.RenewalId, cancellationToken);

        if (renewal == null)
        {
            _logger.LogWarning("Renewal {RenewalId} not found", request.RenewalId);
            return false;
        }

        _context.Renewals.Remove(renewal);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted renewal {RenewalId}", request.RenewalId);

        return true;
    }
}
