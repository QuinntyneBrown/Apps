using Microsoft.EntityFrameworkCore;
using DeliverySchedules.Core.Models;

namespace DeliverySchedules.Core;

public interface IDeliverySchedulesDbContext
{
    DbSet<DeliverySchedule> DeliverySchedules { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
