using Microsoft.EntityFrameworkCore;
using TrainingPlans.Core.Models;

namespace TrainingPlans.Core;

public interface ITrainingPlansDbContext
{
    DbSet<TrainingPlan> TrainingPlans { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
