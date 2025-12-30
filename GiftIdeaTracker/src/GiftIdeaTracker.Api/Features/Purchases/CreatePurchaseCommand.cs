using GiftIdeaTracker.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GiftIdeaTracker.Api.Features.Purchases;

public record CreatePurchaseCommand : IRequest<PurchaseDto>
{
    public Guid GiftIdeaId { get; init; }
    public DateTime PurchaseDate { get; init; }
    public decimal ActualPrice { get; init; }
    public string? Store { get; init; }
}

public class CreatePurchaseCommandHandler : IRequestHandler<CreatePurchaseCommand, PurchaseDto>
{
    private readonly IGiftIdeaTrackerContext _context;
    private readonly ILogger<CreatePurchaseCommandHandler> _logger;

    public CreatePurchaseCommandHandler(
        IGiftIdeaTrackerContext context,
        ILogger<CreatePurchaseCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PurchaseDto> Handle(CreatePurchaseCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating purchase for gift idea {GiftIdeaId}",
            request.GiftIdeaId);

        var purchase = new Purchase
        {
            PurchaseId = Guid.NewGuid(),
            GiftIdeaId = request.GiftIdeaId,
            PurchaseDate = request.PurchaseDate,
            ActualPrice = request.ActualPrice,
            Store = request.Store,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Purchases.Add(purchase);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created purchase {PurchaseId} for gift idea {GiftIdeaId}",
            purchase.PurchaseId,
            request.GiftIdeaId);

        return purchase.ToDto();
    }
}
