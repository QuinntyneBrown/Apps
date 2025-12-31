// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MotorcycleRideLog.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MotorcycleRideLog.Api.Features.Routes;

public class GetRoutes
{
    public class Query : IRequest<List<RouteDto>>
    {
        public Guid UserId { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<RouteDto>>
    {
        private readonly IMotorcycleRideLogContext _context;

        public Handler(IMotorcycleRideLogContext context)
        {
            _context = context;
        }

        public async Task<List<RouteDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var routes = await _context.Routes
                .Where(x => x.UserId == request.UserId)
                .OrderByDescending(x => x.IsFavorite)
                .ThenBy(x => x.Name)
                .ToListAsync(cancellationToken);

            return routes.Select(RouteDto.FromEntity).ToList();
        }
    }
}
