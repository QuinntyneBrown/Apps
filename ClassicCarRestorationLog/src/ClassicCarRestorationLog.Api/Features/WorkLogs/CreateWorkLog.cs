// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ClassicCarRestorationLog.Core;
using MediatR;

namespace ClassicCarRestorationLog.Api.Features.WorkLogs;

public class CreateWorkLog
{
    public class Command : IRequest<WorkLogDto>
    {
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
        public DateTime? WorkDate { get; set; }
        public int HoursWorked { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? WorkPerformed { get; set; }
    }

    public class Handler : IRequestHandler<Command, WorkLogDto>
    {
        private readonly IClassicCarRestorationLogContext _context;

        public Handler(IClassicCarRestorationLogContext context)
        {
            _context = context;
        }

        public async Task<WorkLogDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var workLog = new WorkLog
            {
                WorkLogId = Guid.NewGuid(),
                UserId = request.UserId,
                ProjectId = request.ProjectId,
                WorkDate = request.WorkDate ?? DateTime.UtcNow,
                HoursWorked = request.HoursWorked,
                Description = request.Description,
                WorkPerformed = request.WorkPerformed,
                CreatedAt = DateTime.UtcNow
            };

            _context.WorkLogs.Add(workLog);
            await _context.SaveChangesAsync(cancellationToken);

            return WorkLogDto.FromEntity(workLog);
        }
    }
}
