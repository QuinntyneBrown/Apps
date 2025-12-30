using KnowledgeBaseSecondBrain.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KnowledgeBaseSecondBrain.Api.Features.NoteLinks;

public record DeleteNoteLinkCommand : IRequest<bool>
{
    public Guid NoteLinkId { get; init; }
}

public class DeleteNoteLinkCommandHandler : IRequestHandler<DeleteNoteLinkCommand, bool>
{
    private readonly IKnowledgeBaseSecondBrainContext _context;
    private readonly ILogger<DeleteNoteLinkCommandHandler> _logger;

    public DeleteNoteLinkCommandHandler(
        IKnowledgeBaseSecondBrainContext context,
        ILogger<DeleteNoteLinkCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteNoteLinkCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting note link {NoteLinkId}", request.NoteLinkId);

        var noteLink = await _context.Links
            .FirstOrDefaultAsync(nl => nl.NoteLinkId == request.NoteLinkId, cancellationToken);

        if (noteLink == null)
        {
            _logger.LogWarning("Note link {NoteLinkId} not found", request.NoteLinkId);
            return false;
        }

        _context.Links.Remove(noteLink);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted note link {NoteLinkId}", request.NoteLinkId);

        return true;
    }
}
