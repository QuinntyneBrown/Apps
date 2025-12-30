using MusicCollectionOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MusicCollectionOrganizer.Api.Features.Artists;

public record GetArtistByIdQuery : IRequest<ArtistDto?>
{
    public Guid ArtistId { get; init; }
}

public class GetArtistByIdQueryHandler : IRequestHandler<GetArtistByIdQuery, ArtistDto?>
{
    private readonly IMusicCollectionOrganizerContext _context;
    private readonly ILogger<GetArtistByIdQueryHandler> _logger;

    public GetArtistByIdQueryHandler(
        IMusicCollectionOrganizerContext context,
        ILogger<GetArtistByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ArtistDto?> Handle(GetArtistByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting artist {ArtistId}", request.ArtistId);

        var artist = await _context.Artists
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.ArtistId == request.ArtistId, cancellationToken);

        if (artist == null)
        {
            _logger.LogWarning("Artist {ArtistId} not found", request.ArtistId);
            return null;
        }

        return artist.ToDto();
    }
}
