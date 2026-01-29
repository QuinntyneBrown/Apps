namespace Itineraries.Core.Models;

public class Itinerary
{
    public Guid ItineraryId { get; set; }
    public Guid TenantId { get; set; }
    public Guid TripId { get; set; }
    public DateTime Date { get; set; }
    public string? Activity { get; set; }
    public string? Location { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
