using PackingLists.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PackingLists.Api.Features;

public record GetPackingListByIdQuery(Guid PackingListId) : IRequest<PackingListDto?>;

public class GetPackingListByIdQueryHandler : IRequestHandler<GetPackingListByIdQuery, PackingListDto?>
{
    private readonly IPackingListsDbContext _context;

    public GetPackingListByIdQueryHandler(IPackingListsDbContext context)
    {
        _context = context;
    }

    public async Task<PackingListDto?> Handle(GetPackingListByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _context.PackingLists
            .FirstOrDefaultAsync(p => p.PackingListId == request.PackingListId, cancellationToken);

        if (item == null) return null;

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
