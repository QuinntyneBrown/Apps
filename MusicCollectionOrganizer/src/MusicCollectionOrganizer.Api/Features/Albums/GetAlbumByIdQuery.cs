using MusicCollectionOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MusicCollectionOrganizer.Api.Features.Albums;

public record GetAlbumByIdQuery : IRequest<AlbumDto?>
{
    public Guid AlbumId { get; init; }
}

public class GetAlbumByIdQueryHandler : IRequestHandler<GetAlbumByIdQuery, AlbumDto?>
{
    private readonly IMusicCollectionOrganizerContext _context;
    private readonly ILogger<GetAlbumByIdQueryHandler> _logger;

    public GetAlbumByIdQueryHandler(
        IMusicCollectionOrganizerContext context,
        ILogger<GetAlbumByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<AlbumDto?> Handle(GetAlbumByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting album {AlbumId}", request.AlbumId);

        var album = await _context.Albums
            .Include(a => a.Artist)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.AlbumId == request.AlbumId, cancellationToken);

        if (album == null)
        {
            _logger.LogWarning("Album {AlbumId} not found", request.AlbumId);
            return null;
        }

        return album.ToDto();
    }
}
