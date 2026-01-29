using Microsoft.EntityFrameworkCore;

namespace Messages.Core;

public interface IMessagesDbContext
{
    DbSet<Message> Messages { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
