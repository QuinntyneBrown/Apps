using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Api;

public static class WatchlistItemExtensions
{
    public static WatchlistItemDto ToDto(this WatchlistItem item)
    {
        return new WatchlistItemDto(
            item.WatchlistItemId,
            item.ContentId,
            item.ContentType.ToString(),
            item.Title,
            item.AddedDate,
            item.PriorityLevel?.ToString(),
            item.PriorityRank,
            item.RecommendationSource,
            item.MoodCategory,
            item.WatchOrderPreference
        );
    }
}
