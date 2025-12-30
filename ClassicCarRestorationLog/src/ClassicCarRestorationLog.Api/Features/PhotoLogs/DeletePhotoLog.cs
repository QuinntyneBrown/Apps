// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ClassicCarRestorationLog.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClassicCarRestorationLog.Api.Features.PhotoLogs;

public class DeletePhotoLog
{
    public class Command : IRequest<bool>
    {
        public Guid PhotoLogId { get; set; }
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
            var photoLog = await _context.PhotoLogs
                .FirstOrDefaultAsync(p => p.PhotoLogId == request.PhotoLogId, cancellationToken);

            if (photoLog == null)
            {
                return false;
            }

            _context.PhotoLogs.Remove(photoLog);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
