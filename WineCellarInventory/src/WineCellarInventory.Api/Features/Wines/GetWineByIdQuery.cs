using WineCellarInventory.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WineCellarInventory.Api.Features.Wines;

public record GetWineByIdQuery : IRequest<WineDto?>
{
    public Guid WineId { get; init; }
}

public class GetWineByIdQueryHandler : IRequestHandler<GetWineByIdQuery, WineDto?>
{
    private readonly IWineCellarInventoryContext _context;
    private readonly ILogger<GetWineByIdQueryHandler> _logger;

    public GetWineByIdQueryHandler(
        IWineCellarInventoryContext context,
        ILogger<GetWineByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<WineDto?> Handle(GetWineByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting wine {WineId}", request.WineId);

        var wine = await _context.Wines
            .AsNoTracking()
            .FirstOrDefaultAsync(w => w.WineId == request.WineId, cancellationToken);

        if (wine == null)
        {
            _logger.LogWarning("Wine {WineId} not found", request.WineId);
            return null;
        }

        return wine.ToDto();
    }
}
