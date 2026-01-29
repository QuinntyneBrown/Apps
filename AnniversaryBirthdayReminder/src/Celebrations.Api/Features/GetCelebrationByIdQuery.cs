using Celebrations.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Celebrations.Api.Features;

public record GetCelebrationByIdQuery(Guid CelebrationId) : IRequest<CelebrationDto?>;

public class GetCelebrationByIdQueryHandler : IRequestHandler<GetCelebrationByIdQuery, CelebrationDto?>
{
    private readonly ICelebrationsDbContext _context;

    public GetCelebrationByIdQueryHandler(ICelebrationsDbContext context)
    {
        _context = context;
    }

    public async Task<CelebrationDto?> Handle(GetCelebrationByIdQuery request, CancellationToken cancellationToken)
    {
        var celebration = await _context.Celebrations
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CelebrationId == request.CelebrationId, cancellationToken);

        return celebration?.ToDto();
    }
}
