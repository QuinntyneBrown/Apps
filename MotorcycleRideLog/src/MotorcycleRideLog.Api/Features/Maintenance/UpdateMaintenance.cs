// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MotorcycleRideLog.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MotorcycleRideLog.Api.Features.Maintenance;

public class UpdateMaintenance
{
    public class Command : IRequest<MaintenanceDto>
    {
        public Guid MaintenanceId { get; set; }
        public Guid MotorcycleId { get; set; }
        public DateTime MaintenanceDate { get; set; }
        public MaintenanceType Type { get; set; }
        public int? MileageAtMaintenance { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal? Cost { get; set; }
        public string? ServiceProvider { get; set; }
        public string? PartsReplaced { get; set; }
        public string? Notes { get; set; }
    }

    public class Handler : IRequestHandler<Command, MaintenanceDto>
    {
        private readonly IMotorcycleRideLogContext _context;

        public Handler(IMotorcycleRideLogContext context)
        {
            _context = context;
        }

        public async Task<MaintenanceDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var maintenance = await _context.MaintenanceRecords
                .FirstOrDefaultAsync(x => x.MaintenanceId == request.MaintenanceId, cancellationToken)
                ?? throw new KeyNotFoundException($"Maintenance with ID {request.MaintenanceId} not found.");

            maintenance.MotorcycleId = request.MotorcycleId;
            maintenance.MaintenanceDate = request.MaintenanceDate;
            maintenance.Type = request.Type;
            maintenance.MileageAtMaintenance = request.MileageAtMaintenance;
            maintenance.Description = request.Description;
            maintenance.Cost = request.Cost;
            maintenance.ServiceProvider = request.ServiceProvider;
            maintenance.PartsReplaced = request.PartsReplaced;
            maintenance.Notes = request.Notes;

            await _context.SaveChangesAsync(cancellationToken);

            return MaintenanceDto.FromEntity(maintenance);
        }
    }
}
