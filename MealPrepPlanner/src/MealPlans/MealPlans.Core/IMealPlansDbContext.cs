using Microsoft.EntityFrameworkCore;
using MealPlans.Core.Models;

namespace MealPlans.Core;

public interface IMealPlansDbContext
{
    DbSet<MealPlan> MealPlans { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
