using Manuals.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Manuals.Api.Features;

public record DeleteManualCommand(Guid ManualId) : IRequest<bool>;

public class DeleteManualCommandHandler : IRequestHandler<DeleteManualCommand, bool>
{
    private readonly IManualsDbContext _context;

    public DeleteManualCommandHandler(IManualsDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteManualCommand request, CancellationToken cancellationToken)
    {
        var manual = await _context.Manuals
            .FirstOrDefaultAsync(m => m.ManualId == request.ManualId, cancellationToken);

        if (manual == null) return false;

        _context.Manuals.Remove(manual);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
