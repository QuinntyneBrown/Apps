using HomeMaintenanceSchedule.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeMaintenanceSchedule.Api.Features.Contractors;

public record DeleteContractorCommand : IRequest<bool>
{
    public Guid ContractorId { get; init; }
}

public class DeleteContractorCommandHandler : IRequestHandler<DeleteContractorCommand, bool>
{
    private readonly IHomeMaintenanceScheduleContext _context;
    private readonly ILogger<DeleteContractorCommandHandler> _logger;

    public DeleteContractorCommandHandler(
        IHomeMaintenanceScheduleContext context,
        ILogger<DeleteContractorCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> Handle(DeleteContractorCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting contractor {ContractorId}", request.ContractorId);

        var contractor = await _context.Contractors
            .FirstOrDefaultAsync(c => c.ContractorId == request.ContractorId, cancellationToken);

        if (contractor == null)
        {
            _logger.LogWarning("Contractor {ContractorId} not found", request.ContractorId);
            return false;
        }

        _context.Contractors.Remove(contractor);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Deleted contractor {ContractorId}", request.ContractorId);

        return true;
    }
}
