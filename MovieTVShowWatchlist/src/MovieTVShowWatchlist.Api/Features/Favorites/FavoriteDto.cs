namespace MovieTVShowWatchlist.Api;

public record FavoriteDto(
    Guid FavoriteId,
    Guid ContentId,
    string ContentType,
    DateTime AddedDate,
    string? FavoriteCategory,
    int RewatchCount,
    string? EmotionalSignificance
);
