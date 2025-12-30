using MealPrepPlanner.Core;

namespace MealPrepPlanner.Api.Features.GroceryLists;

public record GroceryListDto
{
    public Guid GroceryListId { get; init; }
    public Guid UserId { get; init; }
    public Guid? MealPlanId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Items { get; init; } = "[]";
    public DateTime? ShoppingDate { get; init; }
    public bool IsCompleted { get; init; }
    public decimal? EstimatedCost { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
    public bool IsScheduledForToday { get; init; }
}

public static class GroceryListExtensions
{
    public static GroceryListDto ToDto(this GroceryList groceryList)
    {
        return new GroceryListDto
        {
            GroceryListId = groceryList.GroceryListId,
            UserId = groceryList.UserId,
            MealPlanId = groceryList.MealPlanId,
            Name = groceryList.Name,
            Items = groceryList.Items,
            ShoppingDate = groceryList.ShoppingDate,
            IsCompleted = groceryList.IsCompleted,
            EstimatedCost = groceryList.EstimatedCost,
            Notes = groceryList.Notes,
            CreatedAt = groceryList.CreatedAt,
            IsScheduledForToday = groceryList.IsScheduledForToday(),
        };
    }
}
