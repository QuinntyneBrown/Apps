using Microsoft.EntityFrameworkCore;
using Contacts.Core.Models;

namespace Contacts.Core;

public interface IContactsDbContext
{
    DbSet<Contact> Contacts { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
