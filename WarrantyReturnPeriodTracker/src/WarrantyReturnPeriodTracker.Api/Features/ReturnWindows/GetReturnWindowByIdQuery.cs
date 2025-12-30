using MediatR;
using Microsoft.EntityFrameworkCore;
using WarrantyReturnPeriodTracker.Core;

namespace WarrantyReturnPeriodTracker.Api.Features.ReturnWindows;

public class GetReturnWindowByIdQuery : IRequest<ReturnWindowDto?>
{
    public Guid ReturnWindowId { get; set; }
}

public class GetReturnWindowByIdQueryHandler : IRequestHandler<GetReturnWindowByIdQuery, ReturnWindowDto?>
{
    private readonly IWarrantyReturnPeriodTrackerContext _context;

    public GetReturnWindowByIdQueryHandler(IWarrantyReturnPeriodTrackerContext context)
    {
        _context = context;
    }

    public async Task<ReturnWindowDto?> Handle(GetReturnWindowByIdQuery request, CancellationToken cancellationToken)
    {
        var returnWindow = await _context.ReturnWindows
            .AsNoTracking()
            .FirstOrDefaultAsync(rw => rw.ReturnWindowId == request.ReturnWindowId, cancellationToken);

        return returnWindow?.ToDto();
    }
}
