using Screenings.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Screenings.Api.Features;

public record GetScreeningByIdQuery(Guid ScreeningId) : IRequest<ScreeningDto?>;

public class GetScreeningByIdQueryHandler : IRequestHandler<GetScreeningByIdQuery, ScreeningDto?>
{
    private readonly IScreeningsDbContext _context;

    public GetScreeningByIdQueryHandler(IScreeningsDbContext context)
    {
        _context = context;
    }

    public async Task<ScreeningDto?> Handle(GetScreeningByIdQuery request, CancellationToken cancellationToken)
    {
        var screening = await _context.Screenings
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.ScreeningId == request.ScreeningId, cancellationToken);

        return screening?.ToDto();
    }
}
