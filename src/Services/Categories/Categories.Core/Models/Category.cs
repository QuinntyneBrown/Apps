namespace Categories.Core.Models;

public class Category
{
    public Guid CategoryId { get; set; }
    public Guid TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = "#000000";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
