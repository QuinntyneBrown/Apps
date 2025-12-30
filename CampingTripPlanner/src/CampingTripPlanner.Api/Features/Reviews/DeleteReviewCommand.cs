using CampingTripPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CampingTripPlanner.Api.Features.Reviews;

public record DeleteReviewCommand : IRequest<bool>
{
    public Guid ReviewId { get; init; }
}

public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, bool>
{
    private readonly ICampingTripPlannerContext _context;
    private readonly ILogger<DeleteReviewCommandHandler> _logger;

    public DeleteReviewCommandHandler(
        ICampingTripPlannerContext context,
        ILogger<DeleteReviewCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting review {ReviewId}", request.ReviewId);

        var review = await _context.Reviews
            .FirstOrDefaultAsync(r => r.ReviewId == request.ReviewId, cancellationToken);

        if (review == null)
        {
            _logger.LogWarning("Review {ReviewId} not found", request.ReviewId);
            return false;
        }

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted review {ReviewId}", request.ReviewId);

        return true;
    }
}
