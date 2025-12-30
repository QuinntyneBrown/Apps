using RoadsideAssistanceInfoHub.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RoadsideAssistanceInfoHub.Api.Features.InsuranceInfos;

public record GetInsuranceInfosQuery : IRequest<IEnumerable<InsuranceInfoDto>>
{
    public Guid? VehicleId { get; init; }
    public string? InsuranceCompany { get; init; }
    public bool? IncludesRoadsideAssistance { get; init; }
}

public class GetInsuranceInfosQueryHandler : IRequestHandler<GetInsuranceInfosQuery, IEnumerable<InsuranceInfoDto>>
{
    private readonly IRoadsideAssistanceInfoHubContext _context;
    private readonly ILogger<GetInsuranceInfosQueryHandler> _logger;

    public GetInsuranceInfosQueryHandler(
        IRoadsideAssistanceInfoHubContext context,
        ILogger<GetInsuranceInfosQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<InsuranceInfoDto>> Handle(GetInsuranceInfosQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting insurance infos");

        var query = _context.InsuranceInfos.AsNoTracking();

        if (request.VehicleId.HasValue)
        {
            query = query.Where(i => i.VehicleId == request.VehicleId.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.InsuranceCompany))
        {
            query = query.Where(i => i.InsuranceCompany.Contains(request.InsuranceCompany));
        }

        if (request.IncludesRoadsideAssistance.HasValue)
        {
            query = query.Where(i => i.IncludesRoadsideAssistance == request.IncludesRoadsideAssistance.Value);
        }

        var insuranceInfos = await query
            .OrderBy(i => i.InsuranceCompany)
            .ThenBy(i => i.PolicyEndDate)
            .ToListAsync(cancellationToken);

        return insuranceInfos.Select(i => i.ToDto());
    }
}
