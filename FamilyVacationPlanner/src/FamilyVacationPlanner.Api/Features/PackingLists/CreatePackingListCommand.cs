using FamilyVacationPlanner.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FamilyVacationPlanner.Api.Features.PackingLists;

public record CreatePackingListCommand : IRequest<PackingListDto>
{
    public Guid TripId { get; init; }
    public string ItemName { get; init; } = string.Empty;
    public bool IsPacked { get; init; }
}

public class CreatePackingListCommandHandler : IRequestHandler<CreatePackingListCommand, PackingListDto>
{
    private readonly IFamilyVacationPlannerContext _context;
    private readonly ILogger<CreatePackingListCommandHandler> _logger;

    public CreatePackingListCommandHandler(
        IFamilyVacationPlannerContext context,
        ILogger<CreatePackingListCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PackingListDto> Handle(CreatePackingListCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating packing list item for trip {TripId}, item: {ItemName}",
            request.TripId,
            request.ItemName);

        var packingList = new PackingList
        {
            PackingListId = Guid.NewGuid(),
            TripId = request.TripId,
            ItemName = request.ItemName,
            IsPacked = request.IsPacked,
            CreatedAt = DateTime.UtcNow,
        };

        _context.PackingLists.Add(packingList);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created packing list item {PackingListId} for trip {TripId}",
            packingList.PackingListId,
            request.TripId);

        return packingList.ToDto();
    }
}
