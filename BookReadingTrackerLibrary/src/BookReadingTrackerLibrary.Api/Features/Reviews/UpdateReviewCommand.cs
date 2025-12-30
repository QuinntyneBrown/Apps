using BookReadingTrackerLibrary.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookReadingTrackerLibrary.Api.Features.Reviews;

public record UpdateReviewCommand : IRequest<ReviewDto?>
{
    public Guid ReviewId { get; init; }
    public int Rating { get; init; }
    public string ReviewText { get; init; } = string.Empty;
    public bool IsRecommended { get; init; }
}

public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, ReviewDto?>
{
    private readonly IBookReadingTrackerLibraryContext _context;
    private readonly ILogger<UpdateReviewCommandHandler> _logger;

    public UpdateReviewCommandHandler(
        IBookReadingTrackerLibraryContext context,
        ILogger<UpdateReviewCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ReviewDto?> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating review {ReviewId}", request.ReviewId);

        var review = await _context.Reviews
            .FirstOrDefaultAsync(r => r.ReviewId == request.ReviewId, cancellationToken);

        if (review == null)
        {
            _logger.LogWarning("Review {ReviewId} not found", request.ReviewId);
            return null;
        }

        review.Rating = request.Rating;
        review.ReviewText = request.ReviewText;
        review.IsRecommended = request.IsRecommended;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated review {ReviewId}", request.ReviewId);

        return review.ToDto();
    }
}
