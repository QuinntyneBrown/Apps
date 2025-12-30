using PersonalWiki.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PersonalWiki.Api.Features.WikiCategories;

public record GetWikiCategoryByIdQuery : IRequest<WikiCategoryDto?>
{
    public Guid WikiCategoryId { get; init; }
}

public class GetWikiCategoryByIdQueryHandler : IRequestHandler<GetWikiCategoryByIdQuery, WikiCategoryDto?>
{
    private readonly IPersonalWikiContext _context;
    private readonly ILogger<GetWikiCategoryByIdQueryHandler> _logger;

    public GetWikiCategoryByIdQueryHandler(
        IPersonalWikiContext context,
        ILogger<GetWikiCategoryByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<WikiCategoryDto?> Handle(GetWikiCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting wiki category {WikiCategoryId}", request.WikiCategoryId);

        var category = await _context.Categories
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.WikiCategoryId == request.WikiCategoryId, cancellationToken);

        if (category == null)
        {
            _logger.LogWarning("Wiki category {WikiCategoryId} not found", request.WikiCategoryId);
            return null;
        }

        return category.ToDto();
    }
}
