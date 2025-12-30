using BookReadingTrackerLibrary.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookReadingTrackerLibrary.Api.Features.Reviews;

public record CreateReviewCommand : IRequest<ReviewDto>
{
    public Guid UserId { get; init; }
    public Guid BookId { get; init; }
    public int Rating { get; init; }
    public string ReviewText { get; init; } = string.Empty;
    public bool IsRecommended { get; init; }
}

public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, ReviewDto>
{
    private readonly IBookReadingTrackerLibraryContext _context;
    private readonly ILogger<CreateReviewCommandHandler> _logger;

    public CreateReviewCommandHandler(
        IBookReadingTrackerLibraryContext context,
        ILogger<CreateReviewCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ReviewDto> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating review for user {UserId}, book {BookId}",
            request.UserId,
            request.BookId);

        var review = new Review
        {
            ReviewId = Guid.NewGuid(),
            UserId = request.UserId,
            BookId = request.BookId,
            Rating = request.Rating,
            ReviewText = request.ReviewText,
            IsRecommended = request.IsRecommended,
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
