using MediatR;
using Microsoft.EntityFrameworkCore;
using TrainingPlans.Core;

namespace TrainingPlans.Api.Features;

public record GetTrainingPlanByIdQuery(Guid TrainingPlanId) : IRequest<TrainingPlanDto?>;

public class GetTrainingPlanByIdQueryHandler : IRequestHandler<GetTrainingPlanByIdQuery, TrainingPlanDto?>
{
    private readonly ITrainingPlansDbContext _context;
    public GetTrainingPlanByIdQueryHandler(ITrainingPlansDbContext context) => _context = context;

    public async Task<TrainingPlanDto?> Handle(GetTrainingPlanByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.TrainingPlans.Where(t => t.TrainingPlanId == request.TrainingPlanId).Select(t => new TrainingPlanDto
        {
            TrainingPlanId = t.TrainingPlanId, UserId = t.UserId, Name = t.Name, RaceId = t.RaceId,
            StartDate = t.StartDate, EndDate = t.EndDate, WeeklyMileageGoal = t.WeeklyMileageGoal,
            PlanDetails = t.PlanDetails, IsActive = t.IsActive, Notes = t.Notes
        }).FirstOrDefaultAsync(cancellationToken);
    }
}
