using PersonalWiki.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalWiki.Api.Features.WikiPages;

public record GetWikiPageByIdQuery : IRequest<WikiPageDto?>
{
    public Guid WikiPageId { get; init; }
}

public class GetWikiPageByIdQueryHandler : IRequestHandler<GetWikiPageByIdQuery, WikiPageDto?>
{
    private readonly IPersonalWikiContext _context;
    private readonly ILogger<GetWikiPageByIdQueryHandler> _logger;

    public GetWikiPageByIdQueryHandler(
        IPersonalWikiContext context,
        ILogger<GetWikiPageByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<WikiPageDto?> Handle(GetWikiPageByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting wiki page {WikiPageId}", request.WikiPageId);

        var page = await _context.Pages
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.WikiPageId == request.WikiPageId, cancellationToken);

        if (page == null)
        {
            _logger.LogWarning("Wiki page {WikiPageId} not found", request.WikiPageId);
            return null;
        }

        return page.ToDto();
    }
}
