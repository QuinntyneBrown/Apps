using MediatR;
using Microsoft.EntityFrameworkCore;
using WarrantyReturnPeriodTracker.Core;

namespace WarrantyReturnPeriodTracker.Api.Features.Purchases;

public class GetPurchaseByIdQuery : IRequest<PurchaseDto?>
{
    public Guid PurchaseId { get; set; }
}

public class GetPurchaseByIdQueryHandler : IRequestHandler<GetPurchaseByIdQuery, PurchaseDto?>
{
    private readonly IWarrantyReturnPeriodTrackerContext _context;

    public GetPurchaseByIdQueryHandler(IWarrantyReturnPeriodTrackerContext context)
    {
        _context = context;
    }

    public async Task<PurchaseDto?> Handle(GetPurchaseByIdQuery request, CancellationToken cancellationToken)
    {
        var purchase = await _context.Purchases
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.PurchaseId == request.PurchaseId, cancellationToken);

        return purchase?.ToDto();
    }
}
