using FamilyVacationPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyVacationPlanner.Api.Features.PackingLists;

public record GetPackingListByIdQuery : IRequest<PackingListDto?>
{
    public Guid PackingListId { get; init; }
}

public class GetPackingListByIdQueryHandler : IRequestHandler<GetPackingListByIdQuery, PackingListDto?>
{
    private readonly IFamilyVacationPlannerContext _context;
    private readonly ILogger<GetPackingListByIdQueryHandler> _logger;

    public GetPackingListByIdQueryHandler(
        IFamilyVacationPlannerContext context,
        ILogger<GetPackingListByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PackingListDto?> Handle(GetPackingListByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting packing list item {PackingListId}", request.PackingListId);

        var packingList = await _context.PackingLists
            .AsNoTracking()
            .FirstOrDefaultAsync(pl => pl.PackingListId == request.PackingListId, cancellationToken);

        if (packingList == null)
        {
            _logger.LogWarning("Packing list item {PackingListId} not found", request.PackingListId);
            return null;
        }

        return packingList.ToDto();
    }
}
