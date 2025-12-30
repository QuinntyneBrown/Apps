using HomeMaintenanceSchedule.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HomeMaintenanceSchedule.Api.Features.ServiceLogs;

public record CreateServiceLogCommand : IRequest<ServiceLogDto>
{
    public Guid MaintenanceTaskId { get; init; }
    public DateTime ServiceDate { get; init; }
    public string Description { get; init; } = string.Empty;
    public Guid? ContractorId { get; init; }
    public decimal? Cost { get; init; }
    public string? Notes { get; init; }
    public string? PartsUsed { get; init; }
    public decimal? LaborHours { get; init; }
    public DateTime? WarrantyExpiresAt { get; init; }
}

public class CreateServiceLogCommandHandler : IRequestHandler<CreateServiceLogCommand, ServiceLogDto>
{
    private readonly IHomeMaintenanceScheduleContext _context;
    private readonly ILogger<CreateServiceLogCommandHandler> _logger;

    public CreateServiceLogCommandHandler(
        IHomeMaintenanceScheduleContext context,
        ILogger<CreateServiceLogCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ServiceLogDto> Handle(CreateServiceLogCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Creating service log for maintenance task {MaintenanceTaskId}",
            request.MaintenanceTaskId);

        var log = new ServiceLog
        {
            ServiceLogId = Guid.NewGuid(),
            MaintenanceTaskId = request.MaintenanceTaskId,
            ServiceDate = request.ServiceDate,
            Description = request.Description,
            ContractorId = request.ContractorId,
            Cost = request.Cost,
            Notes = request.Notes,
            PartsUsed = request.PartsUsed,
            LaborHours = request.LaborHours,
            WarrantyExpiresAt = request.WarrantyExpiresAt,
            CreatedAt = DateTime.UtcNow,
        };

        _context.ServiceLogs.Add(log);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Created service log {ServiceLogId} for maintenance task {MaintenanceTaskId}",
            log.ServiceLogId,
            request.MaintenanceTaskId);

        return log.ToDto();
    }
}
