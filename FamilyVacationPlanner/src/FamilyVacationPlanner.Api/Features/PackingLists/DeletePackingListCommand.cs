using FamilyVacationPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyVacationPlanner.Api.Features.PackingLists;

public record DeletePackingListCommand : IRequest<bool>
{
    public Guid PackingListId { get; init; }
}

public class DeletePackingListCommandHandler : IRequestHandler<DeletePackingListCommand, bool>
{
    private readonly IFamilyVacationPlannerContext _context;
    private readonly ILogger<DeletePackingListCommandHandler> _logger;

    public DeletePackingListCommandHandler(
        IFamilyVacationPlannerContext context,
        ILogger<DeletePackingListCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeletePackingListCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting packing list item {PackingListId}", request.PackingListId);

        var packingList = await _context.PackingLists
            .FirstOrDefaultAsync(pl => pl.PackingListId == request.PackingListId, cancellationToken);

        if (packingList == null)
        {
            _logger.LogWarning("Packing list item {PackingListId} not found", request.PackingListId);
            return false;
        }

        _context.PackingLists.Remove(packingList);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted packing list item {PackingListId}", request.PackingListId);

        return true;
    }
}
