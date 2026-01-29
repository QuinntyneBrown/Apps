using Microsoft.EntityFrameworkCore;
using ValueAssessments.Core.Models;

namespace ValueAssessments.Core;

public interface IValueAssessmentsDbContext
{
    DbSet<ValueAssessment> ValueAssessments { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
