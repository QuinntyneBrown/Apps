// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MotorcycleRideLog.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MotorcycleRideLog.Api.Features.Motorcycles;

public class DeleteMotorcycle
{
    public class Command : IRequest<Unit>
    {
        public Guid MotorcycleId { get; set; }
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
            var motorcycle = await _context.Motorcycles
                .FirstOrDefaultAsync(x => x.MotorcycleId == request.MotorcycleId, cancellationToken)
                ?? throw new KeyNotFoundException($"Motorcycle with ID {request.MotorcycleId} not found.");

            _context.Motorcycles.Remove(motorcycle);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
