using WineCellarInventory.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WineCellarInventory.Api.Features.Wines;

public record GetWinesQuery : IRequest<IEnumerable<WineDto>>
{
    public Guid? UserId { get; init; }
    public WineType? WineType { get; init; }
    public Region? Region { get; init; }
    public int? Vintage { get; init; }
    public string? Producer { get; init; }
}

public class GetWinesQueryHandler : IRequestHandler<GetWinesQuery, IEnumerable<WineDto>>
{
    private readonly IWineCellarInventoryContext _context;
    private readonly ILogger<GetWinesQueryHandler> _logger;

    public GetWinesQueryHandler(
        IWineCellarInventoryContext context,
        ILogger<GetWinesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<WineDto>> Handle(GetWinesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting wines for user {UserId}", request.UserId);

        var query = _context.Wines.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(w => w.UserId == request.UserId.Value);
        }

        if (request.WineType.HasValue)
        {
            query = query.Where(w => w.WineType == request.WineType.Value);
        }

        if (request.Region.HasValue)
        {
            query = query.Where(w => w.Region == request.Region.Value);
        }

        if (request.Vintage.HasValue)
        {
            query = query.Where(w => w.Vintage == request.Vintage.Value);
        }

        if (!string.IsNullOrEmpty(request.Producer))
        {
            query = query.Where(w => w.Producer != null && w.Producer.Contains(request.Producer));
        }

        var wines = await query
            .OrderBy(w => w.Name)
            .ToListAsync(cancellationToken);

        return wines.Select(w => w.ToDto());
    }
}
