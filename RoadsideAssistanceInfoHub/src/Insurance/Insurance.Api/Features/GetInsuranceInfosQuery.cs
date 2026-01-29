using Insurance.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Insurance.Api.Features;

public record GetInsuranceInfosQuery : IRequest<IEnumerable<InsuranceInfoDto>>;

public class GetInsuranceInfosQueryHandler : IRequestHandler<GetInsuranceInfosQuery, IEnumerable<InsuranceInfoDto>>
{
    private readonly IInsuranceDbContext _context;
    public GetInsuranceInfosQueryHandler(IInsuranceDbContext context) => _context = context;
    public async Task<IEnumerable<InsuranceInfoDto>> Handle(GetInsuranceInfosQuery request, CancellationToken ct) => await _context.InsuranceInfos.AsNoTracking().Select(i => i.ToDto()).ToListAsync(ct);
}
