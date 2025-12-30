using KnowledgeBaseSecondBrain.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KnowledgeBaseSecondBrain.Api.Features.NoteLinks;

public record GetNoteLinkByIdQuery : IRequest<NoteLinkDto?>
{
    public Guid NoteLinkId { get; init; }
}

public class GetNoteLinkByIdQueryHandler : IRequestHandler<GetNoteLinkByIdQuery, NoteLinkDto?>
{
    private readonly IKnowledgeBaseSecondBrainContext _context;
    private readonly ILogger<GetNoteLinkByIdQueryHandler> _logger;

    public GetNoteLinkByIdQueryHandler(
        IKnowledgeBaseSecondBrainContext context,
        ILogger<GetNoteLinkByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<NoteLinkDto?> Handle(GetNoteLinkByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting note link {NoteLinkId}", request.NoteLinkId);

        var noteLink = await _context.Links
            .AsNoTracking()
            .FirstOrDefaultAsync(nl => nl.NoteLinkId == request.NoteLinkId, cancellationToken);

        if (noteLink == null)
        {
            _logger.LogWarning("Note link {NoteLinkId} not found", request.NoteLinkId);
            return null;
        }

        return noteLink.ToDto();
    }
}
