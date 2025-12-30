using PerformanceReviewPrepTool.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PerformanceReviewPrepTool.Api.Features.Feedbacks;

public record GetFeedbackByIdQuery : IRequest<FeedbackDto?>
{
    public Guid FeedbackId { get; init; }
}

public class GetFeedbackByIdQueryHandler : IRequestHandler<GetFeedbackByIdQuery, FeedbackDto?>
{
    private readonly IPerformanceReviewPrepToolContext _context;
    private readonly ILogger<GetFeedbackByIdQueryHandler> _logger;

    public GetFeedbackByIdQueryHandler(
        IPerformanceReviewPrepToolContext context,
        ILogger<GetFeedbackByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<FeedbackDto?> Handle(GetFeedbackByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting feedback {FeedbackId}", request.FeedbackId);

        var feedback = await _context.Feedbacks
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.FeedbackId == request.FeedbackId, cancellationToken);

        if (feedback == null)
        {
            _logger.LogWarning("Feedback {FeedbackId} not found", request.FeedbackId);
            return null;
        }

        return feedback.ToDto();
    }
}
