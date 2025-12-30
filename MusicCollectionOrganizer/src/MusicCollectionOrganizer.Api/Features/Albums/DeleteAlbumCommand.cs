using MusicCollectionOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MusicCollectionOrganizer.Api.Features.Albums;

public record DeleteAlbumCommand : IRequest<bool>
{
    public Guid AlbumId { get; init; }
}

public class DeleteAlbumCommandHandler : IRequestHandler<DeleteAlbumCommand, bool>
{
    private readonly IMusicCollectionOrganizerContext _context;
    private readonly ILogger<DeleteAlbumCommandHandler> _logger;

    public DeleteAlbumCommandHandler(
        IMusicCollectionOrganizerContext context,
        ILogger<DeleteAlbumCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteAlbumCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting album {AlbumId}", request.AlbumId);

        var album = await _context.Albums
            .FirstOrDefaultAsync(a => a.AlbumId == request.AlbumId, cancellationToken);

        if (album == null)
        {
            _logger.LogWarning("Album {AlbumId} not found", request.AlbumId);
            return false;
        }

        _context.Albums.Remove(album);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted album {AlbumId}", request.AlbumId);

        return true;
    }
}
