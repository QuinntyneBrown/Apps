using JobSearchOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JobSearchOrganizer.Api.Features.Interviews;

public record UpdateInterviewCommand : IRequest<InterviewDto?>
{
    public Guid InterviewId { get; init; }
    public string InterviewType { get; init; } = string.Empty;
    public DateTime ScheduledDateTime { get; init; }
    public int? DurationMinutes { get; init; }
    public List<string> Interviewers { get; init; } = new List<string>();
    public string? Location { get; init; }
    public string? PreparationNotes { get; init; }
    public string? Feedback { get; init; }
    public bool IsCompleted { get; init; }
}

public class UpdateInterviewCommandHandler : IRequestHandler<UpdateInterviewCommand, InterviewDto?>
{
    private readonly IJobSearchOrganizerContext _context;
    private readonly ILogger<UpdateInterviewCommandHandler> _logger;

    public UpdateInterviewCommandHandler(
        IJobSearchOrganizerContext context,
        ILogger<UpdateInterviewCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<InterviewDto?> Handle(UpdateInterviewCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating interview {InterviewId}", request.InterviewId);

        var interview = await _context.Interviews
            .FirstOrDefaultAsync(i => i.InterviewId == request.InterviewId, cancellationToken);

        if (interview == null)
        {
            _logger.LogWarning("Interview {InterviewId} not found", request.InterviewId);
            return null;
        }

        interview.InterviewType = request.InterviewType;
        interview.ScheduledDateTime = request.ScheduledDateTime;
        interview.DurationMinutes = request.DurationMinutes;
        interview.Interviewers = request.Interviewers;
        interview.Location = request.Location;
        interview.PreparationNotes = request.PreparationNotes;
        interview.Feedback = request.Feedback;
        interview.IsCompleted = request.IsCompleted;
        if (request.IsCompleted && interview.CompletedDate == null)
        {
            interview.CompletedDate = DateTime.UtcNow;
        }
        interview.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated interview {InterviewId}", request.InterviewId);

        return interview.ToDto();
    }
}
