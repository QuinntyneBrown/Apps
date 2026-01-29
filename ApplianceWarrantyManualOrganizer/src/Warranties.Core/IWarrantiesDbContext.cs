using Microsoft.EntityFrameworkCore;
using Warranties.Core.Models;

namespace Warranties.Core;

public interface IWarrantiesDbContext
{
    DbSet<Warranty> Warranties { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
