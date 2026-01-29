using Manuals.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Manuals.Api.Features;

public record GetManualByIdQuery(Guid ManualId) : IRequest<ManualDto?>;

public class GetManualByIdQueryHandler : IRequestHandler<GetManualByIdQuery, ManualDto?>
{
    private readonly IManualsDbContext _context;

    public GetManualByIdQueryHandler(IManualsDbContext context)
    {
        _context = context;
    }

    public async Task<ManualDto?> Handle(GetManualByIdQuery request, CancellationToken cancellationToken)
    {
        var manual = await _context.Manuals
            .FirstOrDefaultAsync(m => m.ManualId == request.ManualId, cancellationToken);
        return manual?.ToDto();
    }
}
