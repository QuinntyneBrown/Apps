using Receipts.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Receipts.Api.Features;

public record GetReceiptsQuery : IRequest<IEnumerable<ReceiptDto>>;

public class GetReceiptsQueryHandler : IRequestHandler<GetReceiptsQuery, IEnumerable<ReceiptDto>>
{
    private readonly IReceiptsDbContext _context;

    public GetReceiptsQueryHandler(IReceiptsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ReceiptDto>> Handle(GetReceiptsQuery request, CancellationToken cancellationToken)
    {
        var receipts = await _context.Receipts.ToListAsync(cancellationToken);
        return receipts.Select(r => r.ToDto());
    }
}
