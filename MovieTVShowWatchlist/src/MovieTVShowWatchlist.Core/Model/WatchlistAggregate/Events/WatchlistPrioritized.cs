namespace MovieTVShowWatchlist.Core;

public class WatchlistPrioritized : DomainEvent
{
    public DateTime ReorderTimestamp { get; set; }
    public Dictionary<Guid, int> ItemRankings { get; set; } = new();
    public Dictionary<Guid, string> PriorityChanges { get; set; } = new();
    public string? SortingCriteria { get; set; }
    public List<string> MoodBasedCategories { get; set; } = new();
    public Dictionary<Guid, int> WatchOrderPreferences { get; set; } = new();
}
