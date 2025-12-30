using VehicleValueTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace VehicleValueTracker.Api.Features.ValueAssessments;

public record GetValueAssessmentByIdQuery : IRequest<ValueAssessmentDto?>
{
    public Guid ValueAssessmentId { get; init; }
}

public class GetValueAssessmentByIdQueryHandler : IRequestHandler<GetValueAssessmentByIdQuery, ValueAssessmentDto?>
{
    private readonly IVehicleValueTrackerContext _context;
    private readonly ILogger<GetValueAssessmentByIdQueryHandler> _logger;

    public GetValueAssessmentByIdQueryHandler(
        IVehicleValueTrackerContext context,
        ILogger<GetValueAssessmentByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ValueAssessmentDto?> Handle(GetValueAssessmentByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting value assessment {ValueAssessmentId}", request.ValueAssessmentId);

        var assessment = await _context.ValueAssessments
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.ValueAssessmentId == request.ValueAssessmentId, cancellationToken);

        if (assessment == null)
        {
            _logger.LogWarning("Value assessment {ValueAssessmentId} not found", request.ValueAssessmentId);
            return null;
        }

        return assessment.ToDto();
    }
}
