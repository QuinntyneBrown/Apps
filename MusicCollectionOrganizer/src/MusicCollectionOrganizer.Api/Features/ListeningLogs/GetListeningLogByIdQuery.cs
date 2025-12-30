using MusicCollectionOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MusicCollectionOrganizer.Api.Features.ListeningLogs;

public record GetListeningLogByIdQuery : IRequest<ListeningLogDto?>
{
    public Guid ListeningLogId { get; init; }
}

public class GetListeningLogByIdQueryHandler : IRequestHandler<GetListeningLogByIdQuery, ListeningLogDto?>
{
    private readonly IMusicCollectionOrganizerContext _context;
    private readonly ILogger<GetListeningLogByIdQueryHandler> _logger;

    public GetListeningLogByIdQueryHandler(
        IMusicCollectionOrganizerContext context,
        ILogger<GetListeningLogByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ListeningLogDto?> Handle(GetListeningLogByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting listening log {ListeningLogId}", request.ListeningLogId);

        var listeningLog = await _context.ListeningLogs
            .Include(l => l.Album)
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.ListeningLogId == request.ListeningLogId, cancellationToken);

        if (listeningLog == null)
        {
            _logger.LogWarning("Listening log {ListeningLogId} not found", request.ListeningLogId);
            return null;
        }

        return listeningLog.ToDto();
    }
}
