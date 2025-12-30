using MediatR;
using Microsoft.EntityFrameworkCore;
using WarrantyReturnPeriodTracker.Core;

namespace WarrantyReturnPeriodTracker.Api.Features.Warranties;

public class GetWarrantyByIdQuery : IRequest<WarrantyDto?>
{
    public Guid WarrantyId { get; set; }
}

public class GetWarrantyByIdQueryHandler : IRequestHandler<GetWarrantyByIdQuery, WarrantyDto?>
{
    private readonly IWarrantyReturnPeriodTrackerContext _context;

    public GetWarrantyByIdQueryHandler(IWarrantyReturnPeriodTrackerContext context)
    {
        _context = context;
    }

    public async Task<WarrantyDto?> Handle(GetWarrantyByIdQuery request, CancellationToken cancellationToken)
    {
        var warranty = await _context.Warranties
            .AsNoTracking()
            .FirstOrDefaultAsync(w => w.WarrantyId == request.WarrantyId, cancellationToken);

        return warranty?.ToDto();
    }
}
