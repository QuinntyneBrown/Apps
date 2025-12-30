using MediatR;
using Microsoft.EntityFrameworkCore;
using WarrantyReturnPeriodTracker.Core;

namespace WarrantyReturnPeriodTracker.Api.Features.Receipts;

public class GetReceiptsQuery : IRequest<List<ReceiptDto>>
{
}

public class GetReceiptsQueryHandler : IRequestHandler<GetReceiptsQuery, List<ReceiptDto>>
{
    private readonly IWarrantyReturnPeriodTrackerContext _context;

    public GetReceiptsQueryHandler(IWarrantyReturnPeriodTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<ReceiptDto>> Handle(GetReceiptsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Receipts
            .AsNoTracking()
            .OrderByDescending(r => r.UploadedAt)
            .Select(r => r.ToDto())
            .ToListAsync(cancellationToken);
    }
}
