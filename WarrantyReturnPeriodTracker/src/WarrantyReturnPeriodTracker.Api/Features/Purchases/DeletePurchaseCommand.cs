using MediatR;
using Microsoft.EntityFrameworkCore;
using WarrantyReturnPeriodTracker.Core;

namespace WarrantyReturnPeriodTracker.Api.Features.Purchases;

public class DeletePurchaseCommand : IRequest<Unit>
{
    public Guid PurchaseId { get; set; }
}

public class DeletePurchaseCommandHandler : IRequestHandler<DeletePurchaseCommand, Unit>
{
    private readonly IWarrantyReturnPeriodTrackerContext _context;

    public DeletePurchaseCommandHandler(IWarrantyReturnPeriodTrackerContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeletePurchaseCommand request, CancellationToken cancellationToken)
    {
        var purchase = await _context.Purchases
            .FirstOrDefaultAsync(p => p.PurchaseId == request.PurchaseId, cancellationToken);

        if (purchase == null)
        {
            throw new InvalidOperationException($"Purchase with ID {request.PurchaseId} not found.");
        }

        _context.Purchases.Remove(purchase);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
