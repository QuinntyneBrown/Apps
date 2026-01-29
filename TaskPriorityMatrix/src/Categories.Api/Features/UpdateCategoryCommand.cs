using Categories.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Categories.Api.Features;

public record UpdateCategoryCommand(Guid CategoryId, string? Name, string? Color) : IRequest<CategoryDto?>;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryDto?>
{
    private readonly ICategoriesDbContext _context;
    private readonly ILogger<UpdateCategoryCommandHandler> _logger;

    public UpdateCategoryCommandHandler(ICategoriesDbContext context, ILogger<UpdateCategoryCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CategoryDto?> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.CategoryId == request.CategoryId, cancellationToken);

        if (category == null) return null;

        if (request.Name != null) category.Name = request.Name;
        if (request.Color != null) category.Color = request.Color;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Category updated: {CategoryId}", category.CategoryId);

        return category.ToDto();
    }
}
