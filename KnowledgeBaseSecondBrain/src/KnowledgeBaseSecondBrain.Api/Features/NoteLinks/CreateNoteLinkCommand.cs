using KnowledgeBaseSecondBrain.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KnowledgeBaseSecondBrain.Api.Features.NoteLinks;

public record CreateNoteLinkCommand : IRequest<NoteLinkDto>
{
    public Guid SourceNoteId { get; init; }
    public Guid TargetNoteId { get; init; }
    public string? Description { get; init; }
    public string? LinkType { get; init; }
}

public class CreateNoteLinkCommandHandler : IRequestHandler<CreateNoteLinkCommand, NoteLinkDto>
{
    private readonly IKnowledgeBaseSecondBrainContext _context;
    private readonly ILogger<CreateNoteLinkCommandHandler> _logger;

    public CreateNoteLinkCommandHandler(
        IKnowledgeBaseSecondBrainContext context,
        ILogger<CreateNoteLinkCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<NoteLinkDto> Handle(CreateNoteLinkCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating note link from {SourceNoteId} to {TargetNoteId}",
            request.SourceNoteId,
            request.TargetNoteId);

        var noteLink = new NoteLink
        {
            NoteLinkId = Guid.NewGuid(),
            SourceNoteId = request.SourceNoteId,
            TargetNoteId = request.TargetNoteId,
            Description = request.Description,
            LinkType = request.LinkType,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Links.Add(noteLink);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created note link {NoteLinkId}",
            noteLink.NoteLinkId);

        return noteLink.ToDto();
    }
}
