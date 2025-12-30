using DailyJournalingApp.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DailyJournalingApp.Api.Features.Tags;

public record CreateTagCommand : IRequest<TagDto>
{
    public Guid UserId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Color { get; init; }
}

public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, TagDto>
{
    private readonly IDailyJournalingAppContext _context;
    private readonly ILogger<CreateTagCommandHandler> _logger;

    public CreateTagCommandHandler(
        IDailyJournalingAppContext context,
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
            UsageCount = 0,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Tags.Add(tag);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Created tag {TagId}", tag.TagId);

        return tag.ToDto();
    }
}
