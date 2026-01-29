using Microsoft.EntityFrameworkCore;
using Nutrition.Core.Models;

namespace Nutrition.Core;

public interface INutritionDbContext
{
    DbSet<NutritionEntry> NutritionEntries { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
