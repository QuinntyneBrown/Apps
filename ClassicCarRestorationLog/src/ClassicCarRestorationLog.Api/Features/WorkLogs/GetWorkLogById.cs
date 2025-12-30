// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ClassicCarRestorationLog.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClassicCarRestorationLog.Api.Features.WorkLogs;

public class GetWorkLogById
{
    public class Query : IRequest<WorkLogDto?>
    {
        public Guid WorkLogId { get; set; }
    }

    public class Handler : IRequestHandler<Query, WorkLogDto?>
    {
        private readonly IClassicCarRestorationLogContext _context;

        public Handler(IClassicCarRestorationLogContext context)
        {
            _context = context;
        }

        public async Task<WorkLogDto?> Handle(Query request, CancellationToken cancellationToken)
        {
            var workLog = await _context.WorkLogs
                .FirstOrDefaultAsync(w => w.WorkLogId == request.WorkLogId, cancellationToken);

            return workLog == null ? null : WorkLogDto.FromEntity(workLog);
        }
    }
}
