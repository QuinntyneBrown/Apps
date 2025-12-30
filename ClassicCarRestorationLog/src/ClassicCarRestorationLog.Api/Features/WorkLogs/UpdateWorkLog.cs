// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ClassicCarRestorationLog.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClassicCarRestorationLog.Api.Features.WorkLogs;

public class UpdateWorkLog
{
    public class Command : IRequest<WorkLogDto?>
    {
        public Guid WorkLogId { get; set; }
        public DateTime WorkDate { get; set; }
        public int HoursWorked { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? WorkPerformed { get; set; }
    }

    public class Handler : IRequestHandler<Command, WorkLogDto?>
    {
        private readonly IClassicCarRestorationLogContext _context;

        public Handler(IClassicCarRestorationLogContext context)
        {
            _context = context;
        }

        public async Task<WorkLogDto?> Handle(Command request, CancellationToken cancellationToken)
        {
            var workLog = await _context.WorkLogs
                .FirstOrDefaultAsync(w => w.WorkLogId == request.WorkLogId, cancellationToken);

            if (workLog == null)
            {
                return null;
            }

            workLog.WorkDate = request.WorkDate;
            workLog.HoursWorked = request.HoursWorked;
            workLog.Description = request.Description;
            workLog.WorkPerformed = request.WorkPerformed;

            await _context.SaveChangesAsync(cancellationToken);

            return WorkLogDto.FromEntity(workLog);
        }
    }
}
