using HomeMaintenanceSchedule.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HomeMaintenanceSchedule.Api.Features.ServiceLogs;

public record UpdateServiceLogCommand : IRequest<ServiceLogDto?>
{
    public Guid ServiceLogId { get; init; }
    public DateTime ServiceDate { get; init; }
    public string Description { get; init; } = string.Empty;
    public Guid? ContractorId { get; init; }
    public decimal? Cost { get; init; }
    public string? Notes { get; init; }
    public string? PartsUsed { get; init; }
    public decimal? LaborHours { get; init; }
    public DateTime? WarrantyExpiresAt { get; init; }
}

public class UpdateServiceLogCommandHandler : IRequestHandler<UpdateServiceLogCommand, ServiceLogDto?>
{
    private readonly IHomeMaintenanceScheduleContext _context;
    private readonly ILogger<UpdateServiceLogCommandHandler> _logger;

    public UpdateServiceLogCommandHandler(
        IHomeMaintenanceScheduleContext context,
        ILogger<UpdateServiceLogCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ServiceLogDto?> Handle(UpdateServiceLogCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating service log {ServiceLogId}", request.ServiceLogId);

        var log = await _context.ServiceLogs
            .FirstOrDefaultAsync(l => l.ServiceLogId == request.ServiceLogId, cancellationToken);

        if (log == null)
        {
            _logger.LogWarning("Service log {ServiceLogId} not found", request.ServiceLogId);
            return null;
        }

        log.ServiceDate = request.ServiceDate;
        log.Description = request.Description;
        log.ContractorId = request.ContractorId;
        log.Cost = request.Cost;
        log.Notes = request.Notes;
        log.PartsUsed = request.PartsUsed;
        log.LaborHours = request.LaborHours;
        log.WarrantyExpiresAt = request.WarrantyExpiresAt;

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Updated service log {ServiceLogId}", request.ServiceLogId);

        return log.ToDto();
    }
}
