using MusicCollectionOrganizer.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MusicCollectionOrganizer.Api.Features.Albums;

public record CreateAlbumCommand : IRequest<AlbumDto>
{
    public Guid UserId { get; init; }
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

public class CreateAlbumCommandHandler : IRequestHandler<CreateAlbumCommand, AlbumDto>
{
    private readonly IMusicCollectionOrganizerContext _context;
    private readonly ILogger<CreateAlbumCommandHandler> _logger;

    public CreateAlbumCommandHandler(
        IMusicCollectionOrganizerContext context,
        ILogger<CreateAlbumCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<AlbumDto> Handle(CreateAlbumCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating album for user {UserId}, title: {Title}",
            request.UserId,
            request.Title);

        var album = new Album
        {
            AlbumId = Guid.NewGuid(),
            UserId = request.UserId,
            Title = request.Title,
            ArtistId = request.ArtistId,
            Format = request.Format,
            Genre = request.Genre,
            ReleaseYear = request.ReleaseYear,
            Label = request.Label,
            PurchasePrice = request.PurchasePrice,
            PurchaseDate = request.PurchaseDate,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Albums.Add(album);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created album {AlbumId} for user {UserId}",
            album.AlbumId,
            request.UserId);

        return album.ToDto();
    }
}
