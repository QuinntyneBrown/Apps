using Categories.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Categories.Api.Features;

public record GetCategoryByIdQuery(Guid CategoryId) : IRequest<CategoryDto?>;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto?>
{
    private readonly ICategoriesDbContext _context;

    public GetCategoryByIdQueryHandler(ICategoriesDbContext context)
    {
        _context = context;
    }

    public async Task<CategoryDto?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.CategoryId == request.CategoryId, cancellationToken);
        return category?.ToDto();
    }
}
