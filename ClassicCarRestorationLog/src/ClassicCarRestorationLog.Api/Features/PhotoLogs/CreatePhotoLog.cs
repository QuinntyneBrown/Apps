// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ClassicCarRestorationLog.Core;
using MediatR;

namespace ClassicCarRestorationLog.Api.Features.PhotoLogs;

public class CreatePhotoLog
{
    public class Command : IRequest<PhotoLogDto>
    {
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
        public DateTime? PhotoDate { get; set; }
        public string? Description { get; set; }
        public string? PhotoUrl { get; set; }
        public ProjectPhase? Phase { get; set; }
    }

    public class Handler : IRequestHandler<Command, PhotoLogDto>
    {
        private readonly IClassicCarRestorationLogContext _context;

        public Handler(IClassicCarRestorationLogContext context)
        {
            _context = context;
        }

        public async Task<PhotoLogDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var photoLog = new PhotoLog
            {
                PhotoLogId = Guid.NewGuid(),
                UserId = request.UserId,
                ProjectId = request.ProjectId,
                PhotoDate = request.PhotoDate ?? DateTime.UtcNow,
                Description = request.Description,
                PhotoUrl = request.PhotoUrl,
                Phase = request.Phase,
                CreatedAt = DateTime.UtcNow
            };

            _context.PhotoLogs.Add(photoLog);
            await _context.SaveChangesAsync(cancellationToken);

            return PhotoLogDto.FromEntity(photoLog);
        }
    }
}
