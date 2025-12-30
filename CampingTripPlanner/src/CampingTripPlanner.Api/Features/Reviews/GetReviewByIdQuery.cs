using CampingTripPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CampingTripPlanner.Api.Features.Reviews;

public record GetReviewByIdQuery : IRequest<ReviewDto?>
{
    public Guid ReviewId { get; init; }
}

public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, ReviewDto?>
{
    private readonly ICampingTripPlannerContext _context;
    private readonly ILogger<GetReviewByIdQueryHandler> _logger;

    public GetReviewByIdQueryHandler(
        ICampingTripPlannerContext context,
        ILogger<GetReviewByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ReviewDto?> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting review {ReviewId}", request.ReviewId);

        var review = await _context.Reviews
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.ReviewId == request.ReviewId, cancellationToken);

        if (review == null)
        {
            _logger.LogWarning("Review {ReviewId} not found", request.ReviewId);
            return null;
        }

        return review.ToDto();
    }
}
