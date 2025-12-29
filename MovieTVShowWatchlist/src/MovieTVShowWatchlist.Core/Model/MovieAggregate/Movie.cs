namespace MovieTVShowWatchlist.Core;

public class Movie
{
    public Guid MovieId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public string? Director { get; set; }
    public int? Runtime { get; set; }
    public string? ExternalId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<ContentGenre> Genres { get; set; } = new List<ContentGenre>();
    public ICollection<ContentAvailability> Availabilities { get; set; } = new List<ContentAvailability>();
}
