using Offers.Core;
using Offers.Core.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Offers.Api.Features;

public record UpdateOfferCommand(
    Guid OfferId,
    decimal SalaryAmount,
    string? SalaryType,
    decimal? SigningBonus,
    decimal? AnnualBonus,
    string? Benefits,
    DateTime StartDate,
    DateTime? ExpirationDate,
    OfferStatus Status,
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
        var offer = await _context.Offers
            .FirstOrDefaultAsync(o => o.OfferId == request.OfferId, cancellationToken);

        if (offer == null) return null;

        offer.SalaryAmount = request.SalaryAmount;
        offer.SalaryType = request.SalaryType;
        offer.SigningBonus = request.SigningBonus;
        offer.AnnualBonus = request.AnnualBonus;
        offer.Benefits = request.Benefits;
        offer.StartDate = request.StartDate;
        offer.ExpirationDate = request.ExpirationDate;
        offer.Status = request.Status;
        offer.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Offer updated: {OfferId}", offer.OfferId);

        return offer.ToDto();
    }
}
