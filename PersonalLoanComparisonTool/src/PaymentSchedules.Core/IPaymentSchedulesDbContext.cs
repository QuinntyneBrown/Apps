using Microsoft.EntityFrameworkCore;
using PaymentSchedules.Core.Models;

namespace PaymentSchedules.Core;

public interface IPaymentSchedulesDbContext
{
    DbSet<PaymentSchedule> PaymentSchedules { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
