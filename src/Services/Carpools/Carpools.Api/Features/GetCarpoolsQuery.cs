using Carpools.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Carpools.Api.Features;

public record GetCarpoolsQuery : IRequest<IEnumerable<CarpoolDto>>;

public class GetCarpoolsQueryHandler : IRequestHandler<GetCarpoolsQuery, IEnumerable<CarpoolDto>>
{
    private readonly ICarpoolsDbContext _context;

    public GetCarpoolsQueryHandler(ICarpoolsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CarpoolDto>> Handle(GetCarpoolsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Carpools
            .AsNoTracking()
            .Select(c => c.ToDto())
            .ToListAsync(cancellationToken);
    }
}
