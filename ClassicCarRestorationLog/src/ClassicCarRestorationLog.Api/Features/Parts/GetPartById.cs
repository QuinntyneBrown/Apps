// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ClassicCarRestorationLog.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClassicCarRestorationLog.Api.Features.Parts;

public class GetPartById
{
    public class Query : IRequest<PartDto?>
    {
        public Guid PartId { get; set; }
    }

    public class Handler : IRequestHandler<Query, PartDto?>
    {
        private readonly IClassicCarRestorationLogContext _context;

        public Handler(IClassicCarRestorationLogContext context)
        {
            _context = context;
        }

        public async Task<PartDto?> Handle(Query request, CancellationToken cancellationToken)
        {
            var part = await _context.Parts
                .FirstOrDefaultAsync(p => p.PartId == request.PartId, cancellationToken);

            return part == null ? null : PartDto.FromEntity(part);
        }
    }
}
