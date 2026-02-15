using Sources.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Sources.Api.Features;

public record GetSourcesQuery : IRequest<IEnumerable<SourceDto>>;

public class GetSourcesQueryHandler : IRequestHandler<GetSourcesQuery, IEnumerable<SourceDto>>
{
    private readonly ISourcesDbContext _context;

    public GetSourcesQueryHandler(ISourcesDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SourceDto>> Handle(GetSourcesQuery request, CancellationToken cancellationToken)
    {
        var sources = await _context.Sources
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return sources.Select(s => s.ToDto());
    }
}
