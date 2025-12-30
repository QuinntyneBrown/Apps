using VehicleValueTracker.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace VehicleValueTracker.Api.Features.ValueAssessments;

public record CreateValueAssessmentCommand : IRequest<ValueAssessmentDto>
{
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

public class CreateValueAssessmentCommandHandler : IRequestHandler<CreateValueAssessmentCommand, ValueAssessmentDto>
{
    private readonly IVehicleValueTrackerContext _context;
    private readonly ILogger<CreateValueAssessmentCommandHandler> _logger;

    public CreateValueAssessmentCommandHandler(
        IVehicleValueTrackerContext context,
        ILogger<CreateValueAssessmentCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ValueAssessmentDto> Handle(CreateValueAssessmentCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating value assessment for vehicle {VehicleId}",
            request.VehicleId);

        var assessment = new ValueAssessment
        {
            ValueAssessmentId = Guid.NewGuid(),
            VehicleId = request.VehicleId,
            AssessmentDate = request.AssessmentDate,
            EstimatedValue = request.EstimatedValue,
            MileageAtAssessment = request.MileageAtAssessment,
            ConditionGrade = request.ConditionGrade,
            ValuationSource = request.ValuationSource,
            ExteriorCondition = request.ExteriorCondition,
            InteriorCondition = request.InteriorCondition,
            MechanicalCondition = request.MechanicalCondition,
            Modifications = request.Modifications,
            KnownIssues = request.KnownIssues,
            DepreciationAmount = request.DepreciationAmount,
            DepreciationPercentage = request.DepreciationPercentage,
            Notes = request.Notes,
        };

        _context.ValueAssessments.Add(assessment);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created value assessment {ValueAssessmentId} for vehicle {VehicleId}",
            assessment.ValueAssessmentId,
            request.VehicleId);

        return assessment.ToDto();
    }
}
