using Appliances.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Appliances.Api.Features;

public record GetAppliancesQuery : IRequest<IEnumerable<ApplianceDto>>;

public class GetAppliancesQueryHandler : IRequestHandler<GetAppliancesQuery, IEnumerable<ApplianceDto>>
{
    private readonly IAppliancesDbContext _context;

    public GetAppliancesQueryHandler(IAppliancesDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ApplianceDto>> Handle(GetAppliancesQuery request, CancellationToken cancellationToken)
    {
        var appliances = await _context.Appliances.ToListAsync(cancellationToken);
        return appliances.Select(a => a.ToDto());
    }
}
