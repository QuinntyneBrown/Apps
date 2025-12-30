using MusicCollectionOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MusicCollectionOrganizer.Api.Features.Artists;

public record GetArtistsQuery : IRequest<IEnumerable<ArtistDto>>
{
    public Guid? UserId { get; init; }
    public string? Country { get; init; }
    public int? FormedYear { get; init; }
}

public class GetArtistsQueryHandler : IRequestHandler<GetArtistsQuery, IEnumerable<ArtistDto>>
{
    private readonly IMusicCollectionOrganizerContext _context;
    private readonly ILogger<GetArtistsQueryHandler> _logger;

    public GetArtistsQueryHandler(
        IMusicCollectionOrganizerContext context,
        ILogger<GetArtistsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<ArtistDto>> Handle(GetArtistsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting artists for user {UserId}", request.UserId);

        var query = _context.Artists.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(a => a.UserId == request.UserId.Value);
        }

        if (!string.IsNullOrEmpty(request.Country))
        {
            query = query.Where(a => a.Country == request.Country);
        }

        if (request.FormedYear.HasValue)
        {
            query = query.Where(a => a.FormedYear == request.FormedYear.Value);
        }

        var artists = await query
            .OrderBy(a => a.Name)
            .ToListAsync(cancellationToken);

        return artists.Select(a => a.ToDto());
    }
}
