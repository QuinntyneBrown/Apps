using BucketListManager.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BucketListManager.Api.Features.Memories;

public record DeleteMemoryCommand : IRequest<bool>
{
    public Guid MemoryId { get; init; }
}

public class DeleteMemoryCommandHandler : IRequestHandler<DeleteMemoryCommand, bool>
{
    private readonly IBucketListManagerContext _context;
    private readonly ILogger<DeleteMemoryCommandHandler> _logger;

    public DeleteMemoryCommandHandler(
        IBucketListManagerContext context,
        ILogger<DeleteMemoryCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteMemoryCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting memory {MemoryId}", request.MemoryId);

        var memory = await _context.Memories
            .FirstOrDefaultAsync(x => x.MemoryId == request.MemoryId, cancellationToken);

        if (memory == null)
        {
            _logger.LogWarning("Memory {MemoryId} not found", request.MemoryId);
            return false;
        }

        _context.Memories.Remove(memory);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted memory {MemoryId}", request.MemoryId);

        return true;
    }
}
