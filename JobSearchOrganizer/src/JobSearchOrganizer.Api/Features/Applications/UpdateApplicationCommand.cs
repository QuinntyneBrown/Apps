using JobSearchOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JobSearchOrganizer.Api.Features.Applications;

public record UpdateApplicationCommand : IRequest<ApplicationDto?>
{
    public Guid ApplicationId { get; init; }
    public string JobTitle { get; init; } = string.Empty;
    public string? JobUrl { get; init; }
    public ApplicationStatus Status { get; init; }
    public DateTime AppliedDate { get; init; }
    public string? SalaryRange { get; init; }
    public string? Location { get; init; }
    public string? JobType { get; init; }
    public bool IsRemote { get; init; }
    public string? ContactPerson { get; init; }
    public string? Notes { get; init; }
}

public class UpdateApplicationCommandHandler : IRequestHandler<UpdateApplicationCommand, ApplicationDto?>
{
    private readonly IJobSearchOrganizerContext _context;
    private readonly ILogger<UpdateApplicationCommandHandler> _logger;

    public UpdateApplicationCommandHandler(
        IJobSearchOrganizerContext context,
        ILogger<UpdateApplicationCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ApplicationDto?> Handle(UpdateApplicationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating application {ApplicationId}", request.ApplicationId);

        var application = await _context.Applications
            .FirstOrDefaultAsync(a => a.ApplicationId == request.ApplicationId, cancellationToken);

        if (application == null)
        {
            _logger.LogWarning("Application {ApplicationId} not found", request.ApplicationId);
            return null;
        }

        application.JobTitle = request.JobTitle;
        application.JobUrl = request.JobUrl;
        application.Status = request.Status;
        application.AppliedDate = request.AppliedDate;
        application.SalaryRange = request.SalaryRange;
        application.Location = request.Location;
        application.JobType = request.JobType;
        application.IsRemote = request.IsRemote;
        application.ContactPerson = request.ContactPerson;
        application.Notes = request.Notes;
        application.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated application {ApplicationId}", request.ApplicationId);

        return application.ToDto();
    }
}
