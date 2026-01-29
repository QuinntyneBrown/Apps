using Warranties.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Warranties.Api.Features;

public record DeleteWarrantyCommand(Guid WarrantyId) : IRequest<bool>;

public class DeleteWarrantyCommandHandler : IRequestHandler<DeleteWarrantyCommand, bool>
{
    private readonly IWarrantiesDbContext _context;

    public DeleteWarrantyCommandHandler(IWarrantiesDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteWarrantyCommand request, CancellationToken cancellationToken)
    {
        var warranty = await _context.Warranties
            .FirstOrDefaultAsync(w => w.WarrantyId == request.WarrantyId, cancellationToken);

        if (warranty == null) return false;

        _context.Warranties.Remove(warranty);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
