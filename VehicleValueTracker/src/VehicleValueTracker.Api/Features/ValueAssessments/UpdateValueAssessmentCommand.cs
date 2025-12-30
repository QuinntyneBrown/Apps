using VehicleValueTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace VehicleValueTracker.Api.Features.ValueAssessments;

public record UpdateValueAssessmentCommand : IRequest<ValueAssessmentDto?>
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

public class UpdateValueAssessmentCommandHandler : IRequestHandler<UpdateValueAssessmentCommand, ValueAssessmentDto?>
{
    private readonly IVehicleValueTrackerContext _context;
    private readonly ILogger<UpdateValueAssessmentCommandHandler> _logger;

    public UpdateValueAssessmentCommandHandler(
        IVehicleValueTrackerContext context,
        ILogger<UpdateValueAssessmentCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ValueAssessmentDto?> Handle(UpdateValueAssessmentCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating value assessment {ValueAssessmentId}", request.ValueAssessmentId);

        var assessment = await _context.ValueAssessments
            .FirstOrDefaultAsync(a => a.ValueAssessmentId == request.ValueAssessmentId, cancellationToken);

        if (assessment == null)
        {
            _logger.LogWarning("Value assessment {ValueAssessmentId} not found", request.ValueAssessmentId);
            return null;
        }

        assessment.VehicleId = request.VehicleId;
        assessment.AssessmentDate = request.AssessmentDate;
        assessment.EstimatedValue = request.EstimatedValue;
        assessment.MileageAtAssessment = request.MileageAtAssessment;
        assessment.ConditionGrade = request.ConditionGrade;
        assessment.ValuationSource = request.ValuationSource;
        assessment.ExteriorCondition = request.ExteriorCondition;
        assessment.InteriorCondition = request.InteriorCondition;
        assessment.MechanicalCondition = request.MechanicalCondition;
        assessment.Modifications = request.Modifications;
        assessment.KnownIssues = request.KnownIssues;
        assessment.DepreciationAmount = request.DepreciationAmount;
        assessment.DepreciationPercentage = request.DepreciationPercentage;
        assessment.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated value assessment {ValueAssessmentId}", request.ValueAssessmentId);

        return assessment.ToDto();
    }
}
