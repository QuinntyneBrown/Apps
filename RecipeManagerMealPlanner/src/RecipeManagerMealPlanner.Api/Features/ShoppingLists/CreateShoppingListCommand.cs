using RecipeManagerMealPlanner.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace RecipeManagerMealPlanner.Api.Features.ShoppingLists;

public record CreateShoppingListCommand : IRequest<ShoppingListDto>
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Items { get; init; } = string.Empty;
    public DateTime CreatedDate { get; init; }
    public string? Notes { get; init; }
}

public class CreateShoppingListCommandHandler : IRequestHandler<CreateShoppingListCommand, ShoppingListDto>
{
    private readonly IRecipeManagerMealPlannerContext _context;
    private readonly ILogger<CreateShoppingListCommandHandler> _logger;

    public CreateShoppingListCommandHandler(
        IRecipeManagerMealPlannerContext context,
        ILogger<CreateShoppingListCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ShoppingListDto> Handle(CreateShoppingListCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating shopping list for user {UserId}, name: {Name}",
            request.UserId,
            request.Name);

        var shoppingList = new ShoppingList
        {
            ShoppingListId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            Items = request.Items,
            CreatedDate = request.CreatedDate,
            IsCompleted = false,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        _context.ShoppingLists.Add(shoppingList);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created shopping list {ShoppingListId} for user {UserId}",
            shoppingList.ShoppingListId,
            request.UserId);

        return shoppingList.ToDto();
    }
}
