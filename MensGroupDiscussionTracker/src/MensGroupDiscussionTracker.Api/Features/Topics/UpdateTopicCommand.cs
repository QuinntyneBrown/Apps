using MensGroupDiscussionTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MensGroupDiscussionTracker.Api.Features.Topics;

public record UpdateTopicCommand : IRequest<TopicDto?>
{
    public Guid TopicId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public TopicCategory Category { get; init; }
    public string? DiscussionNotes { get; init; }
}

public class UpdateTopicCommandHandler : IRequestHandler<UpdateTopicCommand, TopicDto?>
{
    private readonly IMensGroupDiscussionTrackerContext _context;
    private readonly ILogger<UpdateTopicCommandHandler> _logger;

    public UpdateTopicCommandHandler(
        IMensGroupDiscussionTrackerContext context,
        ILogger<UpdateTopicCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TopicDto?> Handle(UpdateTopicCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating topic {TopicId}", request.TopicId);

        var topic = await _context.Topics
            .FirstOrDefaultAsync(t => t.TopicId == request.TopicId, cancellationToken);

        if (topic == null)
        {
            _logger.LogWarning("Topic {TopicId} not found", request.TopicId);
            return null;
        }

        topic.Title = request.Title;
        topic.Description = request.Description;
        topic.Category = request.Category;
        topic.DiscussionNotes = request.DiscussionNotes;
        topic.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated topic {TopicId}", request.TopicId);

        return topic.ToDto();
    }
}
