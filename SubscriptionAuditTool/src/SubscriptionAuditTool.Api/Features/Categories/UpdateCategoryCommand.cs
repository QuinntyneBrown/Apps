using SubscriptionAuditTool.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SubscriptionAuditTool.Api.Features.Categories;

public record UpdateCategoryCommand : IRequest<CategoryDto?>
{
    public Guid CategoryId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public string? ColorCode { get; init; }
}

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryDto?>
{
    private readonly ISubscriptionAuditToolContext _context;
    private readonly ILogger<UpdateCategoryCommandHandler> _logger;

    public UpdateCategoryCommandHandler(
        ISubscriptionAuditToolContext context,
        ILogger<UpdateCategoryCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CategoryDto?> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Updating category {CategoryId}",
            request.CategoryId);

        var category = await _context.Categories
            .Include(c => c.Subscriptions)
            .FirstOrDefaultAsync(c => c.CategoryId == request.CategoryId, cancellationToken);

        if (category == null)
        {
            _logger.LogWarning(
                "Category {CategoryId} not found",
                request.CategoryId);
            return null;
        }

        category.Name = request.Name;
        category.Description = request.Description;
        category.ColorCode = request.ColorCode;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Updated category {CategoryId}",
            request.CategoryId);

        return category.ToDto();
    }
}
