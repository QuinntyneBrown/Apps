using Microsoft.EntityFrameworkCore;
using Families.Core.Models;

namespace Families.Core;

public interface IFamiliesDbContext
{
    DbSet<FamilyMember> FamilyMembers { get; }
    DbSet<Household> Households { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
