using ProfessionalReadingList.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ProfessionalReadingList.Api.Features.Notes;

public record CreateNoteCommand : IRequest<NoteDto>
{
    public Guid UserId { get; init; }
    public Guid ResourceId { get; init; }
    public string Content { get; init; } = string.Empty;
    public string? PageReference { get; init; }
    public string? Quote { get; init; }
    public List<string> Tags { get; init; } = new List<string>();
}

public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, NoteDto>
{
    private readonly IProfessionalReadingListContext _context;
    private readonly ILogger<CreateNoteCommandHandler> _logger;

    public CreateNoteCommandHandler(
        IProfessionalReadingListContext context,
        ILogger<CreateNoteCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<NoteDto> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating note for user {UserId}, resource {ResourceId}",
            request.UserId,
            request.ResourceId);

        var note = new Note
        {
            NoteId = Guid.NewGuid(),
            UserId = request.UserId,
            ResourceId = request.ResourceId,
            Content = request.Content,
            PageReference = request.PageReference,
            Quote = request.Quote,
            Tags = request.Tags,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Notes.Add(note);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created note {NoteId} for user {UserId}",
            note.NoteId,
            request.UserId);

        return note.ToDto();
    }
}
