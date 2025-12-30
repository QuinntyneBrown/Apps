// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ClassicCarRestorationLog.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClassicCarRestorationLog.Api.Features.Parts;

public class DeletePart
{
    public class Command : IRequest<bool>
    {
        public Guid PartId { get; set; }
    }

    public class Handler : IRequestHandler<Command, bool>
    {
        private readonly IClassicCarRestorationLogContext _context;

        public Handler(IClassicCarRestorationLogContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var part = await _context.Parts
                .FirstOrDefaultAsync(p => p.PartId == request.PartId, cancellationToken);

            if (part == null)
            {
                return false;
            }

            _context.Parts.Remove(part);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
