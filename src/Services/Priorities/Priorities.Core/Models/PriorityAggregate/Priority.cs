namespace Priorities.Core.Models;
public class Priority { public Guid PriorityId { get; set; } public Guid UserId { get; set; } public Guid? ReviewId { get; set; } public string Title { get; set; } = string.Empty; public int Rank { get; set; } public bool IsCompleted { get; set; } public DateTime CreatedAt { get; set; } = DateTime.UtcNow; }
