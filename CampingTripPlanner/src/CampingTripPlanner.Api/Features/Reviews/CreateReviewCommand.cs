using CampingTripPlanner.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CampingTripPlanner.Api.Features.Reviews;

public record CreateReviewCommand : IRequest<ReviewDto>
{
    public Guid UserId { get; init; }
    public Guid CampsiteId { get; init; }
    public int Rating { get; init; }
    public string? ReviewText { get; init; }
}

public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, ReviewDto>
{
    private readonly ICampingTripPlannerContext _context;
    private readonly ILogger<CreateReviewCommandHandler> _logger;

    public CreateReviewCommandHandler(
        ICampingTripPlannerContext context,
        ILogger<CreateReviewCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ReviewDto> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating review for user {UserId}, campsite: {CampsiteId}",
            request.UserId,
            request.CampsiteId);

        var review = new Review
        {
            ReviewId = Guid.NewGuid(),
            UserId = request.UserId,
            CampsiteId = request.CampsiteId,
            Rating = request.Rating,
            ReviewText = request.ReviewText,
            ReviewDate = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Reviews.Add(review);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created review {ReviewId} for user {UserId}",
            review.ReviewId,
            request.UserId);

        return review.ToDto();
    }
}
