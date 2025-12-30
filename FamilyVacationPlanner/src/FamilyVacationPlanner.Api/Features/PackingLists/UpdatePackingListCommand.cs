using FamilyVacationPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyVacationPlanner.Api.Features.PackingLists;

public record UpdatePackingListCommand : IRequest<PackingListDto?>
{
    public Guid PackingListId { get; init; }
    public string ItemName { get; init; } = string.Empty;
    public bool IsPacked { get; init; }
}

public class UpdatePackingListCommandHandler : IRequestHandler<UpdatePackingListCommand, PackingListDto?>
{
    private readonly IFamilyVacationPlannerContext _context;
    private readonly ILogger<UpdatePackingListCommandHandler> _logger;

    public UpdatePackingListCommandHandler(
        IFamilyVacationPlannerContext context,
        ILogger<UpdatePackingListCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PackingListDto?> Handle(UpdatePackingListCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating packing list item {PackingListId}", request.PackingListId);

        var packingList = await _context.PackingLists
            .FirstOrDefaultAsync(pl => pl.PackingListId == request.PackingListId, cancellationToken);

        if (packingList == null)
        {
            _logger.LogWarning("Packing list item {PackingListId} not found", request.PackingListId);
            return null;
        }

        packingList.ItemName = request.ItemName;
        packingList.IsPacked = request.IsPacked;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated packing list item {PackingListId}", request.PackingListId);

        return packingList.ToDto();
    }
}
