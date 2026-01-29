using Offers.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Offers.Api.Features;

public record UpdateOfferCommand(
    Guid OfferId,
    string LenderName,
    decimal InterestRate,
    int TermMonths,
    decimal MonthlyPayment,
    decimal TotalCost,
    decimal OriginationFee,
    string? Notes) : IRequest<OfferDto?>;

public class UpdateOfferCommandHandler : IRequestHandler<UpdateOfferCommand, OfferDto?>
{
    private readonly IOffersDbContext _context;
    private readonly ILogger<UpdateOfferCommandHandler> _logger;

    public UpdateOfferCommandHandler(IOffersDbContext context, ILogger<UpdateOfferCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<OfferDto?> Handle(UpdateOfferCommand request, CancellationToken cancellationToken)
    {
        var offer = await _context.Offers.FirstOrDefaultAsync(o => o.OfferId == request.OfferId, cancellationToken);
        if (offer == null) return null;

        offer.LenderName = request.LenderName;
        offer.InterestRate = request.InterestRate;
        offer.TermMonths = request.TermMonths;
        offer.MonthlyPayment = request.MonthlyPayment;
        offer.TotalCost = request.TotalCost;
        offer.OriginationFee = request.OriginationFee;
        offer.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Offer updated: {OfferId}", offer.OfferId);

        return offer.ToDto();
    }
}
