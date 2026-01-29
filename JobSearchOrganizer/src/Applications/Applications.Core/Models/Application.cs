namespace Applications.Core.Models;

public class Application
{
    public Guid ApplicationId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public Guid CompanyId { get; set; }
    public string JobTitle { get; set; } = string.Empty;
    public string? JobDescription { get; set; }
    public string? JobUrl { get; set; }
    public ApplicationStatus Status { get; set; } = ApplicationStatus.Applied;
    public DateTime AppliedDate { get; set; } = DateTime.UtcNow;
    public decimal? SalaryMin { get; set; }
    public decimal? SalaryMax { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public enum ApplicationStatus
{
    Applied,
    Screening,
    Interview,
    Offer,
    Accepted,
    Rejected,
    Withdrawn
}
