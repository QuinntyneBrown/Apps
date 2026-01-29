using FocusSessions.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FocusSessions.Api.Features;

public record GetFocusSessionsQuery : IRequest<IEnumerable<FocusSessionDto>>;

public class GetFocusSessionsQueryHandler : IRequestHandler<GetFocusSessionsQuery, IEnumerable<FocusSessionDto>>
{
    private readonly IFocusSessionsDbContext _context;

    public GetFocusSessionsQueryHandler(IFocusSessionsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<FocusSessionDto>> Handle(GetFocusSessionsQuery request, CancellationToken cancellationToken)
    {
        return await _context.FocusSessions
            .AsNoTracking()
            .OrderByDescending(s => s.StartTime)
            .Select(s => new FocusSessionDto
            {
                SessionId = s.SessionId,
                UserId = s.UserId,
                TaskDescription = s.TaskDescription,
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                PlannedDurationMinutes = s.PlannedDurationMinutes,
                ActualDurationMinutes = s.ActualDurationMinutes,
                Status = s.Status.ToString(),
                CreatedAt = s.CreatedAt
            })
            .ToListAsync(cancellationToken);
    }
}
