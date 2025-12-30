// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ClassicCarRestorationLog.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClassicCarRestorationLog.Api.Features.PhotoLogs;

public class GetPhotoLogs
{
    public class Query : IRequest<List<PhotoLogDto>>
    {
        public Guid? ProjectId { get; set; }
        public Guid? UserId { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<PhotoLogDto>>
    {
        private readonly IClassicCarRestorationLogContext _context;

        public Handler(IClassicCarRestorationLogContext context)
        {
            _context = context;
        }

        public async Task<List<PhotoLogDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _context.PhotoLogs.AsQueryable();

            if (request.ProjectId.HasValue)
            {
                query = query.Where(p => p.ProjectId == request.ProjectId.Value);
            }

            if (request.UserId.HasValue)
            {
                query = query.Where(p => p.UserId == request.UserId.Value);
            }

            var photoLogs = await query
                .OrderByDescending(p => p.PhotoDate)
                .ToListAsync(cancellationToken);

            return photoLogs.Select(PhotoLogDto.FromEntity).ToList();
        }
    }
}
