using Microsoft.EntityFrameworkCore; using Accomplishments.Core.Models;
namespace Accomplishments.Core; public interface IAccomplishmentsDbContext { DbSet<Accomplishment> Accomplishments { get; } Task<int> SaveChangesAsync(CancellationToken cancellationToken = default); }
