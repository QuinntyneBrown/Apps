using MediatR;
using Microsoft.EntityFrameworkCore;
using WarrantyReturnPeriodTracker.Core;

namespace WarrantyReturnPeriodTracker.Api.Features.Warranties;

public class GetWarrantiesQuery : IRequest<List<WarrantyDto>>
{
}

public class GetWarrantiesQueryHandler : IRequestHandler<GetWarrantiesQuery, List<WarrantyDto>>
{
    private readonly IWarrantyReturnPeriodTrackerContext _context;

    public GetWarrantiesQueryHandler(IWarrantyReturnPeriodTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<WarrantyDto>> Handle(GetWarrantiesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Warranties
            .AsNoTracking()
            .OrderByDescending(w => w.StartDate)
            .Select(w => w.ToDto())
            .ToListAsync(cancellationToken);
    }
}
