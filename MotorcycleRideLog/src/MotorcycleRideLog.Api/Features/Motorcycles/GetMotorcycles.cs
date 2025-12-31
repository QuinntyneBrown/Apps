// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MotorcycleRideLog.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MotorcycleRideLog.Api.Features.Motorcycles;

public class GetMotorcycles
{
    public class Query : IRequest<List<MotorcycleDto>>
    {
        public Guid UserId { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<MotorcycleDto>>
    {
        private readonly IMotorcycleRideLogContext _context;

        public Handler(IMotorcycleRideLogContext context)
        {
            _context = context;
        }

        public async Task<List<MotorcycleDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var motorcycles = await _context.Motorcycles
                .Where(x => x.UserId == request.UserId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync(cancellationToken);

            return motorcycles.Select(MotorcycleDto.FromEntity).ToList();
        }
    }
}
