using GiftIdeaTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GiftIdeaTracker.Api.Features.Purchases;

public record UpdatePurchaseCommand : IRequest<PurchaseDto?>
{
    public Guid PurchaseId { get; init; }
    public DateTime PurchaseDate { get; init; }
    public decimal ActualPrice { get; init; }
    public string? Store { get; init; }
}

public class UpdatePurchaseCommandHandler : IRequestHandler<UpdatePurchaseCommand, PurchaseDto?>
{
    private readonly IGiftIdeaTrackerContext _context;
    private readonly ILogger<UpdatePurchaseCommandHandler> _logger;

    public UpdatePurchaseCommandHandler(
        IGiftIdeaTrackerContext context,
        ILogger<UpdatePurchaseCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PurchaseDto?> Handle(UpdatePurchaseCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating purchase {PurchaseId}", request.PurchaseId);

        var purchase = await _context.Purchases
            .FirstOrDefaultAsync(p => p.PurchaseId == request.PurchaseId, cancellationToken);

        if (purchase == null)
        {
            _logger.LogWarning("Purchase {PurchaseId} not found", request.PurchaseId);
            return null;
        }

        purchase.PurchaseDate = request.PurchaseDate;
        purchase.ActualPrice = request.ActualPrice;
        purchase.Store = request.Store;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated purchase {PurchaseId}", request.PurchaseId);

        return purchase.ToDto();
    }
}
