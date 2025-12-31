// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MotorcycleRideLog.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MotorcycleRideLog.Api.Features.Routes;

public class UpdateRoute
{
    public class Command : IRequest<RouteDto>
    {
        public Guid RouteId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? StartLocation { get; set; }
        public string? EndLocation { get; set; }
        public decimal? DistanceMiles { get; set; }
        public string? Waypoints { get; set; }
        public int? EstimatedDurationMinutes { get; set; }
        public string? Difficulty { get; set; }
        public bool IsFavorite { get; set; }
        public string? Notes { get; set; }
    }

    public class Handler : IRequestHandler<Command, RouteDto>
    {
        private readonly IMotorcycleRideLogContext _context;

        public Handler(IMotorcycleRideLogContext context)
        {
            _context = context;
        }

        public async Task<RouteDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var route = await _context.Routes
                .FirstOrDefaultAsync(x => x.RouteId == request.RouteId, cancellationToken)
                ?? throw new KeyNotFoundException($"Route with ID {request.RouteId} not found.");

            route.Name = request.Name;
            route.Description = request.Description;
            route.StartLocation = request.StartLocation;
            route.EndLocation = request.EndLocation;
            route.DistanceMiles = request.DistanceMiles;
            route.Waypoints = request.Waypoints;
            route.EstimatedDurationMinutes = request.EstimatedDurationMinutes;
            route.Difficulty = request.Difficulty;
            route.IsFavorite = request.IsFavorite;
            route.Notes = request.Notes;

            await _context.SaveChangesAsync(cancellationToken);

            return RouteDto.FromEntity(route);
        }
    }
}
