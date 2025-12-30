using MusicCollectionOrganizer.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MusicCollectionOrganizer.Api.Features.Artists;

public record CreateArtistCommand : IRequest<ArtistDto>
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Biography { get; init; }
    public string? Country { get; init; }
    public int? FormedYear { get; init; }
    public string? Website { get; init; }
}

public class CreateArtistCommandHandler : IRequestHandler<CreateArtistCommand, ArtistDto>
{
    private readonly IMusicCollectionOrganizerContext _context;
    private readonly ILogger<CreateArtistCommandHandler> _logger;

    public CreateArtistCommandHandler(
        IMusicCollectionOrganizerContext context,
        ILogger<CreateArtistCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ArtistDto> Handle(CreateArtistCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating artist for user {UserId}, name: {Name}",
            request.UserId,
            request.Name);

        var artist = new Artist
        {
            ArtistId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            Biography = request.Biography,
            Country = request.Country,
            FormedYear = request.FormedYear,
            Website = request.Website,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Artists.Add(artist);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created artist {ArtistId} for user {UserId}",
            artist.ArtistId,
            request.UserId);

        return artist.ToDto();
    }
}
