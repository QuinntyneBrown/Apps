using Microsoft.EntityFrameworkCore;

namespace NutritionInfo.Core;

public interface INutritionInfoDbContext
{
    DbSet<NutritionData> NutritionData { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
