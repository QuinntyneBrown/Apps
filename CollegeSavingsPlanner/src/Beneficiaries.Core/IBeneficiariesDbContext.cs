using Microsoft.EntityFrameworkCore;
using Beneficiaries.Core.Models;

namespace Beneficiaries.Core;

public interface IBeneficiariesDbContext
{
    DbSet<Beneficiary> Beneficiaries { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
