using Experiences.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Experiences.Core;

public interface IExperiencesDbContext
{
    DbSet<Experience> Experiences { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
