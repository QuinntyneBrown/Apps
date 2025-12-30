using PhotographySessionLogger.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PhotographySessionLogger.Api.Features.Sessions;

public record GetSessionByIdQuery : IRequest<SessionDto?>
{
    public Guid SessionId { get; init; }
}

public class GetSessionByIdQueryHandler : IRequestHandler<GetSessionByIdQuery, SessionDto?>
{
    private readonly IPhotographySessionLoggerContext _context;
    private readonly ILogger<GetSessionByIdQueryHandler> _logger;

    public GetSessionByIdQueryHandler(
        IPhotographySessionLoggerContext context,
        ILogger<GetSessionByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<SessionDto?> Handle(GetSessionByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting session {SessionId}", request.SessionId);

        var session = await _context.Sessions
            .Include(s => s.Photos)
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.SessionId == request.SessionId, cancellationToken);

        return session?.ToDto();
    }
}
