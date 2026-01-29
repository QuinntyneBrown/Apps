using Microsoft.EntityFrameworkCore;
using Screenings.Core.Models;

namespace Screenings.Core;

public interface IScreeningsDbContext
{
    DbSet<Screening> Screenings { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
