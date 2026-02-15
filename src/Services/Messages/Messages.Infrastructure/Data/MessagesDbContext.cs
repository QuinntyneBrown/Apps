using Microsoft.EntityFrameworkCore;
using Messages.Core;

namespace Messages.Infrastructure.Data;

public class MessagesDbContext : DbContext, IMessagesDbContext
{
    public MessagesDbContext(DbContextOptions<MessagesDbContext> options) : base(options)
    {
    }

    public DbSet<Message> Messages => Set<Message>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MessagesDbContext).Assembly);
    }
}
