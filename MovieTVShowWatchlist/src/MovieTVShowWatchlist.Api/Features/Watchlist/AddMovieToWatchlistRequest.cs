namespace MovieTVShowWatchlist.Api;

public record AddMovieToWatchlistRequest(
    Guid MovieId,
    string Title,
    int ReleaseYear,
    List<string>? Genres,
    string? Director,
    int? Runtime,
    string? PriorityLevel,
    string? RecommendationSource,
    string? Availability
);
