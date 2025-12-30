using VehicleValueTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace VehicleValueTracker.Api.Features.ValueAssessments;

public record GetValueAssessmentsQuery : IRequest<IEnumerable<ValueAssessmentDto>>
{
    public Guid? VehicleId { get; init; }
    public DateTime? FromDate { get; init; }
    public DateTime? ToDate { get; init; }
    public ConditionGrade? ConditionGrade { get; init; }
}

public class GetValueAssessmentsQueryHandler : IRequestHandler<GetValueAssessmentsQuery, IEnumerable<ValueAssessmentDto>>
{
    private readonly IVehicleValueTrackerContext _context;
    private readonly ILogger<GetValueAssessmentsQueryHandler> _logger;

    public GetValueAssessmentsQueryHandler(
        IVehicleValueTrackerContext context,
        ILogger<GetValueAssessmentsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<ValueAssessmentDto>> Handle(GetValueAssessmentsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting value assessments");

        var query = _context.ValueAssessments.AsNoTracking();

        if (request.VehicleId.HasValue)
        {
            query = query.Where(a => a.VehicleId == request.VehicleId.Value);
        }

        if (request.FromDate.HasValue)
        {
            query = query.Where(a => a.AssessmentDate >= request.FromDate.Value);
        }

        if (request.ToDate.HasValue)
        {
            query = query.Where(a => a.AssessmentDate <= request.ToDate.Value);
        }

        if (request.ConditionGrade.HasValue)
        {
            query = query.Where(a => a.ConditionGrade == request.ConditionGrade.Value);
        }

        var assessments = await query
            .OrderByDescending(a => a.AssessmentDate)
            .ToListAsync(cancellationToken);

        return assessments.Select(a => a.ToDto());
    }
}
