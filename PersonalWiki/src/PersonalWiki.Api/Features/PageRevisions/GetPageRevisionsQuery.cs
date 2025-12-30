using PersonalWiki.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalWiki.Api.Features.PageRevisions;

public record GetPageRevisionsQuery : IRequest<IEnumerable<PageRevisionDto>>
{
    public Guid? WikiPageId { get; init; }
}

public class GetPageRevisionsQueryHandler : IRequestHandler<GetPageRevisionsQuery, IEnumerable<PageRevisionDto>>
{
    private readonly IPersonalWikiContext _context;
    private readonly ILogger<GetPageRevisionsQueryHandler> _logger;

    public GetPageRevisionsQueryHandler(
        IPersonalWikiContext context,
        ILogger<GetPageRevisionsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<PageRevisionDto>> Handle(GetPageRevisionsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting page revisions for page {WikiPageId}", request.WikiPageId);

        var query = _context.Revisions.AsNoTracking();

        if (request.WikiPageId.HasValue)
        {
            query = query.Where(r => r.WikiPageId == request.WikiPageId.Value);
        }

        var revisions = await query
            .OrderByDescending(r => r.Version)
            .ToListAsync(cancellationToken);

        return revisions.Select(r => r.ToDto());
    }
}
