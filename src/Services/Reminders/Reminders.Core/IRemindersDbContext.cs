using Microsoft.EntityFrameworkCore;
using Reminders.Core.Models;

namespace Reminders.Core;

public interface IRemindersDbContext
{
    DbSet<Reminder> Reminders { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
