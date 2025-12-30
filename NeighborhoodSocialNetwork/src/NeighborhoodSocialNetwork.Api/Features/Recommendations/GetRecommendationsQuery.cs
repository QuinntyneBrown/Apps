using NeighborhoodSocialNetwork.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace NeighborhoodSocialNetwork.Api.Features.Recommendations;

public record GetRecommendationsQuery : IRequest<IEnumerable<RecommendationDto>>
{
    public Guid? NeighborId { get; init; }
    public RecommendationType? RecommendationType { get; init; }
    public int? MinRating { get; init; }
    public string? SearchTerm { get; init; }
}

public class GetRecommendationsQueryHandler : IRequestHandler<GetRecommendationsQuery, IEnumerable<RecommendationDto>>
{
    private readonly INeighborhoodSocialNetworkContext _context;
    private readonly ILogger<GetRecommendationsQueryHandler> _logger;

    public GetRecommendationsQueryHandler(
        INeighborhoodSocialNetworkContext context,
        ILogger<GetRecommendationsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<RecommendationDto>> Handle(GetRecommendationsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting recommendations");

        var query = _context.Recommendations.AsNoTracking();

        if (request.NeighborId.HasValue)
        {
            query = query.Where(r => r.NeighborId == request.NeighborId.Value);
        }

        if (request.RecommendationType.HasValue)
        {
            query = query.Where(r => r.RecommendationType == request.RecommendationType.Value);
        }

        if (request.MinRating.HasValue)
        {
            query = query.Where(r => r.Rating >= request.MinRating.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            query = query.Where(r =>
                r.Title.Contains(request.SearchTerm) ||
                r.Description.Contains(request.SearchTerm) ||
                (r.BusinessName != null && r.BusinessName.Contains(request.SearchTerm)));
        }

        var recommendations = await query
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync(cancellationToken);

        return recommendations.Select(r => r.ToDto());
    }
}
