using JobSearchOrganizer.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JobSearchOrganizer.Api.Features.Interviews;

public record CreateInterviewCommand : IRequest<InterviewDto>
{
    public Guid UserId { get; init; }
    public Guid ApplicationId { get; init; }
    public string InterviewType { get; init; } = string.Empty;
    public DateTime ScheduledDateTime { get; init; }
    public int? DurationMinutes { get; init; }
    public List<string> Interviewers { get; init; } = new List<string>();
    public string? Location { get; init; }
    public string? PreparationNotes { get; init; }
}

public class CreateInterviewCommandHandler : IRequestHandler<CreateInterviewCommand, InterviewDto>
{
    private readonly IJobSearchOrganizerContext _context;
    private readonly ILogger<CreateInterviewCommandHandler> _logger;

    public CreateInterviewCommandHandler(
        IJobSearchOrganizerContext context,
        ILogger<CreateInterviewCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<InterviewDto> Handle(CreateInterviewCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating interview for user {UserId}, type: {InterviewType}",
            request.UserId,
            request.InterviewType);

        var interview = new Interview
        {
            InterviewId = Guid.NewGuid(),
            UserId = request.UserId,
            ApplicationId = request.ApplicationId,
            InterviewType = request.InterviewType,
            ScheduledDateTime = request.ScheduledDateTime,
            DurationMinutes = request.DurationMinutes,
            Interviewers = request.Interviewers,
            Location = request.Location,
            PreparationNotes = request.PreparationNotes,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Interviews.Add(interview);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created interview {InterviewId} for user {UserId}",
            interview.InterviewId,
            request.UserId);

        return interview.ToDto();
    }
}
