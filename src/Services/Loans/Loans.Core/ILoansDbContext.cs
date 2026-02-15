using Microsoft.EntityFrameworkCore;
using Loans.Core.Models;

namespace Loans.Core;

public interface ILoansDbContext
{
    DbSet<Loan> Loans { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
