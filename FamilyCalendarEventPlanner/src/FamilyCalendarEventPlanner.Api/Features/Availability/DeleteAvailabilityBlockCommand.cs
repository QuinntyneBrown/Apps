using FamilyCalendarEventPlanner.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FamilyCalendarEventPlanner.Api.Features.Availability;

public record DeleteAvailabilityBlockCommand : IRequest<bool>
{
    public Guid BlockId { get; init; }
}

public class DeleteAvailabilityBlockCommandHandler : IRequestHandler<DeleteAvailabilityBlockCommand, bool>
{
    private readonly IFamilyCalendarEventPlannerContext _context;
    private readonly ILogger<DeleteAvailabilityBlockCommandHandler> _logger;

    public DeleteAvailabilityBlockCommandHandler(
        IFamilyCalendarEventPlannerContext context,
        ILogger<DeleteAvailabilityBlockCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteAvailabilityBlockCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting availability block {BlockId}", request.BlockId);

        var block = await _context.AvailabilityBlocks
            .FirstOrDefaultAsync(b => b.BlockId == request.BlockId, cancellationToken);

        if (block == null)
        {
            _logger.LogWarning("Availability block {BlockId} not found", request.BlockId);
            return false;
        }

        _context.AvailabilityBlocks.Remove(block);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted availability block {BlockId}", request.BlockId);

        return true;
    }
}
