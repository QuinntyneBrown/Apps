namespace Prompts.Core.Models;

public class Prompt
{
    public Guid PromptId { get; set; }
    public Guid TenantId { get; set; }
    public string Text { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string? Difficulty { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
