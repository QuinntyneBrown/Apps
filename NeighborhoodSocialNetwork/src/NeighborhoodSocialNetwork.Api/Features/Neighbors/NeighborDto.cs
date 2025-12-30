using NeighborhoodSocialNetwork.Core;

namespace NeighborhoodSocialNetwork.Api.Features.Neighbors;

public record NeighborDto
{
    public Guid NeighborId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Address { get; init; }
    public string? ContactInfo { get; init; }
    public string? Bio { get; init; }
    public string? Interests { get; init; }
    public bool IsVerified { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

public static class NeighborExtensions
{
    public static NeighborDto ToDto(this Neighbor neighbor)
    {
        return new NeighborDto
        {
            NeighborId = neighbor.NeighborId,
            UserId = neighbor.UserId,
            Name = neighbor.Name,
            Address = neighbor.Address,
            ContactInfo = neighbor.ContactInfo,
            Bio = neighbor.Bio,
            Interests = neighbor.Interests,
            IsVerified = neighbor.IsVerified,
            CreatedAt = neighbor.CreatedAt,
            UpdatedAt = neighbor.UpdatedAt,
        };
    }
}
