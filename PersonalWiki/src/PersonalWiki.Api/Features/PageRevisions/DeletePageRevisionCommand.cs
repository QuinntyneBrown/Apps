using PersonalWiki.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalWiki.Api.Features.PageRevisions;

public record DeletePageRevisionCommand : IRequest<bool>
{
    public Guid PageRevisionId { get; init; }
}

public class DeletePageRevisionCommandHandler : IRequestHandler<DeletePageRevisionCommand, bool>
{
    private readonly IPersonalWikiContext _context;
    private readonly ILogger<DeletePageRevisionCommandHandler> _logger;

    public DeletePageRevisionCommandHandler(
        IPersonalWikiContext context,
        ILogger<DeletePageRevisionCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeletePageRevisionCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting page revision {PageRevisionId}", request.PageRevisionId);

        var revision = await _context.Revisions
            .FirstOrDefaultAsync(r => r.PageRevisionId == request.PageRevisionId, cancellationToken);

        if (revision == null)
        {
            _logger.LogWarning("Page revision {PageRevisionId} not found", request.PageRevisionId);
            return false;
        }

        _context.Revisions.Remove(revision);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted page revision {PageRevisionId}", request.PageRevisionId);

        return true;
    }
}
