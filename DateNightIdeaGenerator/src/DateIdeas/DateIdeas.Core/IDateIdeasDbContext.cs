using Microsoft.EntityFrameworkCore;
using DateIdeas.Core.Models;

namespace DateIdeas.Core;

public interface IDateIdeasDbContext
{
    DbSet<DateIdea> DateIdeas { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
