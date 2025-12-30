using FamilyVacationPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyVacationPlanner.Api.Features.PackingLists;

public record GetPackingListsQuery : IRequest<IEnumerable<PackingListDto>>
{
    public Guid? TripId { get; init; }
}

public class GetPackingListsQueryHandler : IRequestHandler<GetPackingListsQuery, IEnumerable<PackingListDto>>
{
    private readonly IFamilyVacationPlannerContext _context;
    private readonly ILogger<GetPackingListsQueryHandler> _logger;

    public GetPackingListsQueryHandler(
        IFamilyVacationPlannerContext context,
        ILogger<GetPackingListsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<PackingListDto>> Handle(GetPackingListsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting packing list items for trip {TripId}", request.TripId);

        var query = _context.PackingLists.AsNoTracking();

        if (request.TripId.HasValue)
        {
            query = query.Where(pl => pl.TripId == request.TripId.Value);
        }

        var packingLists = await query
            .OrderByDescending(pl => pl.CreatedAt)
            .ToListAsync(cancellationToken);

        return packingLists.Select(pl => pl.ToDto());
    }
}
