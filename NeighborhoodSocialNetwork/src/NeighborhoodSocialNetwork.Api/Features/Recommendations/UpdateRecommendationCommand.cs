using NeighborhoodSocialNetwork.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace NeighborhoodSocialNetwork.Api.Features.Recommendations;

public record UpdateRecommendationCommand : IRequest<RecommendationDto?>
{
    public Guid RecommendationId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public RecommendationType RecommendationType { get; init; }
    public string? BusinessName { get; init; }
    public string? Location { get; init; }
    public int? Rating { get; init; }
}

public class UpdateRecommendationCommandHandler : IRequestHandler<UpdateRecommendationCommand, RecommendationDto?>
{
    private readonly INeighborhoodSocialNetworkContext _context;
    private readonly ILogger<UpdateRecommendationCommandHandler> _logger;

    public UpdateRecommendationCommandHandler(
        INeighborhoodSocialNetworkContext context,
        ILogger<UpdateRecommendationCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<RecommendationDto?> Handle(UpdateRecommendationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating recommendation {RecommendationId}", request.RecommendationId);

        var recommendation = await _context.Recommendations
            .FirstOrDefaultAsync(r => r.RecommendationId == request.RecommendationId, cancellationToken);

        if (recommendation == null)
        {
            _logger.LogWarning("Recommendation {RecommendationId} not found", request.RecommendationId);
            return null;
        }

        recommendation.Title = request.Title;
        recommendation.Description = request.Description;
        recommendation.RecommendationType = request.RecommendationType;
        recommendation.BusinessName = request.BusinessName;
        recommendation.Location = request.Location;
        recommendation.Rating = request.Rating;
        recommendation.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated recommendation {RecommendationId}", request.RecommendationId);

        return recommendation.ToDto();
    }
}
