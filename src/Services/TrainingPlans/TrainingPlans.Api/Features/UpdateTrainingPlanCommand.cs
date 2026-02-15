using MediatR;
using Microsoft.EntityFrameworkCore;
using TrainingPlans.Core;

namespace TrainingPlans.Api.Features;

public record UpdateTrainingPlanCommand : IRequest<TrainingPlanDto?>
{
    public Guid TrainingPlanId { get; init; }
    public string Name { get; init; } = string.Empty;
    public Guid? RaceId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public decimal? WeeklyMileageGoal { get; init; }
    public string? PlanDetails { get; init; }
    public bool IsActive { get; init; }
    public string? Notes { get; init; }
}

public class UpdateTrainingPlanCommandHandler : IRequestHandler<UpdateTrainingPlanCommand, TrainingPlanDto?>
{
    private readonly ITrainingPlansDbContext _context;
    public UpdateTrainingPlanCommandHandler(ITrainingPlansDbContext context) => _context = context;

    public async Task<TrainingPlanDto?> Handle(UpdateTrainingPlanCommand request, CancellationToken cancellationToken)
    {
        var plan = await _context.TrainingPlans.FirstOrDefaultAsync(t => t.TrainingPlanId == request.TrainingPlanId, cancellationToken);
        if (plan == null) return null;

        plan.Name = request.Name; plan.RaceId = request.RaceId; plan.StartDate = request.StartDate;
        plan.EndDate = request.EndDate; plan.WeeklyMileageGoal = request.WeeklyMileageGoal;
        plan.PlanDetails = request.PlanDetails; plan.IsActive = request.IsActive; plan.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        return new TrainingPlanDto
        {
            TrainingPlanId = plan.TrainingPlanId, UserId = plan.UserId, Name = plan.Name, RaceId = plan.RaceId,
            StartDate = plan.StartDate, EndDate = plan.EndDate, WeeklyMileageGoal = plan.WeeklyMileageGoal,
            PlanDetails = plan.PlanDetails, IsActive = plan.IsActive, Notes = plan.Notes
        };
    }
}
