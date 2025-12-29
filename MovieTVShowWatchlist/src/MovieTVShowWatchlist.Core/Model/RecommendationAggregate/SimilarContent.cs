namespace MovieTVShowWatchlist.Core;

public class SimilarContent
{
    public Guid SimilarContentId { get; set; }
    public Guid SourceContentId { get; set; }
    public ContentType SourceContentType { get; set; }
    public Guid SimilarToContentId { get; set; }
    public ContentType SimilarToContentType { get; set; }
    public decimal SimilarityScore { get; set; }
    public string? MatchReasons { get; set; }
    public DateTime DiscoveryDate { get; set; }
    public string? AlgorithmVersion { get; set; }
    public DateTime CreatedAt { get; set; }
}
