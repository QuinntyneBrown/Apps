namespace MovieTVShowWatchlist.Core;

public class GenrePreference
{
    public Guid GenrePreferenceId { get; set; }
    public Guid UserId { get; set; }
    public string Genre { get; set; } = string.Empty;
    public decimal PreferenceStrength { get; set; }
    public string? Evidence { get; set; }
    public DateTime DetectionDate { get; set; }
    public TrendDirection TrendDirection { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public User User { get; set; } = null!;
}
