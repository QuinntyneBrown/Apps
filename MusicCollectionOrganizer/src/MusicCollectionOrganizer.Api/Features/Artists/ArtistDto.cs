using MusicCollectionOrganizer.Core;

namespace MusicCollectionOrganizer.Api.Features.Artists;

public record ArtistDto
{
    public Guid ArtistId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Biography { get; init; }
    public string? Country { get; init; }
    public int? FormedYear { get; init; }
    public string? Website { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class ArtistExtensions
{
    public static ArtistDto ToDto(this Artist artist)
    {
        return new ArtistDto
        {
            ArtistId = artist.ArtistId,
            UserId = artist.UserId,
            Name = artist.Name,
            Biography = artist.Biography,
            Country = artist.Country,
            FormedYear = artist.FormedYear,
            Website = artist.Website,
            CreatedAt = artist.CreatedAt,
        };
    }
}
