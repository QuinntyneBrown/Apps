using Contacts.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Core;

public interface IContactsDbContext
{
    DbSet<Contact> Contacts { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
