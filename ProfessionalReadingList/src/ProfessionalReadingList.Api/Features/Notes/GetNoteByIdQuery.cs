using ProfessionalReadingList.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProfessionalReadingList.Api.Features.Notes;

public record GetNoteByIdQuery : IRequest<NoteDto?>
{
    public Guid NoteId { get; init; }
}

public class GetNoteByIdQueryHandler : IRequestHandler<GetNoteByIdQuery, NoteDto?>
{
    private readonly IProfessionalReadingListContext _context;
    private readonly ILogger<GetNoteByIdQueryHandler> _logger;

    public GetNoteByIdQueryHandler(
        IProfessionalReadingListContext context,
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

        return note?.ToDto();
    }
}
