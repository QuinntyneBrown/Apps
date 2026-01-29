using DateManagement.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DateManagement.Api.Features;

public record DeleteImportantDateCommand(Guid ImportantDateId) : IRequest<bool>;

public class DeleteImportantDateCommandHandler : IRequestHandler<DeleteImportantDateCommand, bool>
{
    private readonly IDateManagementDbContext _context;
    private readonly ILogger<DeleteImportantDateCommandHandler> _logger;

    public DeleteImportantDateCommandHandler(IDateManagementDbContext context, ILogger<DeleteImportantDateCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteImportantDateCommand request, CancellationToken cancellationToken)
    {
        var importantDate = await _context.ImportantDates
            .FirstOrDefaultAsync(d => d.ImportantDateId == request.ImportantDateId, cancellationToken);

        if (importantDate == null) return false;

        _context.ImportantDates.Remove(importantDate);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Important date deleted: {ImportantDateId}", request.ImportantDateId);
        return true;
    }
}
