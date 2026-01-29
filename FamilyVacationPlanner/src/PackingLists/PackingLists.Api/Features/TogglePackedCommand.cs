using PackingLists.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PackingLists.Api.Features;

public record TogglePackedCommand(Guid PackingListId) : IRequest<PackingListDto?>;

public class TogglePackedCommandHandler : IRequestHandler<TogglePackedCommand, PackingListDto?>
{
    private readonly IPackingListsDbContext _context;

    public TogglePackedCommandHandler(IPackingListsDbContext context)
    {
        _context = context;
    }

    public async Task<PackingListDto?> Handle(TogglePackedCommand request, CancellationToken cancellationToken)
    {
        var item = await _context.PackingLists
            .FirstOrDefaultAsync(p => p.PackingListId == request.PackingListId, cancellationToken);

        if (item == null) return null;

        item.IsPacked = !item.IsPacked;
        await _context.SaveChangesAsync(cancellationToken);

        return new PackingListDto
        {
            PackingListId = item.PackingListId,
            TripId = item.TripId,
            ItemName = item.ItemName,
            IsPacked = item.IsPacked,
            CreatedAt = item.CreatedAt
        };
    }
}
