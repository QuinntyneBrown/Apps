namespace Assignments.Core.Models;

public class Assignment
{
    public Guid Id { get; set; }
    public Guid ChoreId { get; set; }
    public Guid FamilyMemberId { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? CompletedAt { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public Guid? TenantId { get; set; }
}
