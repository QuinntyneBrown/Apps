using Applications.Core.Models;

namespace Applications.Api.Features;

public record ApplicationDto(
    Guid ApplicationId,
    Guid UserId,
    Guid CompanyId,
    string JobTitle,
    string? JobDescription,
    string? JobUrl,
    ApplicationStatus Status,
    DateTime AppliedDate,
    decimal? SalaryMin,
    decimal? SalaryMax,
    string? Notes,
    DateTime CreatedAt);

public static class ApplicationExtensions
{
    public static ApplicationDto ToDto(this Application application)
    {
        return new ApplicationDto(
            application.ApplicationId,
            application.UserId,
            application.CompanyId,
            application.JobTitle,
            application.JobDescription,
            application.JobUrl,
            application.Status,
            application.AppliedDate,
            application.SalaryMin,
            application.SalaryMax,
            application.Notes,
            application.CreatedAt);
    }
}
