// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MotorcycleRideLog.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MotorcycleRideLog.Api.Features.Rides;

public class GetRides
{
    public class Query : IRequest<List<RideDto>>
    {
        public Guid UserId { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<RideDto>>
    {
        private readonly IMotorcycleRideLogContext _context;

        public Handler(IMotorcycleRideLogContext context)
        {
            _context = context;
        }

        public async Task<List<RideDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var rides = await _context.Rides
                .Where(x => x.UserId == request.UserId)
                .OrderByDescending(x => x.RideDate)
                .ToListAsync(cancellationToken);

            return rides.Select(RideDto.FromEntity).ToList();
        }
    }
}
