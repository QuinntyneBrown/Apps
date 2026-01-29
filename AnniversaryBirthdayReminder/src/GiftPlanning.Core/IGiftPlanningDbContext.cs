using Microsoft.EntityFrameworkCore;
using GiftPlanning.Core.Models;

namespace GiftPlanning.Core;

public interface IGiftPlanningDbContext
{
    DbSet<GiftPlan> GiftPlans { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
