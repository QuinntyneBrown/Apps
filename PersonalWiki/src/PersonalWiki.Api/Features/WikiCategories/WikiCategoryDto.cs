using PersonalWiki.Core;

namespace PersonalWiki.Api.Features.WikiCategories;

public record WikiCategoryDto
{
    public Guid WikiCategoryId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public Guid? ParentCategoryId { get; init; }
    public string? Icon { get; init; }
    public DateTime CreatedAt { get; init; }
    public int PageCount { get; init; }
}

public static class WikiCategoryExtensions
{
    public static WikiCategoryDto ToDto(this WikiCategory category)
    {
        return new WikiCategoryDto
        {
            WikiCategoryId = category.WikiCategoryId,
            UserId = category.UserId,
            Name = category.Name,
            Description = category.Description,
            ParentCategoryId = category.ParentCategoryId,
            Icon = category.Icon,
            CreatedAt = category.CreatedAt,
            PageCount = category.GetPageCount(),
        };
    }
}
