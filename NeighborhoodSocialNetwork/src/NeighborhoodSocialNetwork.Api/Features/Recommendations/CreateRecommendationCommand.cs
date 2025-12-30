using NeighborhoodSocialNetwork.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace NeighborhoodSocialNetwork.Api.Features.Recommendations;

public record CreateRecommendationCommand : IRequest<RecommendationDto>
{
    public Guid NeighborId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public RecommendationType RecommendationType { get; init; }
    public string? BusinessName { get; init; }
    public string? Location { get; init; }
    public int? Rating { get; init; }
}

public class CreateRecommendationCommandHandler : IRequestHandler<CreateRecommendationCommand, RecommendationDto>
{
    private readonly INeighborhoodSocialNetworkContext _context;
    private readonly ILogger<CreateRecommendationCommandHandler> _logger;

    public CreateRecommendationCommandHandler(
        INeighborhoodSocialNetworkContext context,
        ILogger<CreateRecommendationCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<RecommendationDto> Handle(CreateRecommendationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating recommendation for neighbor {NeighborId}, type: {RecommendationType}",
            request.NeighborId,
            request.RecommendationType);

        var recommendation = new Recommendation
        {
            RecommendationId = Guid.NewGuid(),
            NeighborId = request.NeighborId,
            Title = request.Title,
            Description = request.Description,
            RecommendationType = request.RecommendationType,
            BusinessName = request.BusinessName,
            Location = request.Location,
            Rating = request.Rating,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Recommendations.Add(recommendation);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Created recommendation {RecommendationId}", recommendation.RecommendationId);

        return recommendation.ToDto();
    }
}
