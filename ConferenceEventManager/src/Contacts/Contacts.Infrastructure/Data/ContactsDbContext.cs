using Contacts.Core;
using Contacts.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Infrastructure.Data;

public class ContactsDbContext : DbContext, IContactsDbContext
{
    public ContactsDbContext(DbContextOptions<ContactsDbContext> options) : base(options) { }

    public DbSet<Contact> Contacts => Set<Contact>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.ToTable("Contacts", "contacts");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Company).HasMaxLength(200);
            entity.Property(e => e.JobTitle).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.LinkedInUrl).HasMaxLength(500);
            entity.HasIndex(e => e.EventId);
            entity.HasIndex(e => e.TenantId);
        });
    }
}
