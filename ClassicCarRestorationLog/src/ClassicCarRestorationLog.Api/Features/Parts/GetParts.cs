// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ClassicCarRestorationLog.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClassicCarRestorationLog.Api.Features.Parts;

public class GetParts
{
    public class Query : IRequest<List<PartDto>>
    {
        public Guid? ProjectId { get; set; }
        public Guid? UserId { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<PartDto>>
    {
        private readonly IClassicCarRestorationLogContext _context;

        public Handler(IClassicCarRestorationLogContext context)
        {
            _context = context;
        }

        public async Task<List<PartDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _context.Parts.AsQueryable();

            if (request.ProjectId.HasValue)
            {
                query = query.Where(p => p.ProjectId == request.ProjectId.Value);
            }

            if (request.UserId.HasValue)
            {
                query = query.Where(p => p.UserId == request.UserId.Value);
            }

            var parts = await query
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync(cancellationToken);

            return parts.Select(PartDto.FromEntity).ToList();
        }
    }
}
