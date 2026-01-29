using Microsoft.EntityFrameworkCore;
using Prompts.Core.Models;

namespace Prompts.Core;

public interface IPromptsDbContext
{
    DbSet<Prompt> Prompts { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
