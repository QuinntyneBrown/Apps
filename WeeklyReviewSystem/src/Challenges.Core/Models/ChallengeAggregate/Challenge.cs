namespace Challenges.Core.Models;
public class Challenge { public Guid ChallengeId { get; set; } public Guid UserId { get; set; } public Guid? ReviewId { get; set; } public string Description { get; set; } = string.Empty; public string? Resolution { get; set; } public bool IsResolved { get; set; } public DateTime CreatedAt { get; set; } = DateTime.UtcNow; }
