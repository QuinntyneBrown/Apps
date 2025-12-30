using JobSearchOrganizer.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JobSearchOrganizer.Api.Features.Applications;

public record CreateApplicationCommand : IRequest<ApplicationDto>
{
    public Guid UserId { get; init; }
    public Guid CompanyId { get; init; }
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

public class CreateApplicationCommandHandler : IRequestHandler<CreateApplicationCommand, ApplicationDto>
{
    private readonly IJobSearchOrganizerContext _context;
    private readonly ILogger<CreateApplicationCommandHandler> _logger;

    public CreateApplicationCommandHandler(
        IJobSearchOrganizerContext context,
        ILogger<CreateApplicationCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ApplicationDto> Handle(CreateApplicationCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating application for user {UserId}, job title: {JobTitle}",
            request.UserId,
            request.JobTitle);

        var application = new Application
        {
            ApplicationId = Guid.NewGuid(),
            UserId = request.UserId,
            CompanyId = request.CompanyId,
            JobTitle = request.JobTitle,
            JobUrl = request.JobUrl,
            Status = request.Status,
            AppliedDate = request.AppliedDate,
            SalaryRange = request.SalaryRange,
            Location = request.Location,
            JobType = request.JobType,
            IsRemote = request.IsRemote,
            ContactPerson = request.ContactPerson,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Applications.Add(application);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created application {ApplicationId} for user {UserId}",
            application.ApplicationId,
            request.UserId);

        return application.ToDto();
    }
}
