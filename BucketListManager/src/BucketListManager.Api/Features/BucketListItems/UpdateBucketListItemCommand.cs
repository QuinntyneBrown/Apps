using BucketListManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BucketListManager.Api.Features.BucketListItems;

public record UpdateBucketListItemCommand : IRequest<BucketListItemDto?>
{
    public Guid BucketListItemId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public Category Category { get; init; }
    public Priority Priority { get; init; }
    public ItemStatus Status { get; init; }
    public DateTime? TargetDate { get; init; }
    public DateTime? CompletedDate { get; init; }
    public string? Notes { get; init; }
}

public class UpdateBucketListItemCommandHandler : IRequestHandler<UpdateBucketListItemCommand, BucketListItemDto?>
{
    private readonly IBucketListManagerContext _context;
    private readonly ILogger<UpdateBucketListItemCommandHandler> _logger;

    public UpdateBucketListItemCommandHandler(
        IBucketListManagerContext context,
        ILogger<UpdateBucketListItemCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<BucketListItemDto?> Handle(UpdateBucketListItemCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating bucket list item {BucketListItemId}", request.BucketListItemId);

        var item = await _context.BucketListItems
            .FirstOrDefaultAsync(x => x.BucketListItemId == request.BucketListItemId, cancellationToken);

        if (item == null)
        {
            _logger.LogWarning("Bucket list item {BucketListItemId} not found", request.BucketListItemId);
            return null;
        }

        item.Title = request.Title;
        item.Description = request.Description;
        item.Category = request.Category;
        item.Priority = request.Priority;
        item.Status = request.Status;
        item.TargetDate = request.TargetDate;
        item.CompletedDate = request.CompletedDate;
        item.Notes = request.Notes;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated bucket list item {BucketListItemId}", request.BucketListItemId);

        return item.ToDto();
    }
}
