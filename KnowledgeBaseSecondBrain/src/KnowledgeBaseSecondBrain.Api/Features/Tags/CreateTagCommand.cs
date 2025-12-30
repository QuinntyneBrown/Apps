using KnowledgeBaseSecondBrain.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace KnowledgeBaseSecondBrain.Api.Features.Tags;

public record CreateTagCommand : IRequest<TagDto>
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Color { get; init; }
}

public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, TagDto>
{
    private readonly IKnowledgeBaseSecondBrainContext _context;
    private readonly ILogger<CreateTagCommandHandler> _logger;

    public CreateTagCommandHandler(
        IKnowledgeBaseSecondBrainContext context,
        ILogger<CreateTagCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TagDto> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating tag for user {UserId}, name: {Name}",
            request.UserId,
            request.Name);

        var tag = new Tag
        {
            TagId = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            Color = request.Color,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Tags.Add(tag);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created tag {TagId} for user {UserId}",
            tag.TagId,
            request.UserId);

        return tag.ToDto();
    }
}
