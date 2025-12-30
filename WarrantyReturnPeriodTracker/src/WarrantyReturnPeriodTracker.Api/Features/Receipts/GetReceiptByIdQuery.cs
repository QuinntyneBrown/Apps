using MediatR;
using Microsoft.EntityFrameworkCore;
using WarrantyReturnPeriodTracker.Core;

namespace WarrantyReturnPeriodTracker.Api.Features.Receipts;

public class GetReceiptByIdQuery : IRequest<ReceiptDto?>
{
    public Guid ReceiptId { get; set; }
}

public class GetReceiptByIdQueryHandler : IRequestHandler<GetReceiptByIdQuery, ReceiptDto?>
{
    private readonly IWarrantyReturnPeriodTrackerContext _context;

    public GetReceiptByIdQueryHandler(IWarrantyReturnPeriodTrackerContext context)
    {
        _context = context;
    }

    public async Task<ReceiptDto?> Handle(GetReceiptByIdQuery request, CancellationToken cancellationToken)
    {
        var receipt = await _context.Receipts
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.ReceiptId == request.ReceiptId, cancellationToken);

        return receipt?.ToDto();
    }
}
