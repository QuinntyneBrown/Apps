using NeighborhoodSocialNetwork.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace NeighborhoodSocialNetwork.Api.Features.Recommendations;

public record DeleteRecommendationCommand : IRequest<bool>
{
    public Guid RecommendationId { get; init; }
}

public class DeleteRecommendationCommandHandler : IRequestHandler<DeleteRecommendationCommand, bool>
{
    private readonly INeighborhoodSocialNetworkContext _context;
    private readonly ILogger<DeleteRecommendationCommandHandler> _logger;

    public DeleteRecommendationCommandHandler(
        INeighborhoodSocialNetworkContext context,
        ILogger<DeleteRecommendationCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteRecommendationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting recommendation {RecommendationId}", request.RecommendationId);

        var recommendation = await _context.Recommendations
            .FirstOrDefaultAsync(r => r.RecommendationId == request.RecommendationId, cancellationToken);

        if (recommendation == null)
        {
            _logger.LogWarning("Recommendation {RecommendationId} not found", request.RecommendationId);
            return false;
        }

        _context.Recommendations.Remove(recommendation);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted recommendation {RecommendationId}", request.RecommendationId);

        return true;
    }
}
