using MusicCollectionOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MusicCollectionOrganizer.Api.Features.ListeningLogs;

public record UpdateListeningLogCommand : IRequest<ListeningLogDto?>
{
    public Guid ListeningLogId { get; init; }
    public DateTime ListeningDate { get; init; }
    public int? Rating { get; init; }
    public string? Notes { get; init; }
}

public class UpdateListeningLogCommandHandler : IRequestHandler<UpdateListeningLogCommand, ListeningLogDto?>
{
    private readonly IMusicCollectionOrganizerContext _context;
    private readonly ILogger<UpdateListeningLogCommandHandler> _logger;

    public UpdateListeningLogCommandHandler(
        IMusicCollectionOrganizerContext context,
        ILogger<UpdateListeningLogCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ListeningLogDto?> Handle(UpdateListeningLogCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating listening log {ListeningLogId}", request.ListeningLogId);

        var listeningLog = await _context.ListeningLogs
            .FirstOrDefaultAsync(l => l.ListeningLogId == request.ListeningLogId, cancellationToken);

        if (listeningLog == null)
        {
            _logger.LogWarning("Listening log {ListeningLogId} not found", request.ListeningLogId);
            return null;
        }

        listeningLog.ListeningDate = request.ListeningDate;
        listeningLog.Rating = request.Rating;
        listeningLog.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated listening log {ListeningLogId}", request.ListeningLogId);

        return listeningLog.ToDto();
    }
}
