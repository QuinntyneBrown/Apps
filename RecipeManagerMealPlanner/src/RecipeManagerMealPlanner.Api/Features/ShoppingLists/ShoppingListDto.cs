using RecipeManagerMealPlanner.Core;

namespace RecipeManagerMealPlanner.Api.Features.ShoppingLists;

public record ShoppingListDto
{
    public Guid ShoppingListId { get; init; }
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Items { get; init; } = string.Empty;
    public DateTime CreatedDate { get; init; }
    public bool IsCompleted { get; init; }
    public DateTime? CompletedDate { get; init; }
    public string? Notes { get; init; }
    public DateTime CreatedAt { get; init; }
}

public static class ShoppingListExtensions
{
    public static ShoppingListDto ToDto(this ShoppingList shoppingList)
    {
        return new ShoppingListDto
        {
            ShoppingListId = shoppingList.ShoppingListId,
            UserId = shoppingList.UserId,
            Name = shoppingList.Name,
            Items = shoppingList.Items,
            CreatedDate = shoppingList.CreatedDate,
            IsCompleted = shoppingList.IsCompleted,
            CompletedDate = shoppingList.CompletedDate,
            Notes = shoppingList.Notes,
            CreatedAt = shoppingList.CreatedAt,
        };
    }
}
