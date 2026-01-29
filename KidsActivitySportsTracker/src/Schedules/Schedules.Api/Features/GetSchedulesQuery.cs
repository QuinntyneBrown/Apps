using Schedules.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Schedules.Api.Features;

public record GetSchedulesQuery : IRequest<IEnumerable<ScheduleDto>>;

public class GetSchedulesQueryHandler : IRequestHandler<GetSchedulesQuery, IEnumerable<ScheduleDto>>
{
    private readonly ISchedulesDbContext _context;

    public GetSchedulesQueryHandler(ISchedulesDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ScheduleDto>> Handle(GetSchedulesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Schedules
            .AsNoTracking()
            .Select(s => s.ToDto())
            .ToListAsync(cancellationToken);
    }
}
