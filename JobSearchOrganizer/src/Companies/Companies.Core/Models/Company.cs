namespace Companies.Core.Models;

public class Company
{
    public Guid CompanyId { get; set; }
    public Guid UserId { get; set; }
    public Guid TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Website { get; set; }
    public string? Industry { get; set; }
    public string? Location { get; set; }
    public string? Size { get; set; }
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public int? Rating { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
