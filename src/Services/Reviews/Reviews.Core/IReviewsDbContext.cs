using Microsoft.EntityFrameworkCore;
using Reviews.Core.Models;

namespace Reviews.Core;

public interface IReviewsDbContext
{
    DbSet<Review> Reviews { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
