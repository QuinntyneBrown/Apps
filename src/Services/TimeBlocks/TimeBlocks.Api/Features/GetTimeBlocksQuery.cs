using TimeBlocks.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TimeBlocks.Api.Features;

public record GetTimeBlocksQuery : IRequest<IEnumerable<TimeBlockDto>>;

public class GetTimeBlocksQueryHandler : IRequestHandler<GetTimeBlocksQuery, IEnumerable<TimeBlockDto>>
{
    private readonly ITimeBlocksDbContext _context;

    public GetTimeBlocksQueryHandler(ITimeBlocksDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TimeBlockDto>> Handle(GetTimeBlocksQuery request, CancellationToken cancellationToken)
    {
        var timeBlocks = await _context.TimeBlocks.ToListAsync(cancellationToken);
        return timeBlocks.Select(t => t.ToDto());
    }
}
