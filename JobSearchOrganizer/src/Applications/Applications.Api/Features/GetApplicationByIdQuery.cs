using Applications.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Applications.Api.Features;

public record GetApplicationByIdQuery(Guid ApplicationId) : IRequest<ApplicationDto?>;

public class GetApplicationByIdQueryHandler : IRequestHandler<GetApplicationByIdQuery, ApplicationDto?>
{
    private readonly IApplicationsDbContext _context;

    public GetApplicationByIdQueryHandler(IApplicationsDbContext context)
    {
        _context = context;
    }

    public async Task<ApplicationDto?> Handle(GetApplicationByIdQuery request, CancellationToken cancellationToken)
    {
        var application = await _context.Applications
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.ApplicationId == request.ApplicationId, cancellationToken);

        return application?.ToDto();
    }
}
