using TimeBlocks.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TimeBlocks.Api.Features;

public record DeleteTimeBlockCommand(Guid TimeBlockId) : IRequest<bool>;

public class DeleteTimeBlockCommandHandler : IRequestHandler<DeleteTimeBlockCommand, bool>
{
    private readonly ITimeBlocksDbContext _context;
    private readonly ILogger<DeleteTimeBlockCommandHandler> _logger;

    public DeleteTimeBlockCommandHandler(ITimeBlocksDbContext context, ILogger<DeleteTimeBlockCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteTimeBlockCommand request, CancellationToken cancellationToken)
    {
        var timeBlock = await _context.TimeBlocks
            .FirstOrDefaultAsync(t => t.TimeBlockId == request.TimeBlockId, cancellationToken);

        if (timeBlock == null) return false;

        _context.TimeBlocks.Remove(timeBlock);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("TimeBlock deleted: {TimeBlockId}", request.TimeBlockId);

        return true;
    }
}
