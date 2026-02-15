using Schedules.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Schedules.Api.Features;

public record GetScheduleByIdQuery(Guid ScheduleId) : IRequest<ScheduleDto?>;

public class GetScheduleByIdQueryHandler : IRequestHandler<GetScheduleByIdQuery, ScheduleDto?>
{
    private readonly ISchedulesDbContext _context;

    public GetScheduleByIdQueryHandler(ISchedulesDbContext context)
    {
        _context = context;
    }

    public async Task<ScheduleDto?> Handle(GetScheduleByIdQuery request, CancellationToken cancellationToken)
    {
        var schedule = await _context.Schedules
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.ScheduleId == request.ScheduleId, cancellationToken);

        return schedule?.ToDto();
    }
}
