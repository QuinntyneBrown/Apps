using JobSearchOrganizer.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JobSearchOrganizer.Api.Features.Interviews;

public record GetInterviewsQuery : IRequest<IEnumerable<InterviewDto>>
{
    public Guid? UserId { get; init; }
    public Guid? ApplicationId { get; init; }
    public string? InterviewType { get; init; }
    public bool? IsCompleted { get; init; }
}

public class GetInterviewsQueryHandler : IRequestHandler<GetInterviewsQuery, IEnumerable<InterviewDto>>
{
    private readonly IJobSearchOrganizerContext _context;
    private readonly ILogger<GetInterviewsQueryHandler> _logger;

    public GetInterviewsQueryHandler(
        IJobSearchOrganizerContext context,
        ILogger<GetInterviewsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<InterviewDto>> Handle(GetInterviewsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting interviews for user {UserId}", request.UserId);

        var query = _context.Interviews.AsNoTracking();

        if (request.UserId.HasValue)
        {
            query = query.Where(i => i.UserId == request.UserId.Value);
        }

        if (request.ApplicationId.HasValue)
        {
            query = query.Where(i => i.ApplicationId == request.ApplicationId.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.InterviewType))
        {
            query = query.Where(i => i.InterviewType == request.InterviewType);
        }

        if (request.IsCompleted.HasValue)
        {
            query = query.Where(i => i.IsCompleted == request.IsCompleted.Value);
        }

        var interviews = await query
            .OrderBy(i => i.ScheduledDateTime)
            .ToListAsync(cancellationToken);

        return interviews.Select(i => i.ToDto());
    }
}
