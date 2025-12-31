// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MotorcycleRideLog.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MotorcycleRideLog.Api.Features.Maintenance;

public class GetMaintenance
{
    public class Query : IRequest<List<MaintenanceDto>>
    {
        public Guid UserId { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<MaintenanceDto>>
    {
        private readonly IMotorcycleRideLogContext _context;

        public Handler(IMotorcycleRideLogContext context)
        {
            _context = context;
        }

        public async Task<List<MaintenanceDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var maintenanceRecords = await _context.MaintenanceRecords
                .Where(x => x.UserId == request.UserId)
                .OrderByDescending(x => x.MaintenanceDate)
                .ToListAsync(cancellationToken);

            return maintenanceRecords.Select(MaintenanceDto.FromEntity).ToList();
        }
    }
}
