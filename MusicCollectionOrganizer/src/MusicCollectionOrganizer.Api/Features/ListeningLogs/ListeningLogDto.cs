using MusicCollectionOrganizer.Core;

namespace MusicCollectionOrganizer.Api.Features.ListeningLogs;

public record ListeningLogDto
{
    public Guid ListeningLogId { get; init; }
    public Guid UserId { get; init; }
    public Guid AlbumId { get; init; }
    public string? AlbumTitle { get; init; }
    public DateTime ListeningDate { get; init; }
    public int? Rating { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class ListeningLogExtensions
{
    public static ListeningLogDto ToDto(this ListeningLog listeningLog)
    {
        return new ListeningLogDto
        {
            ListeningLogId = listeningLog.ListeningLogId,
            UserId = listeningLog.UserId,
            AlbumId = listeningLog.AlbumId,
            AlbumTitle = listeningLog.Album?.Title,
            ListeningDate = listeningLog.ListeningDate,
            Rating = listeningLog.Rating,
            Notes = listeningLog.Notes,
            CreatedAt = listeningLog.CreatedAt,
        };
    }
}
