using PersonalWiki.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalWiki.Api.Features.WikiPages;

public record GetWikiPagesQuery : IRequest<IEnumerable<WikiPageDto>>
{
    public Guid? UserId { get; init; }
    public Guid? CategoryId { get; init; }
    public PageStatus? Status { get; init; }
    public bool? IsFeatured { get; init; }
    public string? SearchTerm { get; init; }
}

public class GetWikiPagesQueryHandler : IRequestHandler<GetWikiPagesQuery, IEnumerable<WikiPageDto>>
{
    private readonly IPersonalWikiContext _context;
    private readonly ILogger<GetWikiPagesQueryHandler> _logger;

    public GetWikiPagesQueryHandler(
        IPersonalWikiContext context,
        ILogger<GetWikiPagesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<WikiPageDto>> Handle(GetWikiPagesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting wiki pages for user {UserId}", request.UserId);

        var query = _context.Pages.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(p => p.UserId == request.UserId.Value);
        }

        if (request.CategoryId.HasValue)
        {
            query = query.Where(p => p.CategoryId == request.CategoryId.Value);
        }

        if (request.Status.HasValue)
        {
            query = query.Where(p => p.Status == request.Status.Value);
        }

        if (request.IsFeatured.HasValue)
        {
            query = query.Where(p => p.IsFeatured == request.IsFeatured.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            query = query.Where(p => p.Title.Contains(request.SearchTerm) || p.Content.Contains(request.SearchTerm));
        }

        var pages = await query
            .OrderByDescending(p => p.LastModifiedAt)
            .ToListAsync(cancellationToken);

        return pages.Select(p => p.ToDto());
    }
}
