using PersonalWiki.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalWiki.Api.Features.PageLinks;

public record GetPageLinkByIdQuery : IRequest<PageLinkDto?>
{
    public Guid PageLinkId { get; init; }
}

public class GetPageLinkByIdQueryHandler : IRequestHandler<GetPageLinkByIdQuery, PageLinkDto?>
{
    private readonly IPersonalWikiContext _context;
    private readonly ILogger<GetPageLinkByIdQueryHandler> _logger;

    public GetPageLinkByIdQueryHandler(
        IPersonalWikiContext context,
        ILogger<GetPageLinkByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<PageLinkDto?> Handle(GetPageLinkByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting page link {PageLinkId}", request.PageLinkId);

        var link = await _context.Links
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.PageLinkId == request.PageLinkId, cancellationToken);

        if (link == null)
        {
            _logger.LogWarning("Page link {PageLinkId} not found", request.PageLinkId);
            return null;
        }

        return link.ToDto();
    }
}
