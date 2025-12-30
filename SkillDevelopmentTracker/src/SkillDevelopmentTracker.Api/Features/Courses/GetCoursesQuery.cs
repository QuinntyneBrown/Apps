using SkillDevelopmentTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SkillDevelopmentTracker.Api.Features.Courses;

public record GetCoursesQuery : IRequest<IEnumerable<CourseDto>>
{
    public Guid? UserId { get; init; }
    public string? Provider { get; init; }
    public bool? IsCompleted { get; init; }
}

public class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, IEnumerable<CourseDto>>
{
    private readonly ISkillDevelopmentTrackerContext _context;
    private readonly ILogger<GetCoursesQueryHandler> _logger;

    public GetCoursesQueryHandler(
        ISkillDevelopmentTrackerContext context,
        ILogger<GetCoursesQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<CourseDto>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting courses for user {UserId}", request.UserId);

        var query = _context.Courses.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(c => c.UserId == request.UserId.Value);
        }

        if (!string.IsNullOrEmpty(request.Provider))
        {
            query = query.Where(c => c.Provider == request.Provider);
        }

        if (request.IsCompleted.HasValue)
        {
            query = query.Where(c => c.IsCompleted == request.IsCompleted.Value);
        }

        var courses = await query
            .OrderByDescending(c => c.StartDate)
            .ToListAsync(cancellationToken);

        return courses.Select(c => c.ToDto());
    }
}
