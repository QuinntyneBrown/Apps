using Screenings.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Screenings.Api.Features;

public record GetScreeningsQuery : IRequest<IEnumerable<ScreeningDto>>;

public class GetScreeningsQueryHandler : IRequestHandler<GetScreeningsQuery, IEnumerable<ScreeningDto>>
{
    private readonly IScreeningsDbContext _context;

    public GetScreeningsQueryHandler(IScreeningsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ScreeningDto>> Handle(GetScreeningsQuery request, CancellationToken cancellationToken)
    {
        var screenings = await _context.Screenings
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return screenings.Select(s => s.ToDto());
    }
}
