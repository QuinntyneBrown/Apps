using SkillDevelopmentTracker.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SkillDevelopmentTracker.Api.Features.Courses;

public record CreateCourseCommand : IRequest<CourseDto>
{
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Provider { get; init; } = string.Empty;
    public string? Instructor { get; init; }
    public string? CourseUrl { get; init; }
    public DateTime? StartDate { get; init; }
    public decimal? EstimatedHours { get; init; }
    public string? Notes { get; init; }
}

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, CourseDto>
{
    private readonly ISkillDevelopmentTrackerContext _context;
    private readonly ILogger<CreateCourseCommandHandler> _logger;

    public CreateCourseCommandHandler(
        ISkillDevelopmentTrackerContext context,
        ILogger<CreateCourseCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<CourseDto> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating course for user {UserId}, title: {Title}",
            request.UserId,
            request.Title);

        var course = new Course
        {
            CourseId = Guid.NewGuid(),
            UserId = request.UserId,
            Title = request.Title,
            Provider = request.Provider,
            Instructor = request.Instructor,
            CourseUrl = request.CourseUrl,
            StartDate = request.StartDate,
            EstimatedHours = request.EstimatedHours,
            Notes = request.Notes,
            ProgressPercentage = 0,
            ActualHours = 0,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Courses.Add(course);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created course {CourseId} for user {UserId}",
            course.CourseId,
            request.UserId);

        return course.ToDto();
    }
}
