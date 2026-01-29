using Carpools.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Carpools.Api.Features;

public record DeleteCarpoolCommand(Guid CarpoolId) : IRequest<bool>;

public class DeleteCarpoolCommandHandler : IRequestHandler<DeleteCarpoolCommand, bool>
{
    private readonly ICarpoolsDbContext _context;
    private readonly ILogger<DeleteCarpoolCommandHandler> _logger;

    public DeleteCarpoolCommandHandler(ICarpoolsDbContext context, ILogger<DeleteCarpoolCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteCarpoolCommand request, CancellationToken cancellationToken)
    {
        var carpool = await _context.Carpools
            .FirstOrDefaultAsync(c => c.CarpoolId == request.CarpoolId, cancellationToken);

        if (carpool == null) return false;

        _context.Carpools.Remove(carpool);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Carpool deleted: {CarpoolId}", request.CarpoolId);

        return true;
    }
}
