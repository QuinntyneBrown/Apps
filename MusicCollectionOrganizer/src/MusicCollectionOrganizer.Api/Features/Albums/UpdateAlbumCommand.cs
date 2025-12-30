using MusicCollectionOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MusicCollectionOrganizer.Api.Features.Albums;

public record UpdateAlbumCommand : IRequest<AlbumDto?>
{
    public Guid AlbumId { get; init; }
    public string Title { get; init; } = string.Empty;
    public Guid? ArtistId { get; init; }
    public Format Format { get; init; }
    public Genre Genre { get; init; }
    public int? ReleaseYear { get; init; }
    public string? Label { get; init; }
    public decimal? PurchasePrice { get; init; }
    public DateTime? PurchaseDate { get; init; }
    public string? Notes { get; init; }
}

public class UpdateAlbumCommandHandler : IRequestHandler<UpdateAlbumCommand, AlbumDto?>
{
    private readonly IMusicCollectionOrganizerContext _context;
    private readonly ILogger<UpdateAlbumCommandHandler> _logger;

    public UpdateAlbumCommandHandler(
        IMusicCollectionOrganizerContext context,
        ILogger<UpdateAlbumCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<AlbumDto?> Handle(UpdateAlbumCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating album {AlbumId}", request.AlbumId);

        var album = await _context.Albums
            .FirstOrDefaultAsync(a => a.AlbumId == request.AlbumId, cancellationToken);

        if (album == null)
        {
            _logger.LogWarning("Album {AlbumId} not found", request.AlbumId);
            return null;
        }

        album.Title = request.Title;
        album.ArtistId = request.ArtistId;
        album.Format = request.Format;
        album.Genre = request.Genre;
        album.ReleaseYear = request.ReleaseYear;
        album.Label = request.Label;
        album.PurchasePrice = request.PurchasePrice;
        album.PurchaseDate = request.PurchaseDate;
        album.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated album {AlbumId}", request.AlbumId);

        return album.ToDto();
    }
}
