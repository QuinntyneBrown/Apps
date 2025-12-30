// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeGymEquipmentManager.Core;
using MediatR;

namespace HomeGymEquipmentManager.Api.Features.Maintenance.Commands;

public class CreateMaintenanceCommand : IRequest<MaintenanceDto>
{
    public Guid UserId { get; set; }
    public Guid EquipmentId { get; set; }
    public DateTime MaintenanceDate { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal? Cost { get; set; }
    public DateTime? NextMaintenanceDate { get; set; }
    public string? Notes { get; set; }
}

public class CreateMaintenanceCommandHandler : IRequestHandler<CreateMaintenanceCommand, MaintenanceDto>
{
    private readonly IHomeGymEquipmentManagerContext _context;

    public CreateMaintenanceCommandHandler(IHomeGymEquipmentManagerContext context)
    {
        _context = context;
    }

    public async Task<MaintenanceDto> Handle(CreateMaintenanceCommand request, CancellationToken cancellationToken)
    {
        var maintenance = new Core.Maintenance
        {
            MaintenanceId = Guid.NewGuid(),
            UserId = request.UserId,
            EquipmentId = request.EquipmentId,
            MaintenanceDate = request.MaintenanceDate,
            Description = request.Description,
            Cost = request.Cost,
            NextMaintenanceDate = request.NextMaintenanceDate,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow
        };

        _context.Maintenances.Add(maintenance);
        await _context.SaveChangesAsync(cancellationToken);

        return MaintenanceDto.FromEntity(maintenance);
    }
}
