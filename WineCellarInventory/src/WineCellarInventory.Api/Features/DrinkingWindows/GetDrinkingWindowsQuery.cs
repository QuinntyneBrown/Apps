using WineCellarInventory.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WineCellarInventory.Api.Features.DrinkingWindows;

public record GetDrinkingWindowsQuery : IRequest<IEnumerable<DrinkingWindowDto>>
{
    public Guid? UserId { get; init; }
    public Guid? WineId { get; init; }
    public bool? IsCurrent { get; init; }
}

public class GetDrinkingWindowsQueryHandler : IRequestHandler<GetDrinkingWindowsQuery, IEnumerable<DrinkingWindowDto>>
{
    private readonly IWineCellarInventoryContext _context;
    private readonly ILogger<GetDrinkingWindowsQueryHandler> _logger;

    public GetDrinkingWindowsQueryHandler(
        IWineCellarInventoryContext context,
        ILogger<GetDrinkingWindowsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<DrinkingWindowDto>> Handle(GetDrinkingWindowsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting drinking windows for user {UserId}", request.UserId);

        var query = _context.DrinkingWindows.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(d => d.UserId == request.UserId.Value);
        }

        if (request.WineId.HasValue)
        {
            query = query.Where(d => d.WineId == request.WineId.Value);
        }

        if (request.IsCurrent.HasValue && request.IsCurrent.Value)
        {
            var now = DateTime.UtcNow;
            query = query.Where(d => d.StartDate <= now && d.EndDate >= now);
        }

        var drinkingWindows = await query
            .OrderBy(d => d.StartDate)
            .ToListAsync(cancellationToken);

        return drinkingWindows.Select(d => d.ToDto());
    }
}
