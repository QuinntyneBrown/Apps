using PersonalHealthDashboard.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalHealthDashboard.Api.Features.HealthTrends;

public record GetHealthTrendByIdQuery : IRequest<HealthTrendDto?>
{
    public Guid HealthTrendId { get; init; }
}

public class GetHealthTrendByIdQueryHandler : IRequestHandler<GetHealthTrendByIdQuery, HealthTrendDto?>
{
    private readonly IPersonalHealthDashboardContext _context;
    private readonly ILogger<GetHealthTrendByIdQueryHandler> _logger;

    public GetHealthTrendByIdQueryHandler(
        IPersonalHealthDashboardContext context,
        ILogger<GetHealthTrendByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<HealthTrendDto?> Handle(GetHealthTrendByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting health trend {HealthTrendId}", request.HealthTrendId);

        var healthTrend = await _context.HealthTrends
            .AsNoTracking()
            .FirstOrDefaultAsync(h => h.HealthTrendId == request.HealthTrendId, cancellationToken);

        if (healthTrend == null)
        {
            _logger.LogWarning("Health trend {HealthTrendId} not found", request.HealthTrendId);
            return null;
        }

        return healthTrend.ToDto();
    }
}
