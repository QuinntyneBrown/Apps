using MusicCollectionOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MusicCollectionOrganizer.Api.Features.Albums;

public record GetAlbumsQuery : IRequest<IEnumerable<AlbumDto>>
{
    public Guid? UserId { get; init; }
    public Guid? ArtistId { get; init; }
    public Format? Format { get; init; }
    public Genre? Genre { get; init; }
    public int? ReleaseYear { get; init; }
}

public class GetAlbumsQueryHandler : IRequestHandler<GetAlbumsQuery, IEnumerable<AlbumDto>>
{
    private readonly IMusicCollectionOrganizerContext _context;
    private readonly ILogger<GetAlbumsQueryHandler> _logger;

    public GetAlbumsQueryHandler(
        IMusicCollectionOrganizerContext context,
        ILogger<GetAlbumsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<AlbumDto>> Handle(GetAlbumsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting albums for user {UserId}", request.UserId);

        var query = _context.Albums
            .Include(a => a.Artist)
            .AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(a => a.UserId == request.UserId.Value);
        }

        if (request.ArtistId.HasValue)
        {
            query = query.Where(a => a.ArtistId == request.ArtistId.Value);
        }

        if (request.Format.HasValue)
        {
            query = query.Where(a => a.Format == request.Format.Value);
        }

        if (request.Genre.HasValue)
        {
            query = query.Where(a => a.Genre == request.Genre.Value);
        }

        if (request.ReleaseYear.HasValue)
        {
            query = query.Where(a => a.ReleaseYear == request.ReleaseYear.Value);
        }

        var albums = await query
            .OrderBy(a => a.Title)
            .ToListAsync(cancellationToken);

        return albums.Select(a => a.ToDto());
    }
}
