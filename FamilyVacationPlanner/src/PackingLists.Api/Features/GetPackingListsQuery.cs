using PackingLists.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PackingLists.Api.Features;

public record GetPackingListsQuery : IRequest<IEnumerable<PackingListDto>>;

public class GetPackingListsQueryHandler : IRequestHandler<GetPackingListsQuery, IEnumerable<PackingListDto>>
{
    private readonly IPackingListsDbContext _context;

    public GetPackingListsQueryHandler(IPackingListsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PackingListDto>> Handle(GetPackingListsQuery request, CancellationToken cancellationToken)
    {
        return await _context.PackingLists
            .Select(p => new PackingListDto
            {
                PackingListId = p.PackingListId,
                TripId = p.TripId,
                ItemName = p.ItemName,
                IsPacked = p.IsPacked,
                CreatedAt = p.CreatedAt
            })
            .ToListAsync(cancellationToken);
    }
}
