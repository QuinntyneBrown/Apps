using Notes.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Notes.Core;

public interface INotesDbContext
{
    DbSet<Note> Notes { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
