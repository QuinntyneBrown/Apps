using Receipts.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Receipts.Api.Features;

public record GetReceiptByIdQuery(Guid ReceiptId) : IRequest<ReceiptDto?>;

public class GetReceiptByIdQueryHandler : IRequestHandler<GetReceiptByIdQuery, ReceiptDto?>
{
    private readonly IReceiptsDbContext _context;

    public GetReceiptByIdQueryHandler(IReceiptsDbContext context)
    {
        _context = context;
    }

    public async Task<ReceiptDto?> Handle(GetReceiptByIdQuery request, CancellationToken cancellationToken)
    {
        var receipt = await _context.Receipts
            .FirstOrDefaultAsync(r => r.ReceiptId == request.ReceiptId, cancellationToken);
        return receipt?.ToDto();
    }
}
