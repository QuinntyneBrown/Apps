using Microsoft.EntityFrameworkCore;
using Recipients.Core.Models;

namespace Recipients.Core;

public interface IRecipientsDbContext
{
    DbSet<Recipient> Recipients { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
