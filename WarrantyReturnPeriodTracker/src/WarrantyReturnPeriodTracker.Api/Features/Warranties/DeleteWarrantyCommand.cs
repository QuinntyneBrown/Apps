using MediatR;
using Microsoft.EntityFrameworkCore;
using WarrantyReturnPeriodTracker.Core;

namespace WarrantyReturnPeriodTracker.Api.Features.Warranties;

public class DeleteWarrantyCommand : IRequest<Unit>
{
    public Guid WarrantyId { get; set; }
}

public class DeleteWarrantyCommandHandler : IRequestHandler<DeleteWarrantyCommand, Unit>
{
    private readonly IWarrantyReturnPeriodTrackerContext _context;

    public DeleteWarrantyCommandHandler(IWarrantyReturnPeriodTrackerContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteWarrantyCommand request, CancellationToken cancellationToken)
    {
        var warranty = await _context.Warranties
            .FirstOrDefaultAsync(w => w.WarrantyId == request.WarrantyId, cancellationToken);

        if (warranty == null)
        {
            throw new InvalidOperationException($"Warranty with ID {request.WarrantyId} not found.");
        }

        _context.Warranties.Remove(warranty);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
