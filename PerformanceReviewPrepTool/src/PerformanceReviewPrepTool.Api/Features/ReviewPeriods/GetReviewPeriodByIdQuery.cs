using PerformanceReviewPrepTool.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PerformanceReviewPrepTool.Api.Features.ReviewPeriods;

public record GetReviewPeriodByIdQuery : IRequest<ReviewPeriodDto?>
{
    public Guid ReviewPeriodId { get; init; }
}

public class GetReviewPeriodByIdQueryHandler : IRequestHandler<GetReviewPeriodByIdQuery, ReviewPeriodDto?>
{
    private readonly IPerformanceReviewPrepToolContext _context;
    private readonly ILogger<GetReviewPeriodByIdQueryHandler> _logger;

    public GetReviewPeriodByIdQueryHandler(
        IPerformanceReviewPrepToolContext context,
        ILogger<GetReviewPeriodByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ReviewPeriodDto?> Handle(GetReviewPeriodByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting review period {ReviewPeriodId}", request.ReviewPeriodId);

        var reviewPeriod = await _context.ReviewPeriods
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.ReviewPeriodId == request.ReviewPeriodId, cancellationToken);

        if (reviewPeriod == null)
        {
            _logger.LogWarning("Review period {ReviewPeriodId} not found", request.ReviewPeriodId);
            return null;
        }

        return reviewPeriod.ToDto();
    }
}
