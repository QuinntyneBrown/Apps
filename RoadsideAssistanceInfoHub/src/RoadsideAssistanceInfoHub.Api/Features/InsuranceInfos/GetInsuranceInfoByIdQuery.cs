using RoadsideAssistanceInfoHub.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace RoadsideAssistanceInfoHub.Api.Features.InsuranceInfos;

public record GetInsuranceInfoByIdQuery : IRequest<InsuranceInfoDto?>
{
    public Guid InsuranceInfoId { get; init; }
}

public class GetInsuranceInfoByIdQueryHandler : IRequestHandler<GetInsuranceInfoByIdQuery, InsuranceInfoDto?>
{
    private readonly IRoadsideAssistanceInfoHubContext _context;
    private readonly ILogger<GetInsuranceInfoByIdQueryHandler> _logger;

    public GetInsuranceInfoByIdQueryHandler(
        IRoadsideAssistanceInfoHubContext context,
        ILogger<GetInsuranceInfoByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<InsuranceInfoDto?> Handle(GetInsuranceInfoByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting insurance info {InsuranceInfoId}", request.InsuranceInfoId);

        var insuranceInfo = await _context.InsuranceInfos
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.InsuranceInfoId == request.InsuranceInfoId, cancellationToken);

        if (insuranceInfo == null)
        {
            _logger.LogWarning("Insurance info {InsuranceInfoId} not found", request.InsuranceInfoId);
            return null;
        }

        return insuranceInfo.ToDto();
    }
}
