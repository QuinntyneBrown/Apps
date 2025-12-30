using PerformanceReviewPrepTool.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PerformanceReviewPrepTool.Api.Features.Feedbacks;

public record CreateFeedbackCommand : IRequest<FeedbackDto>
{
    public Guid UserId { get; init; }
    public Guid ReviewPeriodId { get; init; }
    public string Source { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public DateTime ReceivedDate { get; init; }
    public string? FeedbackType { get; init; }
    public string? Category { get; init; }
    public bool IsKeyFeedback { get; init; }
    public string? Notes { get; init; }
}

public class CreateFeedbackCommandHandler : IRequestHandler<CreateFeedbackCommand, FeedbackDto>
{
    private readonly IPerformanceReviewPrepToolContext _context;
    private readonly ILogger<CreateFeedbackCommandHandler> _logger;

    public CreateFeedbackCommandHandler(
        IPerformanceReviewPrepToolContext context,
        ILogger<CreateFeedbackCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<FeedbackDto> Handle(CreateFeedbackCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating feedback for user {UserId} from source: {Source}",
            request.UserId,
            request.Source);

        var feedback = new Feedback
        {
            FeedbackId = Guid.NewGuid(),
            UserId = request.UserId,
            ReviewPeriodId = request.ReviewPeriodId,
            Source = request.Source,
            Content = request.Content,
            ReceivedDate = request.ReceivedDate,
            FeedbackType = request.FeedbackType,
            Category = request.Category,
            IsKeyFeedback = request.IsKeyFeedback,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Feedbacks.Add(feedback);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created feedback {FeedbackId} for user {UserId}",
            feedback.FeedbackId,
            request.UserId);

        return feedback.ToDto();
    }
}
