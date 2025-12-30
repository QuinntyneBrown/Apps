using MensGroupDiscussionTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MensGroupDiscussionTracker.Api.Features.Topics;

public record GetTopicsQuery : IRequest<IEnumerable<TopicDto>>
{
    public Guid? MeetingId { get; init; }
    public Guid? UserId { get; init; }
    public TopicCategory? Category { get; init; }
}

public class GetTopicsQueryHandler : IRequestHandler<GetTopicsQuery, IEnumerable<TopicDto>>
{
    private readonly IMensGroupDiscussionTrackerContext _context;
    private readonly ILogger<GetTopicsQueryHandler> _logger;

    public GetTopicsQueryHandler(
        IMensGroupDiscussionTrackerContext context,
        ILogger<GetTopicsQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<TopicDto>> Handle(GetTopicsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting topics for user {UserId}", request.UserId);

        var query = _context.Topics.AsNoTracking();

        if (request.MeetingId.HasValue)
        {
            query = query.Where(t => t.MeetingId == request.MeetingId.Value);
        }

        if (request.UserId.HasValue)
        {
            query = query.Where(t => t.UserId == request.UserId.Value);
        }

        if (request.Category.HasValue)
        {
            query = query.Where(t => t.Category == request.Category.Value);
        }

        var topics = await query
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync(cancellationToken);

        return topics.Select(t => t.ToDto());
    }
}
