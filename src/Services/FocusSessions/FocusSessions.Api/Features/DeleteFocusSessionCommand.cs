using FocusSessions.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FocusSessions.Api.Features;

public record DeleteFocusSessionCommand : IRequest<bool>
{
    public Guid SessionId { get; init; }
}

public class DeleteFocusSessionCommandHandler : IRequestHandler<DeleteFocusSessionCommand, bool>
{
    private readonly IFocusSessionsDbContext _context;
    private readonly ILogger<DeleteFocusSessionCommandHandler> _logger;

    public DeleteFocusSessionCommandHandler(IFocusSessionsDbContext context, ILogger<DeleteFocusSessionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteFocusSessionCommand request, CancellationToken cancellationToken)
    {
        var session = await _context.FocusSessions
            .FirstOrDefaultAsync(s => s.SessionId == request.SessionId, cancellationToken);

        if (session == null) return false;

        _context.FocusSessions.Remove(session);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Focus session deleted: {SessionId}", request.SessionId);

        return true;
    }
}
