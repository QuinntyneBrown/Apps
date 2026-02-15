using Microsoft.EntityFrameworkCore; using Priorities.Core.Models;
namespace Priorities.Core; public interface IPrioritiesDbContext { DbSet<Priority> Priorities { get; } Task<int> SaveChangesAsync(CancellationToken cancellationToken = default); }
