using MovieTVShowWatchlist.Core;

namespace MovieTVShowWatchlist.Api;

public static class FavoriteExtensions
{
    public static FavoriteDto ToDto(this Favorite favorite)
    {
        return new FavoriteDto(
            favorite.FavoriteId,
            favorite.ContentId,
            favorite.ContentType.ToString(),
            favorite.AddedDate,
            favorite.FavoriteCategory,
            favorite.RewatchCount,
            favorite.EmotionalSignificance
        );
    }
}
