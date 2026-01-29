using MediatR;
using Microsoft.EntityFrameworkCore;
using TrainingPlans.Core;

namespace TrainingPlans.Api.Features;

public record GetTrainingPlansQuery : IRequest<IEnumerable<TrainingPlanDto>>;

public class GetTrainingPlansQueryHandler : IRequestHandler<GetTrainingPlansQuery, IEnumerable<TrainingPlanDto>>
{
    private readonly ITrainingPlansDbContext _context;
    public GetTrainingPlansQueryHandler(ITrainingPlansDbContext context) => _context = context;

    public async Task<IEnumerable<TrainingPlanDto>> Handle(GetTrainingPlansQuery request, CancellationToken cancellationToken)
    {
        return await _context.TrainingPlans.Select(t => new TrainingPlanDto
        {
            TrainingPlanId = t.TrainingPlanId, UserId = t.UserId, Name = t.Name, RaceId = t.RaceId,
            StartDate = t.StartDate, EndDate = t.EndDate, WeeklyMileageGoal = t.WeeklyMileageGoal,
            PlanDetails = t.PlanDetails, IsActive = t.IsActive, Notes = t.Notes
        }).ToListAsync(cancellationToken);
    }
}
