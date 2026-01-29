using Microsoft.EntityFrameworkCore;
using Insurance.Core.Models;

namespace Insurance.Core;

public interface IInsuranceDbContext
{
    DbSet<InsuranceInfo> InsuranceInfos { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
