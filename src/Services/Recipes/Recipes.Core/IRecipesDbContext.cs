using Microsoft.EntityFrameworkCore;
using Recipes.Core.Models;

namespace Recipes.Core;

public interface IRecipesDbContext
{
    DbSet<Recipe> Recipes { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
