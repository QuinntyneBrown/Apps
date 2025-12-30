using SkillDevelopmentTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SkillDevelopmentTracker.Api.Features.Courses;

public record UpdateCourseCommand : IRequest<CourseDto?>
{
    public Guid CourseId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Provider { get; init; } = string.Empty;
    public string? Instructor { get; init; }
    public string? CourseUrl { get; init; }
    public DateTime? StartDate { get; init; }
    public int ProgressPercentage { get; init; }
    public decimal? EstimatedHours { get; init; }
    public decimal ActualHours { get; init; }
    public string? Notes { get; init; }
}

public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, CourseDto?>
{
    private readonly ISkillDevelopmentTrackerContext _context;
    private readonly ILogger<UpdateCourseCommandHandler> _logger;

    public UpdateCourseCommandHandler(
        ISkillDevelopmentTrackerContext context,
        ILogger<UpdateCourseCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CourseDto?> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating course {CourseId}", request.CourseId);

        var course = await _context.Courses
            .FirstOrDefaultAsync(c => c.CourseId == request.CourseId, cancellationToken);

        if (course == null)
        {
            _logger.LogWarning("Course {CourseId} not found", request.CourseId);
            return null;
        }

        course.Title = request.Title;
        course.Provider = request.Provider;
        course.Instructor = request.Instructor;
        course.CourseUrl = request.CourseUrl;
        course.StartDate = request.StartDate;
        course.EstimatedHours = request.EstimatedHours;
        course.ActualHours = request.ActualHours;
        course.Notes = request.Notes;
        course.UpdateProgress(request.ProgressPercentage);

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated course {CourseId}", request.CourseId);

        return course.ToDto();
    }
}
