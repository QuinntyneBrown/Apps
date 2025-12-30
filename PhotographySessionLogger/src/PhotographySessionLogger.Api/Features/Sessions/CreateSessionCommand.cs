using PhotographySessionLogger.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace PhotographySessionLogger.Api.Features.Sessions;

public record CreateSessionCommand : IRequest<SessionDto>
{
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public SessionType SessionType { get; init; }
    public DateTime SessionDate { get; init; }
    public string? Location { get; init; }
    public string? Client { get; init; }
    public string? Notes { get; init; }
}

public class CreateSessionCommandHandler : IRequestHandler<CreateSessionCommand, SessionDto>
{
    private readonly IPhotographySessionLoggerContext _context;
    private readonly ILogger<CreateSessionCommandHandler> _logger;

    public CreateSessionCommandHandler(
        IPhotographySessionLoggerContext context,
        ILogger<CreateSessionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<SessionDto> Handle(CreateSessionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating session for user {UserId}, title: {Title}",
            request.UserId,
            request.Title);

        var session = new Session
        {
            SessionId = Guid.NewGuid(),
            UserId = request.UserId,
            Title = request.Title,
            SessionType = request.SessionType,
            SessionDate = request.SessionDate,
            Location = request.Location,
            Client = request.Client,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Sessions.Add(session);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created session {SessionId} for user {UserId}",
            session.SessionId,
            request.UserId);

        return session.ToDto();
    }
}
