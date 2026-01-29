namespace ValueAssessments.Core.Models;

public class ValueAssessment
{
    public Guid AssessmentId { get; set; }
    public Guid VehicleId { get; set; }
    public Guid UserId { get; set; }
    public decimal EstimatedValue { get; set; }
    public string? AssessmentMethod { get; set; }
    public string? Notes { get; set; }
    public DateTime AssessmentDate { get; set; } = DateTime.UtcNow;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
