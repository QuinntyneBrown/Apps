using Microsoft.EntityFrameworkCore;
using Courses.Core.Models;

namespace Courses.Core;

public interface ICoursesDbContext
{
    DbSet<Course> Courses { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
