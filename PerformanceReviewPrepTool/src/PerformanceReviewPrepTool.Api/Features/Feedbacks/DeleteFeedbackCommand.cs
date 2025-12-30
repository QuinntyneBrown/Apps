using PerformanceReviewPrepTool.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PerformanceReviewPrepTool.Api.Features.Feedbacks;

public record DeleteFeedbackCommand : IRequest<bool>
{
    public Guid FeedbackId { get; init; }
}

public class DeleteFeedbackCommandHandler : IRequestHandler<DeleteFeedbackCommand, bool>
{
    private readonly IPerformanceReviewPrepToolContext _context;
    private readonly ILogger<DeleteFeedbackCommandHandler> _logger;

    public DeleteFeedbackCommandHandler(
        IPerformanceReviewPrepToolContext context,
        ILogger<DeleteFeedbackCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteFeedbackCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting feedback {FeedbackId}", request.FeedbackId);

        var feedback = await _context.Feedbacks
            .FirstOrDefaultAsync(f => f.FeedbackId == request.FeedbackId, cancellationToken);

        if (feedback == null)
        {
            _logger.LogWarning("Feedback {FeedbackId} not found", request.FeedbackId);
            return false;
        }

        _context.Feedbacks.Remove(feedback);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted feedback {FeedbackId}", request.FeedbackId);

        return true;
    }
}
