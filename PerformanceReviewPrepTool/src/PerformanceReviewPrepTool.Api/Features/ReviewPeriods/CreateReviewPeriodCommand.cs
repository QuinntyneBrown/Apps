using PerformanceReviewPrepTool.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PerformanceReviewPrepTool.Api.Features.ReviewPeriods;

public record CreateReviewPeriodCommand : IRequest<ReviewPeriodDto>
{
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public DateTime? ReviewDueDate { get; init; }
    public string? ReviewerName { get; init; }
    public string? Notes { get; init; }
}

public class CreateReviewPeriodCommandHandler : IRequestHandler<CreateReviewPeriodCommand, ReviewPeriodDto>
{
    private readonly IPerformanceReviewPrepToolContext _context;
    private readonly ILogger<CreateReviewPeriodCommandHandler> _logger;

    public CreateReviewPeriodCommandHandler(
        IPerformanceReviewPrepToolContext context,
        ILogger<CreateReviewPeriodCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ReviewPeriodDto> Handle(CreateReviewPeriodCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating review period for user {UserId}: {Title}",
            request.UserId,
            request.Title);

        var reviewPeriod = new ReviewPeriod
        {
            ReviewPeriodId = Guid.NewGuid(),
            UserId = request.UserId,
            Title = request.Title,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            ReviewDueDate = request.ReviewDueDate,
            ReviewerName = request.ReviewerName,
            IsCompleted = false,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        _context.ReviewPeriods.Add(reviewPeriod);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created review period {ReviewPeriodId} for user {UserId}",
            reviewPeriod.ReviewPeriodId,
            request.UserId);

        return reviewPeriod.ToDto();
    }
}
