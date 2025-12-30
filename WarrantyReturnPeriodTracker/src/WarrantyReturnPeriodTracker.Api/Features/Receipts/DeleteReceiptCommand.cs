using MediatR;
using Microsoft.EntityFrameworkCore;
using WarrantyReturnPeriodTracker.Core;

namespace WarrantyReturnPeriodTracker.Api.Features.Receipts;

public class DeleteReceiptCommand : IRequest<Unit>
{
    public Guid ReceiptId { get; set; }
}

public class DeleteReceiptCommandHandler : IRequestHandler<DeleteReceiptCommand, Unit>
{
    private readonly IWarrantyReturnPeriodTrackerContext _context;

    public DeleteReceiptCommandHandler(IWarrantyReturnPeriodTrackerContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteReceiptCommand request, CancellationToken cancellationToken)
    {
        var receipt = await _context.Receipts
            .FirstOrDefaultAsync(r => r.ReceiptId == request.ReceiptId, cancellationToken);

        if (receipt == null)
        {
            throw new InvalidOperationException($"Receipt with ID {request.ReceiptId} not found.");
        }

        _context.Receipts.Remove(receipt);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
