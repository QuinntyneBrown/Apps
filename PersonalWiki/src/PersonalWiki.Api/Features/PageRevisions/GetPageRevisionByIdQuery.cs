using PersonalWiki.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalWiki.Api.Features.PageRevisions;

public record GetPageRevisionByIdQuery : IRequest<PageRevisionDto?>
{
    public Guid PageRevisionId { get; init; }
}

public class GetPageRevisionByIdQueryHandler : IRequestHandler<GetPageRevisionByIdQuery, PageRevisionDto?>
{
    private readonly IPersonalWikiContext _context;
    private readonly ILogger<GetPageRevisionByIdQueryHandler> _logger;

    public GetPageRevisionByIdQueryHandler(
        IPersonalWikiContext context,
        ILogger<GetPageRevisionByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PageRevisionDto?> Handle(GetPageRevisionByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting page revision {PageRevisionId}", request.PageRevisionId);

        var revision = await _context.Revisions
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.PageRevisionId == request.PageRevisionId, cancellationToken);

        if (revision == null)
        {
            _logger.LogWarning("Page revision {PageRevisionId} not found", request.PageRevisionId);
            return null;
        }

        return revision.ToDto();
    }
}
