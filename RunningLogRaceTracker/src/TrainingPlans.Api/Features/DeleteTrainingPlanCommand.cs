using MediatR;
using Microsoft.EntityFrameworkCore;
using TrainingPlans.Core;

namespace TrainingPlans.Api.Features;

public record DeleteTrainingPlanCommand(Guid TrainingPlanId) : IRequest<bool>;

public class DeleteTrainingPlanCommandHandler : IRequestHandler<DeleteTrainingPlanCommand, bool>
{
    private readonly ITrainingPlansDbContext _context;
    public DeleteTrainingPlanCommandHandler(ITrainingPlansDbContext context) => _context = context;

    public async Task<bool> Handle(DeleteTrainingPlanCommand request, CancellationToken cancellationToken)
    {
        var plan = await _context.TrainingPlans.FirstOrDefaultAsync(t => t.TrainingPlanId == request.TrainingPlanId, cancellationToken);
        if (plan == null) return false;
        _context.TrainingPlans.Remove(plan);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
