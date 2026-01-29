using Microsoft.EntityFrameworkCore;
using Introductions.Core.Models;

namespace Introductions.Core;

public interface IIntroductionsDbContext
{
    DbSet<Introduction> Introductions { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
