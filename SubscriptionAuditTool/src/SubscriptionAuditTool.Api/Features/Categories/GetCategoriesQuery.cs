using SubscriptionAuditTool.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SubscriptionAuditTool.Api.Features.Categories;

public record GetCategoriesQuery : IRequest<IEnumerable<CategoryDto>>
{
}

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<CategoryDto>>
{
    private readonly ISubscriptionAuditToolContext _context;
    private readonly ILogger<GetCategoriesQueryHandler> _logger;

    public GetCategoriesQueryHandler(
        ISubscriptionAuditToolContext context,
        ILogger<GetCategoriesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting categories");

        var categories = await _context.Categories
            .Include(c => c.Subscriptions)
            .AsNoTracking()
            .OrderBy(c => c.Name)
            .ToListAsync(cancellationToken);

        return categories.Select(c => c.ToDto());
    }
}
