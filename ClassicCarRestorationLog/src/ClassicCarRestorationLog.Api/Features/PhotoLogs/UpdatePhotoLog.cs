// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ClassicCarRestorationLog.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClassicCarRestorationLog.Api.Features.PhotoLogs;

public class UpdatePhotoLog
{
    public class Command : IRequest<PhotoLogDto?>
    {
        public Guid PhotoLogId { get; set; }
        public DateTime PhotoDate { get; set; }
        public string? Description { get; set; }
        public string? PhotoUrl { get; set; }
        public ProjectPhase? Phase { get; set; }
    }

    public class Handler : IRequestHandler<Command, PhotoLogDto?>
    {
        private readonly IClassicCarRestorationLogContext _context;

        public Handler(IClassicCarRestorationLogContext context)
        {
            _context = context;
        }

        public async Task<PhotoLogDto?> Handle(Command request, CancellationToken cancellationToken)
        {
            var photoLog = await _context.PhotoLogs
                .FirstOrDefaultAsync(p => p.PhotoLogId == request.PhotoLogId, cancellationToken);

            if (photoLog == null)
            {
                return null;
            }

            photoLog.PhotoDate = request.PhotoDate;
            photoLog.Description = request.Description;
            photoLog.PhotoUrl = request.PhotoUrl;
            photoLog.Phase = request.Phase;

            await _context.SaveChangesAsync(cancellationToken);

            return PhotoLogDto.FromEntity(photoLog);
        }
    }
}
