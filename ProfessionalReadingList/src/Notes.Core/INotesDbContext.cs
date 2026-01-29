using Microsoft.EntityFrameworkCore;
using Notes.Core.Models;

namespace Notes.Core;

public interface INotesDbContext
{
    DbSet<Note> Notes { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
