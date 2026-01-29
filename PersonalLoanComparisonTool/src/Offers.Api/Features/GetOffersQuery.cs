using Offers.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Offers.Api.Features;

public record GetOffersQuery : IRequest<IEnumerable<OfferDto>>;

public class GetOffersQueryHandler : IRequestHandler<GetOffersQuery, IEnumerable<OfferDto>>
{
    private readonly IOffersDbContext _context;

    public GetOffersQueryHandler(IOffersDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OfferDto>> Handle(GetOffersQuery request, CancellationToken cancellationToken)
    {
        var offers = await _context.Offers.AsNoTracking().ToListAsync(cancellationToken);
        return offers.Select(o => o.ToDto());
    }
}
