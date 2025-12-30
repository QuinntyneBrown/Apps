using PersonalWiki.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalWiki.Api.Features.WikiCategories;

public record GetWikiCategoriesQuery : IRequest<IEnumerable<WikiCategoryDto>>
{
    public Guid? UserId { get; init; }
    public Guid? ParentCategoryId { get; init; }
    public bool? IncludeRootOnly { get; init; }
}

public class GetWikiCategoriesQueryHandler : IRequestHandler<GetWikiCategoriesQuery, IEnumerable<WikiCategoryDto>>
{
    private readonly IPersonalWikiContext _context;
    private readonly ILogger<GetWikiCategoriesQueryHandler> _logger;

    public GetWikiCategoriesQueryHandler(
        IPersonalWikiContext context,
        ILogger<GetWikiCategoriesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<WikiCategoryDto>> Handle(GetWikiCategoriesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting wiki categories for user {UserId}", request.UserId);

        var query = _context.Categories.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(c => c.UserId == request.UserId.Value);
        }

        if (request.ParentCategoryId.HasValue)
        {
            query = query.Where(c => c.ParentCategoryId == request.ParentCategoryId.Value);
        }

        if (request.IncludeRootOnly == true)
        {
            query = query.Where(c => c.ParentCategoryId == null);
        }

        var categories = await query
            .OrderBy(c => c.Name)
            .ToListAsync(cancellationToken);

        return categories.Select(c => c.ToDto());
    }
}
