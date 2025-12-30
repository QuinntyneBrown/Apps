using MensGroupDiscussionTracker.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MensGroupDiscussionTracker.Api.Features.Topics;

public record CreateTopicCommand : IRequest<TopicDto>
{
    public Guid? MeetingId { get; init; }
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public TopicCategory Category { get; init; }
    public string? DiscussionNotes { get; init; }
}

public class CreateTopicCommandHandler : IRequestHandler<CreateTopicCommand, TopicDto>
{
    private readonly IMensGroupDiscussionTrackerContext _context;
    private readonly ILogger<CreateTopicCommandHandler> _logger;

    public CreateTopicCommandHandler(
        IMensGroupDiscussionTrackerContext context,
        ILogger<CreateTopicCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TopicDto> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating topic for user {UserId}, title: {Title}",
            request.UserId,
            request.Title);

        var topic = new Topic
        {
            TopicId = Guid.NewGuid(),
            MeetingId = request.MeetingId,
            UserId = request.UserId,
            Title = request.Title,
            Description = request.Description,
            Category = request.Category,
            DiscussionNotes = request.DiscussionNotes,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Topics.Add(topic);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created topic {TopicId} for user {UserId}",
            topic.TopicId,
            request.UserId);

        return topic.ToDto();
    }
}
