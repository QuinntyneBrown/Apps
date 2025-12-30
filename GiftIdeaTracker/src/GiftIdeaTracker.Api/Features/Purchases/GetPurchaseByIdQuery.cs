using GiftIdeaTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GiftIdeaTracker.Api.Features.Purchases;

public record GetPurchaseByIdQuery : IRequest<PurchaseDto?>
{
    public Guid PurchaseId { get; init; }
}

public class GetPurchaseByIdQueryHandler : IRequestHandler<GetPurchaseByIdQuery, PurchaseDto?>
{
    private readonly IGiftIdeaTrackerContext _context;
    private readonly ILogger<GetPurchaseByIdQueryHandler> _logger;

    public GetPurchaseByIdQueryHandler(
        IGiftIdeaTrackerContext context,
        ILogger<GetPurchaseByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PurchaseDto?> Handle(GetPurchaseByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting purchase {PurchaseId}", request.PurchaseId);

        var purchase = await _context.Purchases
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.PurchaseId == request.PurchaseId, cancellationToken);

        return purchase?.ToDto();
    }
}
