using PackingLists.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PackingLists.Api.Features;

public record DeletePackingListCommand(Guid PackingListId) : IRequest<bool>;

public class DeletePackingListCommandHandler : IRequestHandler<DeletePackingListCommand, bool>
{
    private readonly IPackingListsDbContext _context;

    public DeletePackingListCommandHandler(IPackingListsDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeletePackingListCommand request, CancellationToken cancellationToken)
    {
        var item = await _context.PackingLists
            .FirstOrDefaultAsync(p => p.PackingListId == request.PackingListId, cancellationToken);

        if (item == null) return false;

        _context.PackingLists.Remove(item);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
