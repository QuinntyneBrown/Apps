using Manuals.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Manuals.Api.Features;

public record GetManualsQuery : IRequest<IEnumerable<ManualDto>>;

public class GetManualsQueryHandler : IRequestHandler<GetManualsQuery, IEnumerable<ManualDto>>
{
    private readonly IManualsDbContext _context;

    public GetManualsQueryHandler(IManualsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ManualDto>> Handle(GetManualsQuery request, CancellationToken cancellationToken)
    {
        var manuals = await _context.Manuals.ToListAsync(cancellationToken);
        return manuals.Select(m => m.ToDto());
    }
}
