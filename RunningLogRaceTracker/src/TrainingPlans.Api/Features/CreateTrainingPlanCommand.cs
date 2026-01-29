using MediatR;
using TrainingPlans.Core;
using TrainingPlans.Core.Models;

namespace TrainingPlans.Api.Features;

public record CreateTrainingPlanCommand : IRequest<TrainingPlanDto>
{
    public Guid UserId { get; init; }
    public Guid TenantId { get; init; }
    public string Name { get; init; } = string.Empty;
    public Guid? RaceId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    public decimal? WeeklyMileageGoal { get; init; }
    public string? PlanDetails { get; init; }
    public string? Notes { get; init; }
}

public class CreateTrainingPlanCommandHandler : IRequestHandler<CreateTrainingPlanCommand, TrainingPlanDto>
{
    private readonly ITrainingPlansDbContext _context;
    public CreateTrainingPlanCommandHandler(ITrainingPlansDbContext context) => _context = context;

    public async Task<TrainingPlanDto> Handle(CreateTrainingPlanCommand request, CancellationToken cancellationToken)
    {
        var plan = new TrainingPlan
        {
            TrainingPlanId = Guid.NewGuid(), UserId = request.UserId, TenantId = request.TenantId,
            Name = request.Name, RaceId = request.RaceId, StartDate = request.StartDate, EndDate = request.EndDate,
            WeeklyMileageGoal = request.WeeklyMileageGoal, PlanDetails = request.PlanDetails,
            IsActive = true, Notes = request.Notes, CreatedAt = DateTime.UtcNow
        };

        _context.TrainingPlans.Add(plan);
        await _context.SaveChangesAsync(cancellationToken);

        return new TrainingPlanDto
        {
            TrainingPlanId = plan.TrainingPlanId, UserId = plan.UserId, Name = plan.Name, RaceId = plan.RaceId,
            StartDate = plan.StartDate, EndDate = plan.EndDate, WeeklyMileageGoal = plan.WeeklyMileageGoal,
            PlanDetails = plan.PlanDetails, IsActive = plan.IsActive, Notes = plan.Notes
        };
    }
}
