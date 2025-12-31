// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MotorcycleRideLog.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MotorcycleRideLog.Api.Features.Routes;

public class GetRouteById
{
    public class Query : IRequest<RouteDto>
    {
        public Guid RouteId { get; set; }
    }

    public class Handler : IRequestHandler<Query, RouteDto>
    {
        private readonly IMotorcycleRideLogContext _context;

        public Handler(IMotorcycleRideLogContext context)
        {
            _context = context;
        }

        public async Task<RouteDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var route = await _context.Routes
                .FirstOrDefaultAsync(x => x.RouteId == request.RouteId, cancellationToken)
                ?? throw new KeyNotFoundException($"Route with ID {request.RouteId} not found.");

            return RouteDto.FromEntity(route);
        }
    }
}
