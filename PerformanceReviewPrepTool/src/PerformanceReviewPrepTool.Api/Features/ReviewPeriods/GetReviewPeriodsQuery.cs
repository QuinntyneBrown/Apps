using PerformanceReviewPrepTool.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PerformanceReviewPrepTool.Api.Features.ReviewPeriods;

public record GetReviewPeriodsQuery : IRequest<IEnumerable<ReviewPeriodDto>>
{
    public Guid? UserId { get; init; }
    public bool? IsCompleted { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
}

public class GetReviewPeriodsQueryHandler : IRequestHandler<GetReviewPeriodsQuery, IEnumerable<ReviewPeriodDto>>
{
    private readonly IPerformanceReviewPrepToolContext _context;
    private readonly ILogger<GetReviewPeriodsQueryHandler> _logger;

    public GetReviewPeriodsQueryHandler(
        IPerformanceReviewPrepToolContext context,
        ILogger<GetReviewPeriodsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<ReviewPeriodDto>> Handle(GetReviewPeriodsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting review periods for user {UserId}", request.UserId);

        var query = _context.ReviewPeriods.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(r => r.UserId == request.UserId.Value);
        }

        if (request.IsCompleted.HasValue)
        {
            query = query.Where(r => r.IsCompleted == request.IsCompleted.Value);
        }

        if (request.StartDate.HasValue)
        {
            query = query.Where(r => r.StartDate >= request.StartDate.Value);
        }

        if (request.EndDate.HasValue)
        {
            query = query.Where(r => r.EndDate <= request.EndDate.Value);
        }

        var reviewPeriods = await query
            .OrderByDescending(r => r.StartDate)
            .ToListAsync(cancellationToken);

        return reviewPeriods.Select(r => r.ToDto());
    }
}
