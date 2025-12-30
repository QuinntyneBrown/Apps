using TravelDestinationWishlist.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TravelDestinationWishlist.Api.Features.Memories;

public record GetMemoriesQuery : IRequest<IEnumerable<MemoryDto>>
{
    public Guid? UserId { get; init; }
    public Guid? TripId { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
}

public class GetMemoriesQueryHandler : IRequestHandler<GetMemoriesQuery, IEnumerable<MemoryDto>>
{
    private readonly ITravelDestinationWishlistContext _context;
    private readonly ILogger<GetMemoriesQueryHandler> _logger;

    public GetMemoriesQueryHandler(
        ITravelDestinationWishlistContext context,
        ILogger<GetMemoriesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<MemoryDto>> Handle(GetMemoriesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting memories for user {UserId}", request.UserId);

        var query = _context.Memories.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(m => m.UserId == request.UserId.Value);
        }

        if (request.TripId.HasValue)
        {
            query = query.Where(m => m.TripId == request.TripId.Value);
        }

        if (request.StartDate.HasValue)
        {
            query = query.Where(m => m.MemoryDate >= request.StartDate.Value);
        }

        if (request.EndDate.HasValue)
        {
            query = query.Where(m => m.MemoryDate <= request.EndDate.Value);
        }

        var memories = await query
            .OrderByDescending(m => m.MemoryDate)
            .ToListAsync(cancellationToken);

        return memories.Select(m => m.ToDto());
    }
}
