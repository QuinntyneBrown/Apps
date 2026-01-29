using Warranties.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Warranties.Api.Features;

public record GetWarrantiesQuery : IRequest<IEnumerable<WarrantyDto>>;

public class GetWarrantiesQueryHandler : IRequestHandler<GetWarrantiesQuery, IEnumerable<WarrantyDto>>
{
    private readonly IWarrantiesDbContext _context;

    public GetWarrantiesQueryHandler(IWarrantiesDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<WarrantyDto>> Handle(GetWarrantiesQuery request, CancellationToken cancellationToken)
    {
        var warranties = await _context.Warranties.ToListAsync(cancellationToken);
        return warranties.Select(w => w.ToDto());
    }
}
