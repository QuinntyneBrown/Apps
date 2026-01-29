using Microsoft.EntityFrameworkCore;
using FamilyMembers.Core.Models;

namespace FamilyMembers.Core;

public interface IFamilyMembersDbContext
{
    DbSet<FamilyMember> FamilyMembers { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
