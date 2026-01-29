namespace Accomplishments.Core.Models;
public class Accomplishment { public Guid AccomplishmentId { get; set; } public Guid UserId { get; set; } public Guid? ReviewId { get; set; } public string Description { get; set; } = string.Empty; public string? Category { get; set; } public DateTime CreatedAt { get; set; } = DateTime.UtcNow; }
