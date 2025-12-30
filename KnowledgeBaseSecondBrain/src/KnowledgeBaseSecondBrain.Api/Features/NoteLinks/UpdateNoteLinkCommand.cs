using KnowledgeBaseSecondBrain.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KnowledgeBaseSecondBrain.Api.Features.NoteLinks;

public record UpdateNoteLinkCommand : IRequest<NoteLinkDto?>
{
    public Guid NoteLinkId { get; init; }
    public string? Description { get; init; }
    public string? LinkType { get; init; }
}

public class UpdateNoteLinkCommandHandler : IRequestHandler<UpdateNoteLinkCommand, NoteLinkDto?>
{
    private readonly IKnowledgeBaseSecondBrainContext _context;
    private readonly ILogger<UpdateNoteLinkCommandHandler> _logger;

    public UpdateNoteLinkCommandHandler(
        IKnowledgeBaseSecondBrainContext context,
        ILogger<UpdateNoteLinkCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<NoteLinkDto?> Handle(UpdateNoteLinkCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating note link {NoteLinkId}", request.NoteLinkId);

        var noteLink = await _context.Links
            .FirstOrDefaultAsync(nl => nl.NoteLinkId == request.NoteLinkId, cancellationToken);

        if (noteLink == null)
        {
            _logger.LogWarning("Note link {NoteLinkId} not found", request.NoteLinkId);
            return null;
        }

        noteLink.Description = request.Description;
        noteLink.LinkType = request.LinkType;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated note link {NoteLinkId}", request.NoteLinkId);

        return noteLink.ToDto();
    }
}
