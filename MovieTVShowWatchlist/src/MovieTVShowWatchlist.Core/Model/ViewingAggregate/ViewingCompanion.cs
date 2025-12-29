namespace MovieTVShowWatchlist.Core;

public class ViewingCompanion
{
    public Guid ViewingCompanionId { get; set; }
    public Guid ViewingRecordId { get; set; }
    public string CompanionName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public ViewingRecord ViewingRecord { get; set; } = null!;
}
