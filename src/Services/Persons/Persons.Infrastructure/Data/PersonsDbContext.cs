using Persons.Core;
using Persons.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Persons.Infrastructure.Data;

public class PersonsDbContext : DbContext, IPersonsDbContext
{
    public PersonsDbContext(DbContextOptions<PersonsDbContext> options) : base(options) { }

    public DbSet<Person> Persons { get; set; } = null!;
    public DbSet<Relationship> Relationships { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(e => { e.ToTable("Persons"); e.HasKey(x => x.PersonId); e.Property(x => x.FirstName).HasMaxLength(100); e.Property(x => x.LastName).HasMaxLength(100); });
        modelBuilder.Entity<Relationship>(e => { e.ToTable("Relationships"); e.HasKey(x => x.RelationshipId); e.Property(x => x.RelationshipType).HasMaxLength(50); });
    }
}
