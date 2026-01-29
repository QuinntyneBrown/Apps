using Offers.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Offers.Api.Features;

public record DeleteOfferCommand(Guid OfferId) : IRequest<bool>;

public class DeleteOfferCommandHandler : IRequestHandler<DeleteOfferCommand, bool>
{
    private readonly IOffersDbContext _context;
    private readonly ILogger<DeleteOfferCommandHandler> _logger;

    public DeleteOfferCommandHandler(IOffersDbContext context, ILogger<DeleteOfferCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteOfferCommand request, CancellationToken cancellationToken)
    {
        var offer = await _context.Offers
            .FirstOrDefaultAsync(o => o.OfferId == request.OfferId, cancellationToken);

        if (offer == null) return false;

        _context.Offers.Remove(offer);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Offer deleted: {OfferId}", request.OfferId);

        return true;
    }
}
