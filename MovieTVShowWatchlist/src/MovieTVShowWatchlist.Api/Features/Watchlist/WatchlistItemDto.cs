namespace MovieTVShowWatchlist.Api;

public record WatchlistItemDto(
    Guid WatchlistItemId,
    Guid ContentId,
    string ContentType,
    string Title,
    DateTime AddedDate,
    string? PriorityLevel,
    int? PriorityRank,
    string? RecommendationSource,
    string? MoodCategory,
    int? WatchOrderPreference
);
