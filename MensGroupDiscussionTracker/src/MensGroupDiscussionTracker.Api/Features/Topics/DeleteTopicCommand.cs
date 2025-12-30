using MensGroupDiscussionTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MensGroupDiscussionTracker.Api.Features.Topics;

public record DeleteTopicCommand : IRequest<bool>
{
    public Guid TopicId { get; init; }
}

public class DeleteTopicCommandHandler : IRequestHandler<DeleteTopicCommand, bool>
{
    private readonly IMensGroupDiscussionTrackerContext _context;
    private readonly ILogger<DeleteTopicCommandHandler> _logger;

    public DeleteTopicCommandHandler(
        IMensGroupDiscussionTrackerContext context,
        ILogger<DeleteTopicCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteTopicCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting topic {TopicId}", request.TopicId);

        var topic = await _context.Topics
            .FirstOrDefaultAsync(t => t.TopicId == request.TopicId, cancellationToken);

        if (topic == null)
        {
            _logger.LogWarning("Topic {TopicId} not found", request.TopicId);
            return false;
        }

        _context.Topics.Remove(topic);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted topic {TopicId}", request.TopicId);

        return true;
    }
}
