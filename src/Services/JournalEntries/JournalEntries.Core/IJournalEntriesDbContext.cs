using Microsoft.EntityFrameworkCore;
using JournalEntries.Core.Models;

namespace JournalEntries.Core;

public interface IJournalEntriesDbContext
{
    DbSet<JournalEntry> JournalEntries { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
