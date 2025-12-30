using KnowledgeBaseSecondBrain.Core;

namespace KnowledgeBaseSecondBrain.Api.Features.SearchQueries;

public record SearchQueryDto
{
    public Guid SearchQueryId { get; init; }
    public Guid UserId { get; init; }
    public string QueryText { get; init; } = string.Empty;
    public string? Name { get; init; }
    public bool IsSaved { get; init; }
    public int ExecutionCount { get; init; }
    public DateTime? LastExecutedAt { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class SearchQueryExtensions
{
    public static SearchQueryDto ToDto(this SearchQuery searchQuery)
    {
        return new SearchQueryDto
        {
            SearchQueryId = searchQuery.SearchQueryId,
            UserId = searchQuery.UserId,
            QueryText = searchQuery.QueryText,
            Name = searchQuery.Name,
            IsSaved = searchQuery.IsSaved,
            ExecutionCount = searchQuery.ExecutionCount,
            LastExecutedAt = searchQuery.LastExecutedAt,
            CreatedAt = searchQuery.CreatedAt,
        };
    }
}
