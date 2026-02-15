using Warranties.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Warranties.Api.Features;

public record GetWarrantyByIdQuery(Guid WarrantyId) : IRequest<WarrantyDto?>;

public class GetWarrantyByIdQueryHandler : IRequestHandler<GetWarrantyByIdQuery, WarrantyDto?>
{
    private readonly IWarrantiesDbContext _context;

    public GetWarrantyByIdQueryHandler(IWarrantiesDbContext context)
    {
        _context = context;
    }

    public async Task<WarrantyDto?> Handle(GetWarrantyByIdQuery request, CancellationToken cancellationToken)
    {
        var warranty = await _context.Warranties
            .FirstOrDefaultAsync(w => w.WarrantyId == request.WarrantyId, cancellationToken);
        return warranty?.ToDto();
    }
}
