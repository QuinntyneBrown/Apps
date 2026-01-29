using Microsoft.EntityFrameworkCore;
using Skills.Core.Models;

namespace Skills.Core;

public interface ISkillsDbContext
{
    DbSet<Skill> Skills { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
