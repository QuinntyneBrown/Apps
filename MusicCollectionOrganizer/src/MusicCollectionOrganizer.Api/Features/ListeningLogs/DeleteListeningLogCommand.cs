using MusicCollectionOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MusicCollectionOrganizer.Api.Features.ListeningLogs;

public record DeleteListeningLogCommand : IRequest<bool>
{
    public Guid ListeningLogId { get; init; }
}

public class DeleteListeningLogCommandHandler : IRequestHandler<DeleteListeningLogCommand, bool>
{
    private readonly IMusicCollectionOrganizerContext _context;
    private readonly ILogger<DeleteListeningLogCommandHandler> _logger;

    public DeleteListeningLogCommandHandler(
        IMusicCollectionOrganizerContext context,
        ILogger<DeleteListeningLogCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteListeningLogCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting listening log {ListeningLogId}", request.ListeningLogId);

        var listeningLog = await _context.ListeningLogs
            .FirstOrDefaultAsync(l => l.ListeningLogId == request.ListeningLogId, cancellationToken);

        if (listeningLog == null)
        {
            _logger.LogWarning("Listening log {ListeningLogId} not found", request.ListeningLogId);
            return false;
        }

        _context.ListeningLogs.Remove(listeningLog);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted listening log {ListeningLogId}", request.ListeningLogId);

        return true;
    }
}
