using JobSearchOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JobSearchOrganizer.Api.Features.Interviews;

public record GetInterviewByIdQuery : IRequest<InterviewDto?>
{
    public Guid InterviewId { get; init; }
}

public class GetInterviewByIdQueryHandler : IRequestHandler<GetInterviewByIdQuery, InterviewDto?>
{
    private readonly IJobSearchOrganizerContext _context;
    private readonly ILogger<GetInterviewByIdQueryHandler> _logger;

    public GetInterviewByIdQueryHandler(
        IJobSearchOrganizerContext context,
        ILogger<GetInterviewByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<InterviewDto?> Handle(GetInterviewByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting interview {InterviewId}", request.InterviewId);

        var interview = await _context.Interviews
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.InterviewId == request.InterviewId, cancellationToken);

        return interview?.ToDto();
    }
}
