using WineCellarInventory.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WineCellarInventory.Api.Features.DrinkingWindows;

public record DeleteDrinkingWindowCommand : IRequest<bool>
{
    public Guid DrinkingWindowId { get; init; }
}

public class DeleteDrinkingWindowCommandHandler : IRequestHandler<DeleteDrinkingWindowCommand, bool>
{
    private readonly IWineCellarInventoryContext _context;
    private readonly ILogger<DeleteDrinkingWindowCommandHandler> _logger;

    public DeleteDrinkingWindowCommandHandler(
        IWineCellarInventoryContext context,
        ILogger<DeleteDrinkingWindowCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteDrinkingWindowCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting drinking window {DrinkingWindowId}", request.DrinkingWindowId);

        var drinkingWindow = await _context.DrinkingWindows
            .FirstOrDefaultAsync(d => d.DrinkingWindowId == request.DrinkingWindowId, cancellationToken);

        if (drinkingWindow == null)
        {
            _logger.LogWarning("Drinking window {DrinkingWindowId} not found", request.DrinkingWindowId);
            return false;
        }

        _context.DrinkingWindows.Remove(drinkingWindow);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted drinking window {DrinkingWindowId}", request.DrinkingWindowId);

        return true;
    }
}
