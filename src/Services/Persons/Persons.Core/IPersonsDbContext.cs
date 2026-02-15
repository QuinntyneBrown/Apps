using Microsoft.EntityFrameworkCore;
using Persons.Core.Models;

namespace Persons.Core;

public interface IPersonsDbContext
{
    DbSet<Person> Persons { get; }
    DbSet<Relationship> Relationships { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
