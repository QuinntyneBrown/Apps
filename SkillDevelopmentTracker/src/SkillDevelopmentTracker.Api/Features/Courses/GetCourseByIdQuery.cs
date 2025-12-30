using SkillDevelopmentTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SkillDevelopmentTracker.Api.Features.Courses;

public record GetCourseByIdQuery : IRequest<CourseDto?>
{
    public Guid CourseId { get; init; }
}

public class GetCourseByIdQueryHandler : IRequestHandler<GetCourseByIdQuery, CourseDto?>
{
    private readonly ISkillDevelopmentTrackerContext _context;
    private readonly ILogger<GetCourseByIdQueryHandler> _logger;

    public GetCourseByIdQueryHandler(
        ISkillDevelopmentTrackerContext context,
        ILogger<GetCourseByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CourseDto?> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting course {CourseId}", request.CourseId);

        var course = await _context.Courses
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.CourseId == request.CourseId, cancellationToken);

        if (course == null)
        {
            _logger.LogWarning("Course {CourseId} not found", request.CourseId);
            return null;
        }

        return course.ToDto();
    }
}
