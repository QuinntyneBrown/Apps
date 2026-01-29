using Microsoft.EntityFrameworkCore;
using Deductions.Core.Models;

namespace Deductions.Core;

public interface IDeductionsDbContext
{
    DbSet<Deduction> Deductions { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
