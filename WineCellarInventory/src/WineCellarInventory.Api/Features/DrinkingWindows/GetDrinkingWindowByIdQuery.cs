using WineCellarInventory.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WineCellarInventory.Api.Features.DrinkingWindows;

public record GetDrinkingWindowByIdQuery : IRequest<DrinkingWindowDto?>
{
    public Guid DrinkingWindowId { get; init; }
}

public class GetDrinkingWindowByIdQueryHandler : IRequestHandler<GetDrinkingWindowByIdQuery, DrinkingWindowDto?>
{
    private readonly IWineCellarInventoryContext _context;
    private readonly ILogger<GetDrinkingWindowByIdQueryHandler> _logger;

    public GetDrinkingWindowByIdQueryHandler(
        IWineCellarInventoryContext context,
        ILogger<GetDrinkingWindowByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<DrinkingWindowDto?> Handle(GetDrinkingWindowByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting drinking window {DrinkingWindowId}", request.DrinkingWindowId);

        var drinkingWindow = await _context.DrinkingWindows
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.DrinkingWindowId == request.DrinkingWindowId, cancellationToken);

        if (drinkingWindow == null)
        {
            _logger.LogWarning("Drinking window {DrinkingWindowId} not found", request.DrinkingWindowId);
            return null;
        }

        return drinkingWindow.ToDto();
    }
}
