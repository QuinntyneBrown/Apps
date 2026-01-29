using FocusSessions.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FocusSessions.Api.Features;

public record CompleteFocusSessionCommand : IRequest<FocusSessionDto?>
{
    public Guid SessionId { get; init; }
}

public class CompleteFocusSessionCommandHandler : IRequestHandler<CompleteFocusSessionCommand, FocusSessionDto?>
{
    private readonly IFocusSessionsDbContext _context;
    private readonly ILogger<CompleteFocusSessionCommandHandler> _logger;

    public CompleteFocusSessionCommandHandler(IFocusSessionsDbContext context, ILogger<CompleteFocusSessionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<FocusSessionDto?> Handle(CompleteFocusSessionCommand request, CancellationToken cancellationToken)
    {
        var session = await _context.FocusSessions
            .FirstOrDefaultAsync(s => s.SessionId == request.SessionId, cancellationToken);

        if (session == null) return null;

        session.Complete();
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Focus session completed: {SessionId}", session.SessionId);

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
