using FocusSessions.Core;
using FocusSessions.Core.Models;
using MediatR;

namespace FocusSessions.Api.Features;

public record CreateFocusSessionCommand : IRequest<FocusSessionDto>
{
    public Guid TenantId { get; init; }
    public Guid UserId { get; init; }
    public int PlannedDurationMinutes { get; init; }
    public string? TaskDescription { get; init; }
}

public class CreateFocusSessionCommandHandler : IRequestHandler<CreateFocusSessionCommand, FocusSessionDto>
{
    private readonly IFocusSessionsDbContext _context;
    private readonly ILogger<CreateFocusSessionCommandHandler> _logger;

    public CreateFocusSessionCommandHandler(IFocusSessionsDbContext context, ILogger<CreateFocusSessionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<FocusSessionDto> Handle(CreateFocusSessionCommand request, CancellationToken cancellationToken)
    {
        var session = new FocusSession(
            request.TenantId,
            request.UserId,
            request.PlannedDurationMinutes,
            request.TaskDescription);

        _context.FocusSessions.Add(session);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Focus session created: {SessionId}", session.SessionId);

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
