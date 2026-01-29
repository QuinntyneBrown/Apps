using Applications.Core;
using Applications.Core.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Applications.Api.Features;

public record UpdateApplicationCommand(
    Guid ApplicationId,
    string JobTitle,
    string? JobDescription,
    string? JobUrl,
    ApplicationStatus Status,
    decimal? SalaryMin,
    decimal? SalaryMax,
    string? Notes) : IRequest<ApplicationDto?>;

public class UpdateApplicationCommandHandler : IRequestHandler<UpdateApplicationCommand, ApplicationDto?>
{
    private readonly IApplicationsDbContext _context;
    private readonly ILogger<UpdateApplicationCommandHandler> _logger;

    public UpdateApplicationCommandHandler(IApplicationsDbContext context, ILogger<UpdateApplicationCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ApplicationDto?> Handle(UpdateApplicationCommand request, CancellationToken cancellationToken)
    {
        var application = await _context.Applications
            .FirstOrDefaultAsync(a => a.ApplicationId == request.ApplicationId, cancellationToken);

        if (application == null) return null;

        application.JobTitle = request.JobTitle;
        application.JobDescription = request.JobDescription;
        application.JobUrl = request.JobUrl;
        application.Status = request.Status;
        application.SalaryMin = request.SalaryMin;
        application.SalaryMax = request.SalaryMax;
        application.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Application updated: {ApplicationId}", application.ApplicationId);

        return application.ToDto();
    }
}
