using Categories.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Categories.Api.Features;

public record GetCategoriesQuery : IRequest<IEnumerable<CategoryDto>>;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<CategoryDto>>
{
    private readonly ICategoriesDbContext _context;

    public GetCategoriesQueryHandler(ICategoriesDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _context.Categories.ToListAsync(cancellationToken);
        return categories.Select(c => c.ToDto());
    }
}
