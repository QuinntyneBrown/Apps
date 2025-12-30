using JobSearchOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JobSearchOrganizer.Api.Features.Interviews;

public record DeleteInterviewCommand : IRequest<bool>
{
    public Guid InterviewId { get; init; }
}

public class DeleteInterviewCommandHandler : IRequestHandler<DeleteInterviewCommand, bool>
{
    private readonly IJobSearchOrganizerContext _context;
    private readonly ILogger<DeleteInterviewCommandHandler> _logger;

    public DeleteInterviewCommandHandler(
        IJobSearchOrganizerContext context,
        ILogger<DeleteInterviewCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteInterviewCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting interview {InterviewId}", request.InterviewId);

        var interview = await _context.Interviews
            .FirstOrDefaultAsync(i => i.InterviewId == request.InterviewId, cancellationToken);

        if (interview == null)
        {
            _logger.LogWarning("Interview {InterviewId} not found", request.InterviewId);
            return false;
        }

        _context.Interviews.Remove(interview);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted interview {InterviewId}", request.InterviewId);

        return true;
    }
}
