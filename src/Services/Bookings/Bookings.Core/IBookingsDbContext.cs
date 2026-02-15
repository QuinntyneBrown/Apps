using Microsoft.EntityFrameworkCore;
using Bookings.Core.Models;

namespace Bookings.Core;

public interface IBookingsDbContext
{
    DbSet<Booking> Bookings { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
