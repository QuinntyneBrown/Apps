namespace Projects.Core.Models;

public class Project
{
    public Guid ProjectId { get; private set; }
    public Guid TenantId { get; private set; }
    public Guid UserId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public DateTime? StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public bool IsCompleted { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private Project() { }

    public Project(Guid tenantId, Guid userId, string name, string? description = null, DateTime? startDate = null)
    {
        ProjectId = Guid.NewGuid();
        TenantId = tenantId;
        UserId = userId;
        Name = name;
        Description = description;
        StartDate = startDate;
        IsCompleted = false;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string name, string? description = null)
    {
        Name = name;
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Complete()
    {
        IsCompleted = true;
        EndDate = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
