using PersonalHealthDashboard.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalHealthDashboard.Api.Features.Vitals;

public record GetVitalsQuery : IRequest<IEnumerable<VitalDto>>
{
    public Guid? UserId { get; init; }
    public VitalType? VitalType { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public string? Source { get; init; }
}

public class GetVitalsQueryHandler : IRequestHandler<GetVitalsQuery, IEnumerable<VitalDto>>
{
    private readonly IPersonalHealthDashboardContext _context;
    private readonly ILogger<GetVitalsQueryHandler> _logger;

    public GetVitalsQueryHandler(
        IPersonalHealthDashboardContext context,
        ILogger<GetVitalsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<VitalDto>> Handle(GetVitalsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting vitals for user {UserId}", request.UserId);

        var query = _context.Vitals.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(v => v.UserId == request.UserId.Value);
        }

        if (request.VitalType.HasValue)
        {
            query = query.Where(v => v.VitalType == request.VitalType.Value);
        }

        if (request.StartDate.HasValue)
        {
            query = query.Where(v => v.MeasuredAt >= request.StartDate.Value);
        }

        if (request.EndDate.HasValue)
        {
            query = query.Where(v => v.MeasuredAt <= request.EndDate.Value);
        }

        if (!string.IsNullOrEmpty(request.Source))
        {
            query = query.Where(v => v.Source == request.Source);
        }

        var vitals = await query
            .OrderByDescending(v => v.MeasuredAt)
            .ToListAsync(cancellationToken);

        return vitals.Select(v => v.ToDto());
    }
}
