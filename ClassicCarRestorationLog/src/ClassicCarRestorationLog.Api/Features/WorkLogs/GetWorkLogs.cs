// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ClassicCarRestorationLog.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClassicCarRestorationLog.Api.Features.WorkLogs;

public class GetWorkLogs
{
    public class Query : IRequest<List<WorkLogDto>>
    {
        public Guid? ProjectId { get; set; }
        public Guid? UserId { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<WorkLogDto>>
    {
        private readonly IClassicCarRestorationLogContext _context;

        public Handler(IClassicCarRestorationLogContext context)
        {
            _context = context;
        }

        public async Task<List<WorkLogDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _context.WorkLogs.AsQueryable();

            if (request.ProjectId.HasValue)
            {
                query = query.Where(w => w.ProjectId == request.ProjectId.Value);
            }

            if (request.UserId.HasValue)
            {
                query = query.Where(w => w.UserId == request.UserId.Value);
            }

            var workLogs = await query
                .OrderByDescending(w => w.WorkDate)
                .ToListAsync(cancellationToken);

            return workLogs.Select(WorkLogDto.FromEntity).ToList();
        }
    }
}
