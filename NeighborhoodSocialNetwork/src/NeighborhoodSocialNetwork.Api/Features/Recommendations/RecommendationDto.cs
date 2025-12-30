using NeighborhoodSocialNetwork.Core;

namespace NeighborhoodSocialNetwork.Api.Features.Recommendations;

public record RecommendationDto
{
    public Guid RecommendationId { get; init; }
    public Guid NeighborId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public RecommendationType RecommendationType { get; init; }
    public string? BusinessName { get; init; }
    public string? Location { get; init; }
    public int? Rating { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

public static class RecommendationExtensions
{
    public static RecommendationDto ToDto(this Recommendation recommendation)
    {
        return new RecommendationDto
        {
            RecommendationId = recommendation.RecommendationId,
            NeighborId = recommendation.NeighborId,
            Title = recommendation.Title,
            Description = recommendation.Description,
            RecommendationType = recommendation.RecommendationType,
            BusinessName = recommendation.BusinessName,
            Location = recommendation.Location,
            Rating = recommendation.Rating,
            CreatedAt = recommendation.CreatedAt,
            UpdatedAt = recommendation.UpdatedAt,
        };
    }
}
