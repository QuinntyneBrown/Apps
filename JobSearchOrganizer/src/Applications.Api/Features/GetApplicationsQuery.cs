using Applications.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Applications.Api.Features;

public record GetApplicationsQuery : IRequest<IEnumerable<ApplicationDto>>;

public class GetApplicationsQueryHandler : IRequestHandler<GetApplicationsQuery, IEnumerable<ApplicationDto>>
{
    private readonly IApplicationsDbContext _context;

    public GetApplicationsQueryHandler(IApplicationsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ApplicationDto>> Handle(GetApplicationsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Applications
            .AsNoTracking()
            .Select(a => a.ToDto())
            .ToListAsync(cancellationToken);
    }
}
