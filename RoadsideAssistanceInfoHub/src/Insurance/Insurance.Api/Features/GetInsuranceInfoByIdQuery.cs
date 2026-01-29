using Insurance.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Insurance.Api.Features;

public record GetInsuranceInfoByIdQuery(Guid InsuranceInfoId) : IRequest<InsuranceInfoDto?>;

public class GetInsuranceInfoByIdQueryHandler : IRequestHandler<GetInsuranceInfoByIdQuery, InsuranceInfoDto?>
{
    private readonly IInsuranceDbContext _context;
    public GetInsuranceInfoByIdQueryHandler(IInsuranceDbContext context) => _context = context;
    public async Task<InsuranceInfoDto?> Handle(GetInsuranceInfoByIdQuery request, CancellationToken ct)
    {
        var insuranceInfo = await _context.InsuranceInfos.AsNoTracking().FirstOrDefaultAsync(i => i.InsuranceInfoId == request.InsuranceInfoId, ct);
        return insuranceInfo?.ToDto();
    }
}
