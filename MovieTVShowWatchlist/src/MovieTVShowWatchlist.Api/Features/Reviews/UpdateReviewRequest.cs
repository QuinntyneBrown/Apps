namespace MovieTVShowWatchlist.Api;

public record UpdateReviewRequest(
    string ReviewText,
    bool HasSpoilers,
    List<string>? ThemesDiscussed,
    bool WouldRecommend,
    string? TargetAudience
);
