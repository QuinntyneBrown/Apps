using MensGroupDiscussionTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MensGroupDiscussionTracker.Api.Features.Topics;

public record GetTopicByIdQuery : IRequest<TopicDto?>
{
    public Guid TopicId { get; init; }
}

public class GetTopicByIdQueryHandler : IRequestHandler<GetTopicByIdQuery, TopicDto?>
{
    private readonly IMensGroupDiscussionTrackerContext _context;
    private readonly ILogger<GetTopicByIdQueryHandler> _logger;

    public GetTopicByIdQueryHandler(
        IMensGroupDiscussionTrackerContext context,
        ILogger<GetTopicByIdQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TopicDto?> Handle(GetTopicByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting topic {TopicId}", request.TopicId);

        var topic = await _context.Topics
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.TopicId == request.TopicId, cancellationToken);

        return topic?.ToDto();
    }
}
