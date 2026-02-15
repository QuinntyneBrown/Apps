using Businesses.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Businesses.Api.Features;

public record DeleteBusinessCommand(Guid BusinessId, Guid TenantId) : IRequest<bool>;

public class DeleteBusinessCommandHandler : IRequestHandler<DeleteBusinessCommand, bool>
{
    private readonly IBusinessesDbContext _context;

    public DeleteBusinessCommandHandler(IBusinessesDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteBusinessCommand request, CancellationToken cancellationToken)
    {
        var business = await _context.Businesses
            .FirstOrDefaultAsync(b => b.BusinessId == request.BusinessId && b.TenantId == request.TenantId, cancellationToken);

        if (business == null) return false;

        _context.Businesses.Remove(business);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
