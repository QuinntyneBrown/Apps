using PerformanceReviewPrepTool.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PerformanceReviewPrepTool.Api.Features.ReviewPeriods;

public record DeleteReviewPeriodCommand : IRequest<bool>
{
    public Guid ReviewPeriodId { get; init; }
}

public class DeleteReviewPeriodCommandHandler : IRequestHandler<DeleteReviewPeriodCommand, bool>
{
    private readonly IPerformanceReviewPrepToolContext _context;
    private readonly ILogger<DeleteReviewPeriodCommandHandler> _logger;

    public DeleteReviewPeriodCommandHandler(
        IPerformanceReviewPrepToolContext context,
        ILogger<DeleteReviewPeriodCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteReviewPeriodCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting review period {ReviewPeriodId}", request.ReviewPeriodId);

        var reviewPeriod = await _context.ReviewPeriods
            .FirstOrDefaultAsync(r => r.ReviewPeriodId == request.ReviewPeriodId, cancellationToken);

        if (reviewPeriod == null)
        {
            _logger.LogWarning("Review period {ReviewPeriodId} not found", request.ReviewPeriodId);
            return false;
        }

        _context.ReviewPeriods.Remove(reviewPeriod);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted review period {ReviewPeriodId}", request.ReviewPeriodId);

        return true;
    }
}
