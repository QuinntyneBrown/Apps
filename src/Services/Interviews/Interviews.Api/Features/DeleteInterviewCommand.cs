using Interviews.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Interviews.Api.Features;

public record DeleteInterviewCommand(Guid InterviewId) : IRequest<bool>;

public class DeleteInterviewCommandHandler : IRequestHandler<DeleteInterviewCommand, bool>
{
    private readonly IInterviewsDbContext _context;
    private readonly ILogger<DeleteInterviewCommandHandler> _logger;

    public DeleteInterviewCommandHandler(IInterviewsDbContext context, ILogger<DeleteInterviewCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteInterviewCommand request, CancellationToken cancellationToken)
    {
        var interview = await _context.Interviews
            .FirstOrDefaultAsync(i => i.InterviewId == request.InterviewId, cancellationToken);

        if (interview == null) return false;

        _context.Interviews.Remove(interview);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Interview deleted: {InterviewId}", request.InterviewId);

        return true;
    }
}
