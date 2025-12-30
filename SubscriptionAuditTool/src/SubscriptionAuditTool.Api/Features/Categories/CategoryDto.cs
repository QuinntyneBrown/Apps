using SubscriptionAuditTool.Core;

namespace SubscriptionAuditTool.Api.Features.Categories;

public record CategoryDto
{
    public Guid CategoryId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string? ColorCode { get; init; }
    public decimal TotalMonthlyCost { get; init; }
    public int SubscriptionCount { get; init; }
}

public static class CategoryExtensions
{
    public static CategoryDto ToDto(this Category category)
    {
        return new CategoryDto
        {
            CategoryId = category.CategoryId,
            Name = category.Name,
            Description = category.Description,
            ColorCode = category.ColorCode,
            TotalMonthlyCost = category.CalculateTotalMonthlyCost(),
            SubscriptionCount = category.Subscriptions.Count,
        };
    }
}
