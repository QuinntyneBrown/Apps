using WineCellarInventory.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WineCellarInventory.Api.Features.Wines;

public record UpdateWineCommand : IRequest<WineDto?>
{
    public Guid WineId { get; init; }
    public string Name { get; init; } = string.Empty;
    public WineType WineType { get; init; }
    public Region Region { get; init; }
    public int? Vintage { get; init; }
    public string? Producer { get; init; }
    public decimal? PurchasePrice { get; init; }
    public int BottleCount { get; init; }
    public string? StorageLocation { get; init; }
    public string? Notes { get; init; }
}

public class UpdateWineCommandHandler : IRequestHandler<UpdateWineCommand, WineDto?>
{
    private readonly IWineCellarInventoryContext _context;
    private readonly ILogger<UpdateWineCommandHandler> _logger;

    public UpdateWineCommandHandler(
        IWineCellarInventoryContext context,
        ILogger<UpdateWineCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<WineDto?> Handle(UpdateWineCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating wine {WineId}", request.WineId);

        var wine = await _context.Wines
            .FirstOrDefaultAsync(w => w.WineId == request.WineId, cancellationToken);

        if (wine == null)
        {
            _logger.LogWarning("Wine {WineId} not found", request.WineId);
            return null;
        }

        wine.Name = request.Name;
        wine.WineType = request.WineType;
        wine.Region = request.Region;
        wine.Vintage = request.Vintage;
        wine.Producer = request.Producer;
        wine.PurchasePrice = request.PurchasePrice;
        wine.BottleCount = request.BottleCount;
        wine.StorageLocation = request.StorageLocation;
        wine.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated wine {WineId}", request.WineId);

        return wine.ToDto();
    }
}
