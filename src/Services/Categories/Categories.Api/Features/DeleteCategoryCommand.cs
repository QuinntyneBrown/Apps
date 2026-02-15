using Categories.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Categories.Api.Features;

public record DeleteCategoryCommand(Guid CategoryId) : IRequest<bool>;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, bool>
{
    private readonly ICategoriesDbContext _context;
    private readonly ILogger<DeleteCategoryCommandHandler> _logger;

    public DeleteCategoryCommandHandler(ICategoriesDbContext context, ILogger<DeleteCategoryCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.CategoryId == request.CategoryId, cancellationToken);

        if (category == null) return false;

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Category deleted: {CategoryId}", request.CategoryId);

        return true;
    }
}
