using SideHustleIncomeTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SideHustleIncomeTracker.Api.Features.Businesses;

public record DeleteBusinessCommand : IRequest<bool>
{
    public Guid BusinessId { get; init; }
}

public class DeleteBusinessCommandHandler : IRequestHandler<DeleteBusinessCommand, bool>
{
    private readonly ISideHustleIncomeTrackerContext _context;
    private readonly ILogger<DeleteBusinessCommandHandler> _logger;

    public DeleteBusinessCommandHandler(
        ISideHustleIncomeTrackerContext context,
        ILogger<DeleteBusinessCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteBusinessCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting business {BusinessId}", request.BusinessId);

        var business = await _context.Businesses
            .FirstOrDefaultAsync(b => b.BusinessId == request.BusinessId, cancellationToken);

        if (business == null)
        {
            _logger.LogWarning("Business {BusinessId} not found", request.BusinessId);
            return false;
        }

        _context.Businesses.Remove(business);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted business {BusinessId}", request.BusinessId);

        return true;
    }
}
