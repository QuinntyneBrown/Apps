// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MotorcycleRideLog.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MotorcycleRideLog.Api.Features.Rides;

public class DeleteRide
{
    public class Command : IRequest<Unit>
    {
        public Guid RideId { get; set; }
    }

    public class Handler : IRequestHandler<Command, Unit>
    {
        private readonly IMotorcycleRideLogContext _context;

        public Handler(IMotorcycleRideLogContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var ride = await _context.Rides
                .FirstOrDefaultAsync(x => x.RideId == request.RideId, cancellationToken)
                ?? throw new KeyNotFoundException($"Ride with ID {request.RideId} not found.");

            _context.Rides.Remove(ride);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
