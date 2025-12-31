// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MotorcycleRideLog.Core;
using MediatR;

namespace MotorcycleRideLog.Api.Features.Maintenance;

public class CreateMaintenance
{
    public class Command : IRequest<MaintenanceDto>
    {
        public Guid UserId { get; set; }
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
            var maintenance = new Core.Maintenance
            {
                MaintenanceId = Guid.NewGuid(),
                UserId = request.UserId,
                MotorcycleId = request.MotorcycleId,
                MaintenanceDate = request.MaintenanceDate,
                Type = request.Type,
                MileageAtMaintenance = request.MileageAtMaintenance,
                Description = request.Description,
                Cost = request.Cost,
                ServiceProvider = request.ServiceProvider,
                PartsReplaced = request.PartsReplaced,
                Notes = request.Notes,
                CreatedAt = DateTime.UtcNow
            };

            _context.MaintenanceRecords.Add(maintenance);
            await _context.SaveChangesAsync(cancellationToken);

            return MaintenanceDto.FromEntity(maintenance);
        }
    }
}
