using Receipts.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Receipts.Api.Features;

public record DeleteReceiptCommand(Guid ReceiptId) : IRequest<bool>;

public class DeleteReceiptCommandHandler : IRequestHandler<DeleteReceiptCommand, bool>
{
    private readonly IReceiptsDbContext _context;
    private readonly ILogger<DeleteReceiptCommandHandler> _logger;

    public DeleteReceiptCommandHandler(IReceiptsDbContext context, ILogger<DeleteReceiptCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteReceiptCommand request, CancellationToken cancellationToken)
    {
        var receipt = await _context.Receipts
            .FirstOrDefaultAsync(r => r.ReceiptId == request.ReceiptId, cancellationToken);

        if (receipt == null) return false;

        _context.Receipts.Remove(receipt);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Receipt deleted: {ReceiptId}", request.ReceiptId);

        return true;
    }
}
