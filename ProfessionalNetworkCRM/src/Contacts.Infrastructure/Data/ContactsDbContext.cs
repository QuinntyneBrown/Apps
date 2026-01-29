using Contacts.Core;
using Contacts.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Infrastructure.Data;

public class ContactsDbContext : DbContext, IContactsDbContext
{
    public ContactsDbContext(DbContextOptions<ContactsDbContext> options) : base(options) { }

    public DbSet<Contact> Contacts { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Contact>(entity =>
        {
            entity.ToTable("Contacts");
            entity.HasKey(e => e.ContactId);
            entity.Property(e => e.Name).HasMaxLength(500).IsRequired();
        });
    }
}
