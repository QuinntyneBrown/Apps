using Celebrations.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Celebrations.Api.Features;

public record GetCelebrationsQuery : IRequest<IEnumerable<CelebrationDto>>;

public class GetCelebrationsQueryHandler : IRequestHandler<GetCelebrationsQuery, IEnumerable<CelebrationDto>>
{
    private readonly ICelebrationsDbContext _context;

    public GetCelebrationsQueryHandler(ICelebrationsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CelebrationDto>> Handle(GetCelebrationsQuery request, CancellationToken cancellationToken)
    {
        var celebrations = await _context.Celebrations
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return celebrations.Select(c => c.ToDto());
    }
}
