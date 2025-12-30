using GiftIdeaTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GiftIdeaTracker.Api.Features.Purchases;

public record DeletePurchaseCommand : IRequest<bool>
{
    public Guid PurchaseId { get; init; }
}

public class DeletePurchaseCommandHandler : IRequestHandler<DeletePurchaseCommand, bool>
{
    private readonly IGiftIdeaTrackerContext _context;
    private readonly ILogger<DeletePurchaseCommandHandler> _logger;

    public DeletePurchaseCommandHandler(
        IGiftIdeaTrackerContext context,
        ILogger<DeletePurchaseCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeletePurchaseCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting purchase {PurchaseId}", request.PurchaseId);

        var purchase = await _context.Purchases
            .FirstOrDefaultAsync(p => p.PurchaseId == request.PurchaseId, cancellationToken);

        if (purchase == null)
        {
            _logger.LogWarning("Purchase {PurchaseId} not found", request.PurchaseId);
            return false;
        }

        _context.Purchases.Remove(purchase);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted purchase {PurchaseId}", request.PurchaseId);

        return true;
    }
}
