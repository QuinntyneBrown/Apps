using WineCellarInventory.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace WineCellarInventory.Api.Features.Wines;

public record CreateWineCommand : IRequest<WineDto>
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public WineType WineType { get; init; }
    public Region Region { get; init; }
    public int? Vintage { get; init; }
    public string? Producer { get; init; }
    public decimal? PurchasePrice { get; init; }
    public int BottleCount { get; init; } = 1;
    public string? StorageLocation { get; init; }
    public string? Notes { get; init; }
}

public class CreateWineCommandHandler : IRequestHandler<CreateWineCommand, WineDto>
{
    private readonly IWineCellarInventoryContext _context;
    private readonly ILogger<CreateWineCommandHandler> _logger;

    public CreateWineCommandHandler(
        IWineCellarInventoryContext context,
        ILogger<CreateWineCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<WineDto> Handle(CreateWineCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating wine for user {UserId}, name: {Name}",
            request.UserId,
            request.Name);

        var wine = new Wine
        {
            WineId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            WineType = request.WineType,
            Region = request.Region,
            Vintage = request.Vintage,
            Producer = request.Producer,
            PurchasePrice = request.PurchasePrice,
            BottleCount = request.BottleCount,
            StorageLocation = request.StorageLocation,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Wines.Add(wine);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created wine {WineId} for user {UserId}",
            wine.WineId,
            request.UserId);

        return wine.ToDto();
    }
}
