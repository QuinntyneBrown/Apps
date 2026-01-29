using PackingLists.Core;
using PackingLists.Core.Models;
using MediatR;

namespace PackingLists.Api.Features;

public record CreatePackingListCommand : IRequest<PackingListDto>
{
    public Guid TenantId { get; init; }
    public Guid TripId { get; init; }
    public string ItemName { get; init; } = string.Empty;
}

public class CreatePackingListCommandHandler : IRequestHandler<CreatePackingListCommand, PackingListDto>
{
    private readonly IPackingListsDbContext _context;

    public CreatePackingListCommandHandler(IPackingListsDbContext context)
    {
        _context = context;
    }

    public async Task<PackingListDto> Handle(CreatePackingListCommand request, CancellationToken cancellationToken)
    {
        var item = new PackingList
        {
            PackingListId = Guid.NewGuid(),
            TenantId = request.TenantId,
            TripId = request.TripId,
            ItemName = request.ItemName,
            IsPacked = false,
            CreatedAt = DateTime.UtcNow
        };

        _context.PackingLists.Add(item);
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
