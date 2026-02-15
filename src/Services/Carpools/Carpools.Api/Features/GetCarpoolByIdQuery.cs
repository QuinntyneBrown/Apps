using Carpools.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Carpools.Api.Features;

public record GetCarpoolByIdQuery(Guid CarpoolId) : IRequest<CarpoolDto?>;

public class GetCarpoolByIdQueryHandler : IRequestHandler<GetCarpoolByIdQuery, CarpoolDto?>
{
    private readonly ICarpoolsDbContext _context;

    public GetCarpoolByIdQueryHandler(ICarpoolsDbContext context)
    {
        _context = context;
    }

    public async Task<CarpoolDto?> Handle(GetCarpoolByIdQuery request, CancellationToken cancellationToken)
    {
        var carpool = await _context.Carpools
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CarpoolId == request.CarpoolId, cancellationToken);

        return carpool?.ToDto();
    }
}
