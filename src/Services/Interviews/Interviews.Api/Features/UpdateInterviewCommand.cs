using Interviews.Core;
using Interviews.Core.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Interviews.Api.Features;

public record UpdateInterviewCommand(
    Guid InterviewId,
    DateTime ScheduledDate,
    InterviewType Type,
    string? InterviewerName,
    string? Location,
    string? MeetingLink,
    string? Notes,
    InterviewStatus Status,
    string? Feedback) : IRequest<InterviewDto?>;

public class UpdateInterviewCommandHandler : IRequestHandler<UpdateInterviewCommand, InterviewDto?>
{
    private readonly IInterviewsDbContext _context;
    private readonly ILogger<UpdateInterviewCommandHandler> _logger;

    public UpdateInterviewCommandHandler(IInterviewsDbContext context, ILogger<UpdateInterviewCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<InterviewDto?> Handle(UpdateInterviewCommand request, CancellationToken cancellationToken)
    {
        var interview = await _context.Interviews
            .FirstOrDefaultAsync(i => i.InterviewId == request.InterviewId, cancellationToken);

        if (interview == null) return null;

        interview.ScheduledDate = request.ScheduledDate;
        interview.Type = request.Type;
        interview.InterviewerName = request.InterviewerName;
        interview.Location = request.Location;
        interview.MeetingLink = request.MeetingLink;
        interview.Notes = request.Notes;
        interview.Status = request.Status;
        interview.Feedback = request.Feedback;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Interview updated: {InterviewId}", interview.InterviewId);

        return interview.ToDto();
    }
}
