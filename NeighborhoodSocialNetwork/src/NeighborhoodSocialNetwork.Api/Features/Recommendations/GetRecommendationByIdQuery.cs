using NeighborhoodSocialNetwork.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace NeighborhoodSocialNetwork.Api.Features.Recommendations;

public record GetRecommendationByIdQuery : IRequest<RecommendationDto?>
{
    public Guid RecommendationId { get; init; }
}

public class GetRecommendationByIdQueryHandler : IRequestHandler<GetRecommendationByIdQuery, RecommendationDto?>
{
    private readonly INeighborhoodSocialNetworkContext _context;
    private readonly ILogger<GetRecommendationByIdQueryHandler> _logger;

    public GetRecommendationByIdQueryHandler(
        INeighborhoodSocialNetworkContext context,
        ILogger<GetRecommendationByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<RecommendationDto?> Handle(GetRecommendationByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting recommendation {RecommendationId}", request.RecommendationId);

        var recommendation = await _context.Recommendations
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.RecommendationId == request.RecommendationId, cancellationToken);

        if (recommendation == null)
        {
            _logger.LogWarning("Recommendation {RecommendationId} not found", request.RecommendationId);
            return null;
        }

        return recommendation.ToDto();
    }
}
