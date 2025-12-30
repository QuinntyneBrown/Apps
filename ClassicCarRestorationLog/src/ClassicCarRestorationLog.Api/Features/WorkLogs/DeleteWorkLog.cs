// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ClassicCarRestorationLog.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClassicCarRestorationLog.Api.Features.WorkLogs;

public class DeleteWorkLog
{
    public class Command : IRequest<bool>
    {
        public Guid WorkLogId { get; set; }
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
            var workLog = await _context.WorkLogs
                .FirstOrDefaultAsync(w => w.WorkLogId == request.WorkLogId, cancellationToken);

            if (workLog == null)
            {
                return false;
            }

            _context.WorkLogs.Remove(workLog);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
