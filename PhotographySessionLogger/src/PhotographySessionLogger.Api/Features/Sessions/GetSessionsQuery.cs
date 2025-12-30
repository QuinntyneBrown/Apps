using PhotographySessionLogger.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PhotographySessionLogger.Api.Features.Sessions;

public record GetSessionsQuery : IRequest<IEnumerable<SessionDto>>
{
    public Guid? UserId { get; init; }
    public SessionType? SessionType { get; init; }
    public string? Location { get; init; }
}

public class GetSessionsQueryHandler : IRequestHandler<GetSessionsQuery, IEnumerable<SessionDto>>
{
    private readonly IPhotographySessionLoggerContext _context;
    private readonly ILogger<GetSessionsQueryHandler> _logger;

    public GetSessionsQueryHandler(
        IPhotographySessionLoggerContext context,
        ILogger<GetSessionsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<SessionDto>> Handle(GetSessionsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting sessions for user {UserId}", request.UserId);

        var query = _context.Sessions
            .Include(s => s.Photos)
            .AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(s => s.UserId == request.UserId.Value);
        }

        if (request.SessionType.HasValue)
        {
            query = query.Where(s => s.SessionType == request.SessionType.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.Location))
        {
            query = query.Where(s => s.Location != null && s.Location.Contains(request.Location));
        }

        var sessions = await query
            .OrderByDescending(s => s.SessionDate)
            .ToListAsync(cancellationToken);

        return sessions.Select(s => s.ToDto());
    }
}
