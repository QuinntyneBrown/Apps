using SubscriptionAuditTool.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SubscriptionAuditTool.Api.Features.Categories;

public record GetCategoryByIdQuery : IRequest<CategoryDto?>
{
    public Guid CategoryId { get; init; }
}

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto?>
{
    private readonly ISubscriptionAuditToolContext _context;
    private readonly ILogger<GetCategoryByIdQueryHandler> _logger;

    public GetCategoryByIdQueryHandler(
        ISubscriptionAuditToolContext context,
        ILogger<GetCategoryByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CategoryDto?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Getting category {CategoryId}",
            request.CategoryId);

        var category = await _context.Categories
            .Include(c => c.Subscriptions)
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CategoryId == request.CategoryId, cancellationToken);

        if (category == null)
        {
            _logger.LogWarning(
                "Category {CategoryId} not found",
                request.CategoryId);
            return null;
        }

        return category.ToDto();
    }
}
