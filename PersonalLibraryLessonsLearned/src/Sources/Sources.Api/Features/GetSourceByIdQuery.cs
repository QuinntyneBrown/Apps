using Sources.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Sources.Api.Features;

public record GetSourceByIdQuery(Guid SourceId) : IRequest<SourceDto?>;

public class GetSourceByIdQueryHandler : IRequestHandler<GetSourceByIdQuery, SourceDto?>
{
    private readonly ISourcesDbContext _context;

    public GetSourceByIdQueryHandler(ISourcesDbContext context)
    {
        _context = context;
    }

    public async Task<SourceDto?> Handle(GetSourceByIdQuery request, CancellationToken cancellationToken)
    {
        var source = await _context.Sources
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.SourceId == request.SourceId, cancellationToken);

        return source?.ToDto();
    }
}
