namespace Neighbors.Core;

public class Neighbor
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ConnectedUserId { get; set; }
    public string Status { get; set; } = "Pending";
    public DateTime ConnectedAt { get; set; } = DateTime.UtcNow;
}
