using PersonalWiki.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalWiki.Api.Features.PageLinks;

public record GetPageLinksQuery : IRequest<IEnumerable<PageLinkDto>>
{
    public Guid? SourcePageId { get; init; }
    public Guid? TargetPageId { get; init; }
}

public class GetPageLinksQueryHandler : IRequestHandler<GetPageLinksQuery, IEnumerable<PageLinkDto>>
{
    private readonly IPersonalWikiContext _context;
    private readonly ILogger<GetPageLinksQueryHandler> _logger;

    public GetPageLinksQueryHandler(
        IPersonalWikiContext context,
        ILogger<GetPageLinksQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<PageLinkDto>> Handle(GetPageLinksQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting page links");

        var query = _context.Links.AsNoTracking();

        if (request.SourcePageId.HasValue)
        {
            query = query.Where(l => l.SourcePageId == request.SourcePageId.Value);
        }

        if (request.TargetPageId.HasValue)
        {
            query = query.Where(l => l.TargetPageId == request.TargetPageId.Value);
        }

        var links = await query
            .OrderByDescending(l => l.CreatedAt)
            .ToListAsync(cancellationToken);

        return links.Select(l => l.ToDto());
    }
}
