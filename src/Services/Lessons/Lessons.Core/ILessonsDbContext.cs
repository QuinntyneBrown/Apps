using Microsoft.EntityFrameworkCore;
using Lessons.Core.Models;

namespace Lessons.Core;

public interface ILessonsDbContext
{
    DbSet<Lesson> Lessons { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
