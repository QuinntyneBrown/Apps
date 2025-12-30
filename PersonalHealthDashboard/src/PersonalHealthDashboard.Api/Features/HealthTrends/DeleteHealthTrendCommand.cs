using PersonalHealthDashboard.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalHealthDashboard.Api.Features.HealthTrends;

public record DeleteHealthTrendCommand : IRequest<bool>
{
    public Guid HealthTrendId { get; init; }
}

public class DeleteHealthTrendCommandHandler : IRequestHandler<DeleteHealthTrendCommand, bool>
{
    private readonly IPersonalHealthDashboardContext _context;
    private readonly ILogger<DeleteHealthTrendCommandHandler> _logger;

    public DeleteHealthTrendCommandHandler(
        IPersonalHealthDashboardContext context,
        ILogger<DeleteHealthTrendCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteHealthTrendCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting health trend {HealthTrendId}", request.HealthTrendId);

        var healthTrend = await _context.HealthTrends
            .FirstOrDefaultAsync(h => h.HealthTrendId == request.HealthTrendId, cancellationToken);

        if (healthTrend == null)
        {
            _logger.LogWarning("Health trend {HealthTrendId} not found", request.HealthTrendId);
            return false;
        }

        _context.HealthTrends.Remove(healthTrend);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted health trend {HealthTrendId}", request.HealthTrendId);

        return true;
    }
}
