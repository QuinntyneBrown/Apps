using BucketListManager.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BucketListManager.Api.Features.BucketListItems;

public record CreateBucketListItemCommand : IRequest<BucketListItemDto>
{
    public Guid UserId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public Category Category { get; init; }
    public Priority Priority { get; init; } = Priority.Medium;
    public DateTime? TargetDate { get; init; }
    public string? Notes { get; init; }
}

public class CreateBucketListItemCommandHandler : IRequestHandler<CreateBucketListItemCommand, BucketListItemDto>
{
    private readonly IBucketListManagerContext _context;
    private readonly ILogger<CreateBucketListItemCommandHandler> _logger;

    public CreateBucketListItemCommandHandler(
        IBucketListManagerContext context,
        ILogger<CreateBucketListItemCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<BucketListItemDto> Handle(CreateBucketListItemCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating bucket list item for user {UserId}, title: {Title}",
            request.UserId,
            request.Title);

        var item = new BucketListItem
        {
            BucketListItemId = Guid.NewGuid(),
            UserId = request.UserId,
            Title = request.Title,
            Description = request.Description,
            Category = request.Category,
            Priority = request.Priority,
            Status = ItemStatus.NotStarted,
            TargetDate = request.TargetDate,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
        };

        _context.BucketListItems.Add(item);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created bucket list item {BucketListItemId} for user {UserId}",
            item.BucketListItemId,
            request.UserId);

        return item.ToDto();
    }
}
