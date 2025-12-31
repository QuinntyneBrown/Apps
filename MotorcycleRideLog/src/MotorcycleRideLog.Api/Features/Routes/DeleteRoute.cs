// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MotorcycleRideLog.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MotorcycleRideLog.Api.Features.Routes;

public class DeleteRoute
{
    public class Command : IRequest<Unit>
    {
        public Guid RouteId { get; set; }
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
            var route = await _context.Routes
                .FirstOrDefaultAsync(x => x.RouteId == request.RouteId, cancellationToken)
                ?? throw new KeyNotFoundException($"Route with ID {request.RouteId} not found.");

            _context.Routes.Remove(route);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
