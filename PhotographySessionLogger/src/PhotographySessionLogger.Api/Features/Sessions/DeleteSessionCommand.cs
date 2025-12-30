using PhotographySessionLogger.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PhotographySessionLogger.Api.Features.Sessions;

public record DeleteSessionCommand : IRequest<bool>
{
    public Guid SessionId { get; init; }
}

public class DeleteSessionCommandHandler : IRequestHandler<DeleteSessionCommand, bool>
{
    private readonly IPhotographySessionLoggerContext _context;
    private readonly ILogger<DeleteSessionCommandHandler> _logger;

    public DeleteSessionCommandHandler(
        IPhotographySessionLoggerContext context,
        ILogger<DeleteSessionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteSessionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting session {SessionId}", request.SessionId);

        var session = await _context.Sessions
            .FirstOrDefaultAsync(s => s.SessionId == request.SessionId, cancellationToken);

        if (session == null)
        {
            _logger.LogWarning("Session {SessionId} not found", request.SessionId);
            return false;
        }

        _context.Sessions.Remove(session);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted session {SessionId}", request.SessionId);

        return true;
    }
}
