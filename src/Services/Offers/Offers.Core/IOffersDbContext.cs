using Microsoft.EntityFrameworkCore;
using Offers.Core.Models;

namespace Offers.Core;

public interface IOffersDbContext
{
    DbSet<Offer> Offers { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
