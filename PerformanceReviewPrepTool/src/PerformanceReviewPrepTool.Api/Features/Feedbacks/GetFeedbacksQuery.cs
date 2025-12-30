using PerformanceReviewPrepTool.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PerformanceReviewPrepTool.Api.Features.Feedbacks;

public record GetFeedbacksQuery : IRequest<IEnumerable<FeedbackDto>>
{
    public Guid? UserId { get; init; }
    public Guid? ReviewPeriodId { get; init; }
    public bool? IsKeyFeedback { get; init; }
    public string? FeedbackType { get; init; }
    public string? Category { get; init; }
}

public class GetFeedbacksQueryHandler : IRequestHandler<GetFeedbacksQuery, IEnumerable<FeedbackDto>>
{
    private readonly IPerformanceReviewPrepToolContext _context;
    private readonly ILogger<GetFeedbacksQueryHandler> _logger;

    public GetFeedbacksQueryHandler(
        IPerformanceReviewPrepToolContext context,
        ILogger<GetFeedbacksQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<FeedbackDto>> Handle(GetFeedbacksQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting feedbacks for user {UserId}", request.UserId);

        var query = _context.Feedbacks.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(f => f.UserId == request.UserId.Value);
        }

        if (request.ReviewPeriodId.HasValue)
        {
            query = query.Where(f => f.ReviewPeriodId == request.ReviewPeriodId.Value);
        }

        if (request.IsKeyFeedback.HasValue)
        {
            query = query.Where(f => f.IsKeyFeedback == request.IsKeyFeedback.Value);
        }

        if (!string.IsNullOrEmpty(request.FeedbackType))
        {
            query = query.Where(f => f.FeedbackType == request.FeedbackType);
        }

        if (!string.IsNullOrEmpty(request.Category))
        {
            query = query.Where(f => f.Category == request.Category);
        }

        var feedbacks = await query
            .OrderByDescending(f => f.ReceivedDate)
            .ToListAsync(cancellationToken);

        return feedbacks.Select(f => f.ToDto());
    }
}
