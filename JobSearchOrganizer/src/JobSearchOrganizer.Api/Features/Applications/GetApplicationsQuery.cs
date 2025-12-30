using JobSearchOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JobSearchOrganizer.Api.Features.Applications;

public record GetApplicationsQuery : IRequest<IEnumerable<ApplicationDto>>
{
    public Guid? UserId { get; init; }
    public Guid? CompanyId { get; init; }
    public ApplicationStatus? Status { get; init; }
    public bool? IsRemote { get; init; }
}

public class GetApplicationsQueryHandler : IRequestHandler<GetApplicationsQuery, IEnumerable<ApplicationDto>>
{
    private readonly IJobSearchOrganizerContext _context;
    private readonly ILogger<GetApplicationsQueryHandler> _logger;

    public GetApplicationsQueryHandler(
        IJobSearchOrganizerContext context,
        ILogger<GetApplicationsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<ApplicationDto>> Handle(GetApplicationsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting applications for user {UserId}", request.UserId);

        var query = _context.Applications.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(a => a.UserId == request.UserId.Value);
        }

        if (request.CompanyId.HasValue)
        {
            query = query.Where(a => a.CompanyId == request.CompanyId.Value);
        }

        if (request.Status.HasValue)
        {
            query = query.Where(a => a.Status == request.Status.Value);
        }

        if (request.IsRemote.HasValue)
        {
            query = query.Where(a => a.IsRemote == request.IsRemote.Value);
        }

        var applications = await query
            .OrderByDescending(a => a.AppliedDate)
            .ToListAsync(cancellationToken);

        return applications.Select(a => a.ToDto());
    }
}
