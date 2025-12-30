// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ClassicCarRestorationLog.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClassicCarRestorationLog.Api.Features.PhotoLogs;

public class GetPhotoLogById
{
    public class Query : IRequest<PhotoLogDto?>
    {
        public Guid PhotoLogId { get; set; }
    }

    public class Handler : IRequestHandler<Query, PhotoLogDto?>
    {
        private readonly IClassicCarRestorationLogContext _context;

        public Handler(IClassicCarRestorationLogContext context)
        {
            _context = context;
        }

        public async Task<PhotoLogDto?> Handle(Query request, CancellationToken cancellationToken)
        {
            var photoLog = await _context.PhotoLogs
                .FirstOrDefaultAsync(p => p.PhotoLogId == request.PhotoLogId, cancellationToken);

            return photoLog == null ? null : PhotoLogDto.FromEntity(photoLog);
        }
    }
}
