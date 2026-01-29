using Policies.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Policies.Api.Features;

public record GetPoliciesQuery : IRequest<IEnumerable<PolicyDto>>;

public class GetPoliciesQueryHandler : IRequestHandler<GetPoliciesQuery, IEnumerable<PolicyDto>>
{
    private readonly IPoliciesDbContext _context;
    public GetPoliciesQueryHandler(IPoliciesDbContext context) => _context = context;
    public async Task<IEnumerable<PolicyDto>> Handle(GetPoliciesQuery request, CancellationToken ct) => await _context.Policies.AsNoTracking().Select(p => p.ToDto()).ToListAsync(ct);
}
