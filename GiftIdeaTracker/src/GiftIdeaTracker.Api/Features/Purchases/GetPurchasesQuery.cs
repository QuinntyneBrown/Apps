using GiftIdeaTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GiftIdeaTracker.Api.Features.Purchases;

public record GetPurchasesQuery : IRequest<IEnumerable<PurchaseDto>>
{
    public Guid? GiftIdeaId { get; init; }
}

public class GetPurchasesQueryHandler : IRequestHandler<GetPurchasesQuery, IEnumerable<PurchaseDto>>
{
    private readonly IGiftIdeaTrackerContext _context;
    private readonly ILogger<GetPurchasesQueryHandler> _logger;

    public GetPurchasesQueryHandler(
        IGiftIdeaTrackerContext context,
        ILogger<GetPurchasesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<PurchaseDto>> Handle(GetPurchasesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting purchases");

        var query = _context.Purchases.AsNoTracking();

        if (request.GiftIdeaId.HasValue)
        {
            query = query.Where(p => p.GiftIdeaId == request.GiftIdeaId.Value);
        }

        var purchases = await query
            .OrderByDescending(p => p.PurchaseDate)
            .ToListAsync(cancellationToken);

        return purchases.Select(p => p.ToDto());
    }
}
