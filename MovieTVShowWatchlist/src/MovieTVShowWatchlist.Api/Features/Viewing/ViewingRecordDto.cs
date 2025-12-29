namespace MovieTVShowWatchlist.Api;

public record ViewingRecordDto(
    Guid ViewingRecordId,
    Guid ContentId,
    string ContentType,
    DateTime WatchDate,
    string? ViewingPlatform,
    string? ViewingLocation,
    string? ViewingContext,
    bool IsRewatch,
    int? ViewingDurationMinutes,
    List<string>? Companions
);
