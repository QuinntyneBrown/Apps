using Contributions.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Contributions.Api.Features;

public record GetContributionByIdQuery(Guid ContributionId) : IRequest<ContributionDto?>;

public class GetContributionByIdQueryHandler : IRequestHandler<GetContributionByIdQuery, ContributionDto?>
{
    private readonly IContributionsDbContext _context;
    public GetContributionByIdQueryHandler(IContributionsDbContext context) => _context = context;
    public async Task<ContributionDto?> Handle(GetContributionByIdQuery request, CancellationToken ct)
    {
        var contribution = await _context.Contributions.AsNoTracking().FirstOrDefaultAsync(c => c.ContributionId == request.ContributionId, ct);
        return contribution?.ToDto();
    }
}
