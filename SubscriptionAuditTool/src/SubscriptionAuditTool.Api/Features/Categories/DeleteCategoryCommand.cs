using SubscriptionAuditTool.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SubscriptionAuditTool.Api.Features.Categories;

public record DeleteCategoryCommand : IRequest<bool>
{
    public Guid CategoryId { get; init; }
}

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
{
    private readonly ISubscriptionAuditToolContext _context;
    private readonly ILogger<DeleteCategoryCommandHandler> _logger;

    public DeleteCategoryCommandHandler(
        ISubscriptionAuditToolContext context,
        ILogger<DeleteCategoryCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Deleting category {CategoryId}",
            request.CategoryId);

        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.CategoryId == request.CategoryId, cancellationToken);

        if (category == null)
        {
            _logger.LogWarning(
                "Category {CategoryId} not found",
                request.CategoryId);
            return false;
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Deleted category {CategoryId}",
            request.CategoryId);

        return true;
    }
}
