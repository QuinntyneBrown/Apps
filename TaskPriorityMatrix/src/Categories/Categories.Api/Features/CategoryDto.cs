using Categories.Core.Models;

namespace Categories.Api.Features;

public record CategoryDto
{
    public Guid CategoryId { get; init; }
    public Guid TenantId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Color { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
}

public static class CategoryDtoExtensions
{
    public static CategoryDto ToDto(this Category category) => new()
    {
        CategoryId = category.CategoryId,
        TenantId = category.TenantId,
        Name = category.Name,
        Color = category.Color,
        CreatedAt = category.CreatedAt
    };
}
