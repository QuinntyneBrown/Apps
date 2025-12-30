using KnowledgeBaseSecondBrain.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KnowledgeBaseSecondBrain.Api.Features.NoteLinks;

public record GetNoteLinksQuery : IRequest<IEnumerable<NoteLinkDto>>
{
    public Guid? SourceNoteId { get; init; }
    public Guid? TargetNoteId { get; init; }
}

public class GetNoteLinksQueryHandler : IRequestHandler<GetNoteLinksQuery, IEnumerable<NoteLinkDto>>
{
    private readonly IKnowledgeBaseSecondBrainContext _context;
    private readonly ILogger<GetNoteLinksQueryHandler> _logger;

    public GetNoteLinksQueryHandler(
        IKnowledgeBaseSecondBrainContext context,
        ILogger<GetNoteLinksQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<NoteLinkDto>> Handle(GetNoteLinksQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting note links");

        var query = _context.Links.AsNoTracking();

        if (request.SourceNoteId.HasValue)
        {
            query = query.Where(nl => nl.SourceNoteId == request.SourceNoteId.Value);
        }

        if (request.TargetNoteId.HasValue)
        {
            query = query.Where(nl => nl.TargetNoteId == request.TargetNoteId.Value);
        }

        var noteLinks = await query
            .OrderByDescending(nl => nl.CreatedAt)
            .ToListAsync(cancellationToken);

        return noteLinks.Select(nl => nl.ToDto());
    }
}
