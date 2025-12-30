using JobSearchOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JobSearchOrganizer.Api.Features.Applications;

public record GetApplicationByIdQuery : IRequest<ApplicationDto?>
{
    public Guid ApplicationId { get; init; }
}

public class GetApplicationByIdQueryHandler : IRequestHandler<GetApplicationByIdQuery, ApplicationDto?>
{
    private readonly IJobSearchOrganizerContext _context;
    private readonly ILogger<GetApplicationByIdQueryHandler> _logger;

    public GetApplicationByIdQueryHandler(
        IJobSearchOrganizerContext context,
        ILogger<GetApplicationByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ApplicationDto?> Handle(GetApplicationByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting application {ApplicationId}", request.ApplicationId);

        var application = await _context.Applications
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.ApplicationId == request.ApplicationId, cancellationToken);

        return application?.ToDto();
    }
}
