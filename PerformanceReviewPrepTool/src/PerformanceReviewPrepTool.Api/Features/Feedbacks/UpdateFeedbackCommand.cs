using PerformanceReviewPrepTool.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PerformanceReviewPrepTool.Api.Features.Feedbacks;

public record UpdateFeedbackCommand : IRequest<FeedbackDto?>
{
    public Guid FeedbackId { get; init; }
    public string Source { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public DateTime ReceivedDate { get; init; }
    public string? FeedbackType { get; init; }
    public string? Category { get; init; }
    public bool IsKeyFeedback { get; init; }
    public string? Notes { get; init; }
}

public class UpdateFeedbackCommandHandler : IRequestHandler<UpdateFeedbackCommand, FeedbackDto?>
{
    private readonly IPerformanceReviewPrepToolContext _context;
    private readonly ILogger<UpdateFeedbackCommandHandler> _logger;

    public UpdateFeedbackCommandHandler(
        IPerformanceReviewPrepToolContext context,
        ILogger<UpdateFeedbackCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<FeedbackDto?> Handle(UpdateFeedbackCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating feedback {FeedbackId}", request.FeedbackId);

        var feedback = await _context.Feedbacks
            .FirstOrDefaultAsync(f => f.FeedbackId == request.FeedbackId, cancellationToken);

        if (feedback == null)
        {
            _logger.LogWarning("Feedback {FeedbackId} not found", request.FeedbackId);
            return null;
        }

        feedback.Source = request.Source;
        feedback.Content = request.Content;
        feedback.ReceivedDate = request.ReceivedDate;
        feedback.FeedbackType = request.FeedbackType;
        feedback.Category = request.Category;
        feedback.IsKeyFeedback = request.IsKeyFeedback;
        feedback.Notes = request.Notes;
        feedback.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated feedback {FeedbackId}", request.FeedbackId);

        return feedback.ToDto();
    }
}
