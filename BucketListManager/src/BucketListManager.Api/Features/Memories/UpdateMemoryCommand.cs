using BucketListManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BucketListManager.Api.Features.Memories;

public record UpdateMemoryCommand : IRequest<MemoryDto?>
{
    public Guid MemoryId { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public DateTime MemoryDate { get; init; }
    public string? PhotoUrl { get; init; }
}

public class UpdateMemoryCommandHandler : IRequestHandler<UpdateMemoryCommand, MemoryDto?>
{
    private readonly IBucketListManagerContext _context;
    private readonly ILogger<UpdateMemoryCommandHandler> _logger;

    public UpdateMemoryCommandHandler(
        IBucketListManagerContext context,
        ILogger<UpdateMemoryCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<MemoryDto?> Handle(UpdateMemoryCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating memory {MemoryId}", request.MemoryId);

        var memory = await _context.Memories
            .FirstOrDefaultAsync(x => x.MemoryId == request.MemoryId, cancellationToken);

        if (memory == null)
        {
            _logger.LogWarning("Memory {MemoryId} not found", request.MemoryId);
            return null;
        }

        memory.Title = request.Title;
        memory.Description = request.Description;
        memory.MemoryDate = request.MemoryDate;
        memory.PhotoUrl = request.PhotoUrl;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated memory {MemoryId}", request.MemoryId);

        return memory.ToDto();
    }
}
