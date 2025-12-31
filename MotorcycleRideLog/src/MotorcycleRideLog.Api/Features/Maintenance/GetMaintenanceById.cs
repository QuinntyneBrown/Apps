// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MotorcycleRideLog.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MotorcycleRideLog.Api.Features.Maintenance;

public class GetMaintenanceById
{
    public class Query : IRequest<MaintenanceDto>
    {
        public Guid MaintenanceId { get; set; }
    }

    public class Handler : IRequestHandler<Query, MaintenanceDto>
    {
        private readonly IMotorcycleRideLogContext _context;

        public Handler(IMotorcycleRideLogContext context)
        {
            _context = context;
        }

        public async Task<MaintenanceDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var maintenance = await _context.MaintenanceRecords
                .FirstOrDefaultAsync(x => x.MaintenanceId == request.MaintenanceId, cancellationToken)
                ?? throw new KeyNotFoundException($"Maintenance with ID {request.MaintenanceId} not found.");

            return MaintenanceDto.FromEntity(maintenance);
        }
    }
}
