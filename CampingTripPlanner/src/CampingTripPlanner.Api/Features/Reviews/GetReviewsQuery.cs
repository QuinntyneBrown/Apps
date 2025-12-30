using CampingTripPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CampingTripPlanner.Api.Features.Reviews;

public record GetReviewsQuery : IRequest<IEnumerable<ReviewDto>>
{
    public Guid? UserId { get; init; }
    public Guid? CampsiteId { get; init; }
}

public class GetReviewsQueryHandler : IRequestHandler<GetReviewsQuery, IEnumerable<ReviewDto>>
{
    private readonly ICampingTripPlannerContext _context;
    private readonly ILogger<GetReviewsQueryHandler> _logger;

    public GetReviewsQueryHandler(
        ICampingTripPlannerContext context,
        ILogger<GetReviewsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<ReviewDto>> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting reviews for user {UserId}", request.UserId);

        var query = _context.Reviews.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(r => r.UserId == request.UserId.Value);
        }

        if (request.CampsiteId.HasValue)
        {
            query = query.Where(r => r.CampsiteId == request.CampsiteId.Value);
        }

        var reviews = await query
            .OrderByDescending(r => r.ReviewDate)
            .ToListAsync(cancellationToken);

        return reviews.Select(r => r.ToDto());
    }
}
