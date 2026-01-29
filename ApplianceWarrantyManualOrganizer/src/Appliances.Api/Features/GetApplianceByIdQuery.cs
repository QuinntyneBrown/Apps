using Appliances.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appliances.Api.Features;

public record GetApplianceByIdQuery(Guid ApplianceId) : IRequest<ApplianceDto?>;

public class GetApplianceByIdQueryHandler : IRequestHandler<GetApplianceByIdQuery, ApplianceDto?>
{
    private readonly IAppliancesDbContext _context;

    public GetApplianceByIdQueryHandler(IAppliancesDbContext context)
    {
        _context = context;
    }

    public async Task<ApplianceDto?> Handle(GetApplianceByIdQuery request, CancellationToken cancellationToken)
    {
        var appliance = await _context.Appliances
            .FirstOrDefaultAsync(a => a.ApplianceId == request.ApplianceId, cancellationToken);
        return appliance?.ToDto();
    }
}
