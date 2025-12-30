using VehicleValueTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace VehicleValueTracker.Api.Features.ValueAssessments;

public record DeleteValueAssessmentCommand : IRequest<bool>
{
    public Guid ValueAssessmentId { get; init; }
}

public class DeleteValueAssessmentCommandHandler : IRequestHandler<DeleteValueAssessmentCommand, bool>
{
    private readonly IVehicleValueTrackerContext _context;
    private readonly ILogger<DeleteValueAssessmentCommandHandler> _logger;

    public DeleteValueAssessmentCommandHandler(
        IVehicleValueTrackerContext context,
        ILogger<DeleteValueAssessmentCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteValueAssessmentCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting value assessment {ValueAssessmentId}", request.ValueAssessmentId);

        var assessment = await _context.ValueAssessments
            .FirstOrDefaultAsync(a => a.ValueAssessmentId == request.ValueAssessmentId, cancellationToken);

        if (assessment == null)
        {
            _logger.LogWarning("Value assessment {ValueAssessmentId} not found", request.ValueAssessmentId);
            return false;
        }

        _context.ValueAssessments.Remove(assessment);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted value assessment {ValueAssessmentId}", request.ValueAssessmentId);

        return true;
    }
}
