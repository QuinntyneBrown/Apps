using MusicCollectionOrganizer.Core;

namespace MusicCollectionOrganizer.Api.Features.Albums;

public record AlbumDto
{
    public Guid AlbumId { get; init; }
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public Guid? ArtistId { get; init; }
    public string? ArtistName { get; init; }
    public Format Format { get; init; }
    public Genre Genre { get; init; }
    public int? ReleaseYear { get; init; }
    public string? Label { get; init; }
    public decimal? PurchasePrice { get; init; }
    public DateTime? PurchaseDate { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class AlbumExtensions
{
    public static AlbumDto ToDto(this Album album)
    {
        return new AlbumDto
        {
            AlbumId = album.AlbumId,
            UserId = album.UserId,
            Title = album.Title,
            ArtistId = album.ArtistId,
            ArtistName = album.Artist?.Name,
            Format = album.Format,
            Genre = album.Genre,
            ReleaseYear = album.ReleaseYear,
            Label = album.Label,
            PurchasePrice = album.PurchasePrice,
            PurchaseDate = album.PurchaseDate,
            Notes = album.Notes,
            CreatedAt = album.CreatedAt,
        };
    }
}
