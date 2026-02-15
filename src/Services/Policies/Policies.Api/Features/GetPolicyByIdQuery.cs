using Policies.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Policies.Api.Features;

public record GetPolicyByIdQuery(Guid PolicyId) : IRequest<PolicyDto?>;

public class GetPolicyByIdQueryHandler : IRequestHandler<GetPolicyByIdQuery, PolicyDto?>
{
    private readonly IPoliciesDbContext _context;
    public GetPolicyByIdQueryHandler(IPoliciesDbContext context) => _context = context;
    public async Task<PolicyDto?> Handle(GetPolicyByIdQuery request, CancellationToken ct)
    {
        var policy = await _context.Policies.AsNoTracking().FirstOrDefaultAsync(p => p.PolicyId == request.PolicyId, ct);
        return policy?.ToDto();
    }
}
