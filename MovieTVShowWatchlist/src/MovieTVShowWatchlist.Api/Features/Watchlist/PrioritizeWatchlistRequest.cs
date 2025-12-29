namespace MovieTVShowWatchlist.Api;

public record PrioritizeWatchlistRequest(
    Dictionary<Guid, int> ItemRankings,
    string? SortingCriteria,
    List<string>? MoodBasedCategories
);
