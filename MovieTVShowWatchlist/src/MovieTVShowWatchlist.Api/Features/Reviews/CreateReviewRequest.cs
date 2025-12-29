namespace MovieTVShowWatchlist.Api;

public record CreateReviewRequest(
    Guid ContentId,
    string ContentType,
    string ReviewText,
    bool HasSpoilers,
    List<string>? ThemesDiscussed,
    bool WouldRecommend,
    string? TargetAudience
);
