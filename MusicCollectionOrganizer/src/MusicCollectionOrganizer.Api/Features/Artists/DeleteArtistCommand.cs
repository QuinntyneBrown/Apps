using MusicCollectionOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MusicCollectionOrganizer.Api.Features.Artists;

public record DeleteArtistCommand : IRequest<bool>
{
    public Guid ArtistId { get; init; }
}

public class DeleteArtistCommandHandler : IRequestHandler<DeleteArtistCommand, bool>
{
    private readonly IMusicCollectionOrganizerContext _context;
    private readonly ILogger<DeleteArtistCommandHandler> _logger;

    public DeleteArtistCommandHandler(
        IMusicCollectionOrganizerContext context,
        ILogger<DeleteArtistCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteArtistCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting artist {ArtistId}", request.ArtistId);

        var artist = await _context.Artists
            .FirstOrDefaultAsync(a => a.ArtistId == request.ArtistId, cancellationToken);

        if (artist == null)
        {
            _logger.LogWarning("Artist {ArtistId} not found", request.ArtistId);
            return false;
        }

        _context.Artists.Remove(artist);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted artist {ArtistId}", request.ArtistId);

        return true;
    }
}
