namespace MovieTVShowWatchlist.Core;

public class ContentGenre
{
    public Guid ContentGenreId { get; set; }
    public Guid ContentId { get; set; }
    public ContentType ContentType { get; set; }
    public string Genre { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
