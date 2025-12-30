using WineCellarInventory.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WineCellarInventory.Api.Features.Wines;

public record DeleteWineCommand : IRequest<bool>
{
    public Guid WineId { get; init; }
}

public class DeleteWineCommandHandler : IRequestHandler<DeleteWineCommand, bool>
{
    private readonly IWineCellarInventoryContext _context;
    private readonly ILogger<DeleteWineCommandHandler> _logger;

    public DeleteWineCommandHandler(
        IWineCellarInventoryContext context,
        ILogger<DeleteWineCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteWineCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting wine {WineId}", request.WineId);

        var wine = await _context.Wines
            .FirstOrDefaultAsync(w => w.WineId == request.WineId, cancellationToken);

        if (wine == null)
        {
            _logger.LogWarning("Wine {WineId} not found", request.WineId);
            return false;
        }

        _context.Wines.Remove(wine);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted wine {WineId}", request.WineId);

        return true;
    }
}
