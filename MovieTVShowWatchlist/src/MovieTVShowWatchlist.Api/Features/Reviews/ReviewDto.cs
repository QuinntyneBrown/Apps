namespace MovieTVShowWatchlist.Api;

public record ReviewDto(
    Guid ReviewId,
    Guid ContentId,
    string ContentType,
    string ReviewText,
    bool HasSpoilers,
    DateTime ReviewDate,
    bool WouldRecommend,
    string? TargetAudience,
    List<string>? Themes
);
