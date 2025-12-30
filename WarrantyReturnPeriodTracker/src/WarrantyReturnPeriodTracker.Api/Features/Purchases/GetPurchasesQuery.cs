using MediatR;
using Microsoft.EntityFrameworkCore;
using WarrantyReturnPeriodTracker.Core;

namespace WarrantyReturnPeriodTracker.Api.Features.Purchases;

public class GetPurchasesQuery : IRequest<List<PurchaseDto>>
{
}

public class GetPurchasesQueryHandler : IRequestHandler<GetPurchasesQuery, List<PurchaseDto>>
{
    private readonly IWarrantyReturnPeriodTrackerContext _context;

    public GetPurchasesQueryHandler(IWarrantyReturnPeriodTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<PurchaseDto>> Handle(GetPurchasesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Purchases
            .AsNoTracking()
            .OrderByDescending(p => p.CreatedAt)
            .Select(p => p.ToDto())
            .ToListAsync(cancellationToken);
    }
}
