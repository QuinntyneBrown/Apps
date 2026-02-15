using FocusSessions.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FocusSessions.Api.Features;

public record GetFocusSessionByIdQuery : IRequest<FocusSessionDto?>
{
    public Guid SessionId { get; init; }
}

public class GetFocusSessionByIdQueryHandler : IRequestHandler<GetFocusSessionByIdQuery, FocusSessionDto?>
{
    private readonly IFocusSessionsDbContext _context;

    public GetFocusSessionByIdQueryHandler(IFocusSessionsDbContext context)
    {
        _context = context;
    }

    public async Task<FocusSessionDto?> Handle(GetFocusSessionByIdQuery request, CancellationToken cancellationToken)
    {
        var session = await _context.FocusSessions
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.SessionId == request.SessionId, cancellationToken);

        if (session == null) return null;

        return new FocusSessionDto
        {
            SessionId = session.SessionId,
            UserId = session.UserId,
            TaskDescription = session.TaskDescription,
            StartTime = session.StartTime,
            EndTime = session.EndTime,
            PlannedDurationMinutes = session.PlannedDurationMinutes,
            ActualDurationMinutes = session.ActualDurationMinutes,
            Status = session.Status.ToString(),
            CreatedAt = session.CreatedAt
        };
    }
}
