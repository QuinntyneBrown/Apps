using BucketListManager.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BucketListManager.Api.Features.Memories;

public record CreateMemoryCommand : IRequest<MemoryDto>
{
    public Guid UserId { get; init; }
    public Guid BucketListItemId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public DateTime? MemoryDate { get; init; }
    public string? PhotoUrl { get; init; }
}

public class CreateMemoryCommandHandler : IRequestHandler<CreateMemoryCommand, MemoryDto>
{
    private readonly IBucketListManagerContext _context;
    private readonly ILogger<CreateMemoryCommandHandler> _logger;

    public CreateMemoryCommandHandler(
        IBucketListManagerContext context,
        ILogger<CreateMemoryCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MemoryDto> Handle(CreateMemoryCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating memory for bucket list item {BucketListItemId}, title: {Title}",
            request.BucketListItemId,
            request.Title);

        var memory = new Memory
        {
            MemoryId = Guid.NewGuid(),
            UserId = request.UserId,
            BucketListItemId = request.BucketListItemId,
            Title = request.Title,
            Description = request.Description,
            MemoryDate = request.MemoryDate ?? DateTime.UtcNow,
            PhotoUrl = request.PhotoUrl,
            CreatedAt = DateTime.UtcNow,
        };

        _context.Memories.Add(memory);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created memory {MemoryId} for bucket list item {BucketListItemId}",
            memory.MemoryId,
            request.BucketListItemId);

        return memory.ToDto();
    }
}
