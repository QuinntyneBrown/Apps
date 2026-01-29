using TimeBlocks.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TimeBlocks.Api.Features;

public record GetTimeBlockByIdQuery(Guid TimeBlockId) : IRequest<TimeBlockDto?>;

public class GetTimeBlockByIdQueryHandler : IRequestHandler<GetTimeBlockByIdQuery, TimeBlockDto?>
{
    private readonly ITimeBlocksDbContext _context;

    public GetTimeBlockByIdQueryHandler(ITimeBlocksDbContext context)
    {
        _context = context;
    }

    public async Task<TimeBlockDto?> Handle(GetTimeBlockByIdQuery request, CancellationToken cancellationToken)
    {
        var timeBlock = await _context.TimeBlocks
            .FirstOrDefaultAsync(t => t.TimeBlockId == request.TimeBlockId, cancellationToken);
        return timeBlock?.ToDto();
    }
}
