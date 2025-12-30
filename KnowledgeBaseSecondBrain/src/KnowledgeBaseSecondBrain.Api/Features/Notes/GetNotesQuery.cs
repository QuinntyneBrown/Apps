using KnowledgeBaseSecondBrain.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KnowledgeBaseSecondBrain.Api.Features.Notes;

public record GetNotesQuery : IRequest<IEnumerable<NoteDto>>
{
    public Guid? UserId { get; init; }
    public NoteType? NoteType { get; init; }
    public bool? IsFavorite { get; init; }
    public bool? IsArchived { get; init; }
}

public class GetNotesQueryHandler : IRequestHandler<GetNotesQuery, IEnumerable<NoteDto>>
{
    private readonly IKnowledgeBaseSecondBrainContext _context;
    private readonly ILogger<GetNotesQueryHandler> _logger;

    public GetNotesQueryHandler(
        IKnowledgeBaseSecondBrainContext context,
        ILogger<GetNotesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<NoteDto>> Handle(GetNotesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting notes for user {UserId}", request.UserId);

        var query = _context.Notes.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(n => n.UserId == request.UserId.Value);
        }

        if (request.NoteType.HasValue)
        {
            query = query.Where(n => n.NoteType == request.NoteType.Value);
        }

        if (request.IsFavorite.HasValue)
        {
            query = query.Where(n => n.IsFavorite == request.IsFavorite.Value);
        }

        if (request.IsArchived.HasValue)
        {
            query = query.Where(n => n.IsArchived == request.IsArchived.Value);
        }

        var notes = await query
            .OrderByDescending(n => n.LastModifiedAt)
            .ToListAsync(cancellationToken);

        return notes.Select(n => n.ToDto());
    }
}
