using CampingTripPlanner.Core;

namespace CampingTripPlanner.Api.Features.Campsites;

public record CampsiteDto
{
    public Guid CampsiteId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Location { get; init; } = string.Empty;
    public CampsiteType CampsiteType { get; init; }
    public string? Description { get; init; }
    public bool HasElectricity { get; init; }
    public bool HasWater { get; init; }
    public decimal? CostPerNight { get; init; }
    public bool IsFavorite { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class CampsiteExtensions
{
    public static CampsiteDto ToDto(this Campsite campsite)
    {
        return new CampsiteDto
        {
            CampsiteId = campsite.CampsiteId,
            UserId = campsite.UserId,
            Name = campsite.Name,
            Location = campsite.Location,
            CampsiteType = campsite.CampsiteType,
            Description = campsite.Description,
            HasElectricity = campsite.HasElectricity,
            HasWater = campsite.HasWater,
            CostPerNight = campsite.CostPerNight,
            IsFavorite = campsite.IsFavorite,
            CreatedAt = campsite.CreatedAt,
        };
    }
}
