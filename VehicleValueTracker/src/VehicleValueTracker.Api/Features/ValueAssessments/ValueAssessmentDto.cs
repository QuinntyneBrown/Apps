using VehicleValueTracker.Core;

namespace VehicleValueTracker.Api.Features.ValueAssessments;

public record ValueAssessmentDto
{
    public Guid ValueAssessmentId { get; init; }
    public Guid VehicleId { get; init; }
    public DateTime AssessmentDate { get; init; }
    public decimal EstimatedValue { get; init; }
    public decimal MileageAtAssessment { get; init; }
    public ConditionGrade ConditionGrade { get; init; }
    public string? ValuationSource { get; init; }
    public string? ExteriorCondition { get; init; }
    public string? InteriorCondition { get; init; }
    public string? MechanicalCondition { get; init; }
    public List<string> Modifications { get; init; } = new List<string>();
    public List<string> KnownIssues { get; init; } = new List<string>();
    public decimal? DepreciationAmount { get; init; }
    public decimal? DepreciationPercentage { get; init; }
    public string? Notes { get; init; }
}

public static class ValueAssessmentExtensions
{
    public static ValueAssessmentDto ToDto(this ValueAssessment assessment)
    {
        return new ValueAssessmentDto
        {
            ValueAssessmentId = assessment.ValueAssessmentId,
            VehicleId = assessment.VehicleId,
            AssessmentDate = assessment.AssessmentDate,
            EstimatedValue = assessment.EstimatedValue,
            MileageAtAssessment = assessment.MileageAtAssessment,
            ConditionGrade = assessment.ConditionGrade,
            ValuationSource = assessment.ValuationSource,
            ExteriorCondition = assessment.ExteriorCondition,
            InteriorCondition = assessment.InteriorCondition,
            MechanicalCondition = assessment.MechanicalCondition,
            Modifications = assessment.Modifications,
            KnownIssues = assessment.KnownIssues,
            DepreciationAmount = assessment.DepreciationAmount,
            DepreciationPercentage = assessment.DepreciationPercentage,
            Notes = assessment.Notes,
        };
    }
}
