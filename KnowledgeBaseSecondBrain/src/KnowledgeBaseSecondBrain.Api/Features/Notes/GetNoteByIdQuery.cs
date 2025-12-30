using KnowledgeBaseSecondBrain.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KnowledgeBaseSecondBrain.Api.Features.Notes;

public record GetNoteByIdQuery : IRequest<NoteDto?>
{
    public Guid NoteId { get; init; }
}

public class GetNoteByIdQueryHandler : IRequestHandler<GetNoteByIdQuery, NoteDto?>
{
    private readonly IKnowledgeBaseSecondBrainContext _context;
    private readonly ILogger<GetNoteByIdQueryHandler> _logger;

    public GetNoteByIdQueryHandler(
        IKnowledgeBaseSecondBrainContext context,
        ILogger<GetNoteByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<NoteDto?> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting note {NoteId}", request.NoteId);

        var note = await _context.Notes
            .AsNoTracking()
            .FirstOrDefaultAsync(n => n.NoteId == request.NoteId, cancellationToken);

        if (note == null)
        {
            _logger.LogWarning("Note {NoteId} not found", request.NoteId);
            return null;
        }

        return note.ToDto();
    }
}
