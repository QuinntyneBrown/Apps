using Microsoft.EntityFrameworkCore;
using Contacts.Core;
using Contacts.Core.Models;

namespace Contacts.Infrastructure.Data;

public class ContactsDbContext : DbContext, IContactsDbContext
{
    public ContactsDbContext(DbContextOptions<ContactsDbContext> options) : base(options) { }

    public DbSet<EmergencyContact> EmergencyContacts => Set<EmergencyContact>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmergencyContact>(entity =>
        {
            entity.HasKey(e => e.EmergencyContactId);
            entity.Property(e => e.Name).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Relationship).HasMaxLength(100);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20).IsRequired();
            entity.Property(e => e.AlternatePhone).HasMaxLength(20);
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.ContactType).HasMaxLength(100);
            entity.Property(e => e.ServiceArea).HasMaxLength(200);
            entity.Property(e => e.Notes).HasMaxLength(1000);
            entity.HasIndex(e => e.TenantId);
        });
    }
}
