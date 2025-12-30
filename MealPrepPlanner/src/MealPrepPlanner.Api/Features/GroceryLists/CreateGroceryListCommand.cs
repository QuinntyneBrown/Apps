using MealPrepPlanner.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MealPrepPlanner.Api.Features.GroceryLists;

public record CreateGroceryListCommand : IRequest<GroceryListDto>
{
    public Guid UserId { get; init; }
    public Guid? MealPlanId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Items { get; init; } = "[]";
    public DateTime? ShoppingDate { get; init; }
    public decimal? EstimatedCost { get; init; }
    public string? Notes { get; init; }
}

public class CreateGroceryListCommandHandler : IRequestHandler<CreateGroceryListCommand, GroceryListDto>
{
    private readonly IMealPrepPlannerContext _context;
    private readonly ILogger<CreateGroceryListCommandHandler> _logger;

    public CreateGroceryListCommandHandler(
        IMealPrepPlannerContext context,
        ILogger<CreateGroceryListCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<GroceryListDto> Handle(CreateGroceryListCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating grocery list for user {UserId}, name: {Name}",
            request.UserId,
            request.Name);

        var groceryList = new GroceryList
        {
            GroceryListId = Guid.NewGuid(),
            UserId = request.UserId,
            MealPlanId = request.MealPlanId,
            Name = request.Name,
            Items = request.Items,
            ShoppingDate = request.ShoppingDate,
            IsCompleted = false,
            EstimatedCost = request.EstimatedCost,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        _context.GroceryLists.Add(groceryList);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created grocery list {GroceryListId} for user {UserId}",
            groceryList.GroceryListId,
            request.UserId);

        return groceryList.ToDto();
    }
}
