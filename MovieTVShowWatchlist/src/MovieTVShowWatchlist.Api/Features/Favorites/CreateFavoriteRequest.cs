namespace MovieTVShowWatchlist.Api;

public record CreateFavoriteRequest(
    string ContentType,
    string? FavoriteCategory,
    string? EmotionalSignificance
);
