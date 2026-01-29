using Offers.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Offers.Api.Features;

public record GetOfferByIdQuery(Guid OfferId) : IRequest<OfferDto?>;

public class GetOfferByIdQueryHandler : IRequestHandler<GetOfferByIdQuery, OfferDto?>
{
    private readonly IOffersDbContext _context;

    public GetOfferByIdQueryHandler(IOffersDbContext context)
    {
        _context = context;
    }

    public async Task<OfferDto?> Handle(GetOfferByIdQuery request, CancellationToken cancellationToken)
    {
        var offer = await _context.Offers
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.OfferId == request.OfferId, cancellationToken);

        return offer?.ToDto();
    }
}
