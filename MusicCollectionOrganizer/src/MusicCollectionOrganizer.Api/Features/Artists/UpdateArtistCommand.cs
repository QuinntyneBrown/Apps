using MusicCollectionOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MusicCollectionOrganizer.Api.Features.Artists;

public record UpdateArtistCommand : IRequest<ArtistDto?>
{
    public Guid ArtistId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Biography { get; init; }
    public string? Country { get; init; }
    public int? FormedYear { get; init; }
    public string? Website { get; init; }
}

public class UpdateArtistCommandHandler : IRequestHandler<UpdateArtistCommand, ArtistDto?>
{
    private readonly IMusicCollectionOrganizerContext _context;
    private readonly ILogger<UpdateArtistCommandHandler> _logger;

    public UpdateArtistCommandHandler(
        IMusicCollectionOrganizerContext context,
        ILogger<UpdateArtistCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ArtistDto?> Handle(UpdateArtistCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating artist {ArtistId}", request.ArtistId);

        var artist = await _context.Artists
            .FirstOrDefaultAsync(a => a.ArtistId == request.ArtistId, cancellationToken);

        if (artist == null)
        {
            _logger.LogWarning("Artist {ArtistId} not found", request.ArtistId);
            return null;
        }

        artist.Name = request.Name;
        artist.Biography = request.Biography;
        artist.Country = request.Country;
        artist.FormedYear = request.FormedYear;
        artist.Website = request.Website;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated artist {ArtistId}", request.ArtistId);

        return artist.ToDto();
    }
}
