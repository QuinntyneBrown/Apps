using MediatR;
using Microsoft.EntityFrameworkCore;
using WarrantyReturnPeriodTracker.Core;

namespace WarrantyReturnPeriodTracker.Api.Features.ReturnWindows;

public class GetReturnWindowsQuery : IRequest<List<ReturnWindowDto>>
{
}

public class GetReturnWindowsQueryHandler : IRequestHandler<GetReturnWindowsQuery, List<ReturnWindowDto>>
{
    private readonly IWarrantyReturnPeriodTrackerContext _context;

    public GetReturnWindowsQueryHandler(IWarrantyReturnPeriodTrackerContext context)
    {
        _context = context;
    }

    public async Task<List<ReturnWindowDto>> Handle(GetReturnWindowsQuery request, CancellationToken cancellationToken)
    {
        return await _context.ReturnWindows
            .AsNoTracking()
            .OrderByDescending(rw => rw.StartDate)
            .Select(rw => rw.ToDto())
            .ToListAsync(cancellationToken);
    }
}
