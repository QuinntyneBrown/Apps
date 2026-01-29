namespace Projects.Core.Models;

public class Project
{
    public Guid ProjectId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Organization { get; set; }
    public string? Role { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<string> Technologies { get; set; } = new();
    public List<string> Outcomes { get; set; } = new();
    public string? ProjectUrl { get; set; }
    public bool IsFeatured { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public void ToggleFeatured()
    {
        IsFeatured = !IsFeatured;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddTechnology(string technology)
    {
        if (!Technologies.Contains(technology, StringComparer.OrdinalIgnoreCase))
        {
            Technologies.Add(technology);
            UpdatedAt = DateTime.UtcNow;
        }
    }

    public void Complete(DateTime endDate)
    {
        EndDate = endDate;
        UpdatedAt = DateTime.UtcNow;
    }
}
