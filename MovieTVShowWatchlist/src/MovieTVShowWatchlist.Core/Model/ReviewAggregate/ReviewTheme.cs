namespace MovieTVShowWatchlist.Core;

public class ReviewTheme
{
    public Guid ReviewThemeId { get; set; }
    public Guid ReviewId { get; set; }
    public string Theme { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    public Review Review { get; set; } = null!;
}
