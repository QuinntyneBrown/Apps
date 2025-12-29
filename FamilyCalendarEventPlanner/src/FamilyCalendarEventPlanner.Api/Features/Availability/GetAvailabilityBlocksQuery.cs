using FamilyCalendarEventPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyCalendarEventPlanner.Api.Features.Availability;

public record GetAvailabilityBlocksQuery : IRequest<IEnumerable<AvailabilityBlockDto>>
{
    public Guid MemberId { get; init; }
}

public class GetAvailabilityBlocksQueryHandler : IRequestHandler<GetAvailabilityBlocksQuery, IEnumerable<AvailabilityBlockDto>>
{
    private readonly IFamilyCalendarEventPlannerContext _context;
    private readonly ILogger<GetAvailabilityBlocksQueryHandler> _logger;

    public GetAvailabilityBlocksQueryHandler(
        IFamilyCalendarEventPlannerContext context,
        ILogger<GetAvailabilityBlocksQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<AvailabilityBlockDto>> Handle(GetAvailabilityBlocksQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting availability blocks for member {MemberId}", request.MemberId);

        var blocks = await _context.AvailabilityBlocks
            .AsNoTracking()
            .Where(b => b.MemberId == request.MemberId)
            .OrderBy(b => b.StartTime)
            .ToListAsync(cancellationToken);

        return blocks.Select(b => b.ToDto());
    }
}
