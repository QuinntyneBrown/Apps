using WineCellarInventory.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace WineCellarInventory.Api.Features.DrinkingWindows;

public record CreateDrinkingWindowCommand : IRequest<DrinkingWindowDto>
{
    public Guid UserId { get; init; }
    public Guid WineId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public string? Notes { get; init; }
}

public class CreateDrinkingWindowCommandHandler : IRequestHandler<CreateDrinkingWindowCommand, DrinkingWindowDto>
{
    private readonly IWineCellarInventoryContext _context;
    private readonly ILogger<CreateDrinkingWindowCommandHandler> _logger;

    public CreateDrinkingWindowCommandHandler(
        IWineCellarInventoryContext context,
        ILogger<CreateDrinkingWindowCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<DrinkingWindowDto> Handle(CreateDrinkingWindowCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating drinking window for user {UserId}, wine: {WineId}",
            request.UserId,
            request.WineId);

        var drinkingWindow = new DrinkingWindow
        {
            DrinkingWindowId = Guid.NewGuid(),
            UserId = request.UserId,
            WineId = request.WineId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        _context.DrinkingWindows.Add(drinkingWindow);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created drinking window {DrinkingWindowId} for wine {WineId}",
            drinkingWindow.DrinkingWindowId,
            request.WineId);

        return drinkingWindow.ToDto();
    }
}
