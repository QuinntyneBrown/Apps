using Intakes.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Intakes.Api.Features;

public record GetIntakesQuery : IRequest<IEnumerable<IntakeDto>>;

public class GetIntakesQueryHandler : IRequestHandler<GetIntakesQuery, IEnumerable<IntakeDto>>
{
    private readonly IIntakesDbContext _context;

    public GetIntakesQueryHandler(IIntakesDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<IntakeDto>> Handle(GetIntakesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Intakes
            .AsNoTracking()
            .Select(i => i.ToDto())
            .ToListAsync(cancellationToken);
    }
}
