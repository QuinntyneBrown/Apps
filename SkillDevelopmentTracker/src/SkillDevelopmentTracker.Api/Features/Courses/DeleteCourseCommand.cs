using SkillDevelopmentTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SkillDevelopmentTracker.Api.Features.Courses;

public record DeleteCourseCommand : IRequest<bool>
{
    public Guid CourseId { get; init; }
}

public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, bool>
{
    private readonly ISkillDevelopmentTrackerContext _context;
    private readonly ILogger<DeleteCourseCommandHandler> _logger;

    public DeleteCourseCommandHandler(
        ISkillDevelopmentTrackerContext context,
        ILogger<DeleteCourseCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting course {CourseId}", request.CourseId);

        var course = await _context.Courses
            .FirstOrDefaultAsync(c => c.CourseId == request.CourseId, cancellationToken);

        if (course == null)
        {
            _logger.LogWarning("Course {CourseId} not found", request.CourseId);
            return false;
        }

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted course {CourseId}", request.CourseId);

        return true;
    }
}
