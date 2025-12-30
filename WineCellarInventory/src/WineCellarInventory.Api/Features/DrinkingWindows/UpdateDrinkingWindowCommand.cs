using WineCellarInventory.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WineCellarInventory.Api.Features.DrinkingWindows;

public record UpdateDrinkingWindowCommand : IRequest<DrinkingWindowDto?>
{
    public Guid DrinkingWindowId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public string? Notes { get; init; }
}

public class UpdateDrinkingWindowCommandHandler : IRequestHandler<UpdateDrinkingWindowCommand, DrinkingWindowDto?>
{
    private readonly IWineCellarInventoryContext _context;
    private readonly ILogger<UpdateDrinkingWindowCommandHandler> _logger;

    public UpdateDrinkingWindowCommandHandler(
        IWineCellarInventoryContext context,
        ILogger<UpdateDrinkingWindowCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<DrinkingWindowDto?> Handle(UpdateDrinkingWindowCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating drinking window {DrinkingWindowId}", request.DrinkingWindowId);

        var drinkingWindow = await _context.DrinkingWindows
            .FirstOrDefaultAsync(d => d.DrinkingWindowId == request.DrinkingWindowId, cancellationToken);

        if (drinkingWindow == null)
        {
            _logger.LogWarning("Drinking window {DrinkingWindowId} not found", request.DrinkingWindowId);
            return null;
        }

        drinkingWindow.StartDate = request.StartDate;
        drinkingWindow.EndDate = request.EndDate;
        drinkingWindow.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated drinking window {DrinkingWindowId}", request.DrinkingWindowId);

        return drinkingWindow.ToDto();
    }
}
