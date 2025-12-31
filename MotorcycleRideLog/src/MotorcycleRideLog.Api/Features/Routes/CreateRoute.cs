// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MotorcycleRideLog.Core;
using MediatR;

namespace MotorcycleRideLog.Api.Features.Routes;

public class CreateRoute
{
    public class Command : IRequest<RouteDto>
    {
        public Guid UserId { get; set; }
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
            var route = new Core.Route
            {
                RouteId = Guid.NewGuid(),
                UserId = request.UserId,
                Name = request.Name,
                Description = request.Description,
                StartLocation = request.StartLocation,
                EndLocation = request.EndLocation,
                DistanceMiles = request.DistanceMiles,
                Waypoints = request.Waypoints,
                EstimatedDurationMinutes = request.EstimatedDurationMinutes,
                Difficulty = request.Difficulty,
                IsFavorite = request.IsFavorite,
                Notes = request.Notes,
                CreatedAt = DateTime.UtcNow
            };

            _context.Routes.Add(route);
            await _context.SaveChangesAsync(cancellationToken);

            return RouteDto.FromEntity(route);
        }
    }
}
