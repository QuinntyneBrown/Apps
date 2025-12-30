using TravelDestinationWishlist.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TravelDestinationWishlist.Api.Features.Memories;

public record GetMemoryByIdQuery : IRequest<MemoryDto?>
{
    public Guid MemoryId { get; init; }
}

public class GetMemoryByIdQueryHandler : IRequestHandler<GetMemoryByIdQuery, MemoryDto?>
{
    private readonly ITravelDestinationWishlistContext _context;
    private readonly ILogger<GetMemoryByIdQueryHandler> _logger;

    public GetMemoryByIdQueryHandler(
        ITravelDestinationWishlistContext context,
        ILogger<GetMemoryByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MemoryDto?> Handle(GetMemoryByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting memory {MemoryId}", request.MemoryId);

        var memory = await _context.Memories
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.MemoryId == request.MemoryId, cancellationToken);

        return memory?.ToDto();
    }
}
