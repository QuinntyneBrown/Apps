namespace MovieTVShowWatchlist.Api;

public record RemoveWatchlistItemRequest(
    string? RemovalReason,
    string? AlternativeAdded
);
