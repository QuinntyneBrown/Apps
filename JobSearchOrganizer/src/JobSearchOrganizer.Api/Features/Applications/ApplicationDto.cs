using JobSearchOrganizer.Core;

namespace JobSearchOrganizer.Api.Features.Applications;

public record ApplicationDto
{
    public Guid ApplicationId { get; init; }
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
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

public static class ApplicationExtensions
{
    public static ApplicationDto ToDto(this Application application)
    {
        return new ApplicationDto
        {
            ApplicationId = application.ApplicationId,
            UserId = application.UserId,
            CompanyId = application.CompanyId,
            JobTitle = application.JobTitle,
            JobUrl = application.JobUrl,
            Status = application.Status,
            AppliedDate = application.AppliedDate,
            SalaryRange = application.SalaryRange,
            Location = application.Location,
            JobType = application.JobType,
            IsRemote = application.IsRemote,
            ContactPerson = application.ContactPerson,
            Notes = application.Notes,
            CreatedAt = application.CreatedAt,
            UpdatedAt = application.UpdatedAt,
        };
    }
}
