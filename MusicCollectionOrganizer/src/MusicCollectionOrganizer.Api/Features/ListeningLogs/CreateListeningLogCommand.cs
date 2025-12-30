using MusicCollectionOrganizer.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MusicCollectionOrganizer.Api.Features.ListeningLogs;

public record CreateListeningLogCommand : IRequest<ListeningLogDto>
{
    public Guid UserId { get; init; }
    public Guid AlbumId { get; init; }
    public DateTime ListeningDate { get; init; }
    public int? Rating { get; init; }
    public string? Notes { get; init; }
}

public class CreateListeningLogCommandHandler : IRequestHandler<CreateListeningLogCommand, ListeningLogDto>
{
    private readonly IMusicCollectionOrganizerContext _context;
    private readonly ILogger<CreateListeningLogCommandHandler> _logger;

    public CreateListeningLogCommandHandler(
        IMusicCollectionOrganizerContext context,
        ILogger<CreateListeningLogCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ListeningLogDto> Handle(CreateListeningLogCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating listening log for user {UserId}, album: {AlbumId}",
            request.UserId,
            request.AlbumId);

        var listeningLog = new ListeningLog
        {
            ListeningLogId = Guid.NewGuid(),
            UserId = request.UserId,
            AlbumId = request.AlbumId,
            ListeningDate = request.ListeningDate,
            Rating = request.Rating,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        _context.ListeningLogs.Add(listeningLog);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created listening log {ListeningLogId} for user {UserId}",
            listeningLog.ListeningLogId,
            request.UserId);

        return listeningLog.ToDto();
    }
}
