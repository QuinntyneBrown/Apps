using MediatR;
using Microsoft.EntityFrameworkCore;
using WarrantyReturnPeriodTracker.Core;

namespace WarrantyReturnPeriodTracker.Api.Features.ReturnWindows;

public class DeleteReturnWindowCommand : IRequest<Unit>
{
    public Guid ReturnWindowId { get; set; }
}

public class DeleteReturnWindowCommandHandler : IRequestHandler<DeleteReturnWindowCommand, Unit>
{
    private readonly IWarrantyReturnPeriodTrackerContext _context;

    public DeleteReturnWindowCommandHandler(IWarrantyReturnPeriodTrackerContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteReturnWindowCommand request, CancellationToken cancellationToken)
    {
        var returnWindow = await _context.ReturnWindows
            .FirstOrDefaultAsync(rw => rw.ReturnWindowId == request.ReturnWindowId, cancellationToken);

        if (returnWindow == null)
        {
            throw new InvalidOperationException($"Return window with ID {request.ReturnWindowId} not found.");
        }

        _context.ReturnWindows.Remove(returnWindow);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
