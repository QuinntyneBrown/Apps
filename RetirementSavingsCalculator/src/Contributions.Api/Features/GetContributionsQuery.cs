using Contributions.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Contributions.Api.Features;

public record GetContributionsQuery : IRequest<IEnumerable<ContributionDto>>;

public class GetContributionsQueryHandler : IRequestHandler<GetContributionsQuery, IEnumerable<ContributionDto>>
{
    private readonly IContributionsDbContext _context;
    public GetContributionsQueryHandler(IContributionsDbContext context) => _context = context;
    public async Task<IEnumerable<ContributionDto>> Handle(GetContributionsQuery request, CancellationToken ct) => await _context.Contributions.AsNoTracking().Select(c => c.ToDto()).ToListAsync(ct);
}
