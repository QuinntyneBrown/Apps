using MusicCollectionOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MusicCollectionOrganizer.Api.Features.ListeningLogs;

public record GetListeningLogsQuery : IRequest<IEnumerable<ListeningLogDto>>
{
    public Guid? UserId { get; init; }
    public Guid? AlbumId { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public int? MinRating { get; init; }
}

public class GetListeningLogsQueryHandler : IRequestHandler<GetListeningLogsQuery, IEnumerable<ListeningLogDto>>
{
    private readonly IMusicCollectionOrganizerContext _context;
    private readonly ILogger<GetListeningLogsQueryHandler> _logger;

    public GetListeningLogsQueryHandler(
        IMusicCollectionOrganizerContext context,
        ILogger<GetListeningLogsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<ListeningLogDto>> Handle(GetListeningLogsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting listening logs for user {UserId}", request.UserId);

        var query = _context.ListeningLogs
            .Include(l => l.Album)
            .AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(l => l.UserId == request.UserId.Value);
        }

        if (request.AlbumId.HasValue)
        {
            query = query.Where(l => l.AlbumId == request.AlbumId.Value);
        }

        if (request.StartDate.HasValue)
        {
            query = query.Where(l => l.ListeningDate >= request.StartDate.Value);
        }

        if (request.EndDate.HasValue)
        {
            query = query.Where(l => l.ListeningDate <= request.EndDate.Value);
        }

        if (request.MinRating.HasValue)
        {
            query = query.Where(l => l.Rating >= request.MinRating.Value);
        }

        var listeningLogs = await query
            .OrderByDescending(l => l.ListeningDate)
            .ToListAsync(cancellationToken);

        return listeningLogs.Select(l => l.ToDto());
    }
}
