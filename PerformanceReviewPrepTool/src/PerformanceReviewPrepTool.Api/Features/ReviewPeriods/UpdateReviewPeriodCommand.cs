using PerformanceReviewPrepTool.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PerformanceReviewPrepTool.Api.Features.ReviewPeriods;

public record UpdateReviewPeriodCommand : IRequest<ReviewPeriodDto?>
{
    public Guid ReviewPeriodId { get; init; }
    public string Title { get; init; } = string.Empty;
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public DateTime? ReviewDueDate { get; init; }
    public string? ReviewerName { get; init; }
    public bool IsCompleted { get; init; }
    public string? Notes { get; init; }
}

public class UpdateReviewPeriodCommandHandler : IRequestHandler<UpdateReviewPeriodCommand, ReviewPeriodDto?>
{
    private readonly IPerformanceReviewPrepToolContext _context;
    private readonly ILogger<UpdateReviewPeriodCommandHandler> _logger;

    public UpdateReviewPeriodCommandHandler(
        IPerformanceReviewPrepToolContext context,
        ILogger<UpdateReviewPeriodCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ReviewPeriodDto?> Handle(UpdateReviewPeriodCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating review period {ReviewPeriodId}", request.ReviewPeriodId);

        var reviewPeriod = await _context.ReviewPeriods
            .FirstOrDefaultAsync(r => r.ReviewPeriodId == request.ReviewPeriodId, cancellationToken);

        if (reviewPeriod == null)
        {
            _logger.LogWarning("Review period {ReviewPeriodId} not found", request.ReviewPeriodId);
            return null;
        }

        reviewPeriod.Title = request.Title;
        reviewPeriod.StartDate = request.StartDate;
        reviewPeriod.EndDate = request.EndDate;
        reviewPeriod.ReviewDueDate = request.ReviewDueDate;
        reviewPeriod.ReviewerName = request.ReviewerName;
        reviewPeriod.Notes = request.Notes;
        reviewPeriod.UpdatedAt = DateTime.UtcNow;

        if (request.IsCompleted && !reviewPeriod.IsCompleted)
        {
            reviewPeriod.Complete();
        }

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated review period {ReviewPeriodId}", request.ReviewPeriodId);

        return reviewPeriod.ToDto();
    }
}
