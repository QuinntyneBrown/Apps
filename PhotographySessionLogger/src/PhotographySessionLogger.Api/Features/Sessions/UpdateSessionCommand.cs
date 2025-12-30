using PhotographySessionLogger.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PhotographySessionLogger.Api.Features.Sessions;

public record UpdateSessionCommand : IRequest<SessionDto?>
{
    public Guid SessionId { get; init; }
    public string Title { get; init; } = string.Empty;
    public SessionType SessionType { get; init; }
    public DateTime SessionDate { get; init; }
    public string? Location { get; init; }
    public string? Client { get; init; }
    public string? Notes { get; init; }
}

public class UpdateSessionCommandHandler : IRequestHandler<UpdateSessionCommand, SessionDto?>
{
    private readonly IPhotographySessionLoggerContext _context;
    private readonly ILogger<UpdateSessionCommandHandler> _logger;

    public UpdateSessionCommandHandler(
        IPhotographySessionLoggerContext context,
        ILogger<UpdateSessionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<SessionDto?> Handle(UpdateSessionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating session {SessionId}", request.SessionId);

        var session = await _context.Sessions
            .Include(s => s.Photos)
            .FirstOrDefaultAsync(s => s.SessionId == request.SessionId, cancellationToken);

        if (session == null)
        {
            _logger.LogWarning("Session {SessionId} not found", request.SessionId);
            return null;
        }

        session.Title = request.Title;
        session.SessionType = request.SessionType;
        session.SessionDate = request.SessionDate;
        session.Location = request.Location;
        session.Client = request.Client;
        session.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated session {SessionId}", request.SessionId);

        return session.ToDto();
    }
}
