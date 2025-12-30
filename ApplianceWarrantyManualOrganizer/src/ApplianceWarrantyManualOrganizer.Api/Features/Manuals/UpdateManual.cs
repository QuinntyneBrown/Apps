// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ApplianceWarrantyManualOrganizer.Core;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplianceWarrantyManualOrganizer.Api.Features.Manuals;

public class UpdateManual
{
    public class Command : IRequest<ManualDto>
    {
        public Guid ManualId { get; set; }
        public string? Title { get; set; }
        public string? FileUrl { get; set; }
        public string? FileType { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.ManualId).NotEmpty();
            RuleFor(x => x.Title).MaximumLength(200);
            RuleFor(x => x.FileUrl).MaximumLength(500);
            RuleFor(x => x.FileType).MaximumLength(50);
        }
    }

    public class Handler : IRequestHandler<Command, ManualDto>
    {
        private readonly IApplianceWarrantyManualOrganizerContext _context;

        public Handler(IApplianceWarrantyManualOrganizerContext context)
        {
            _context = context;
        }

        public async Task<ManualDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var manual = await _context.Manuals
                .FirstOrDefaultAsync(m => m.ManualId == request.ManualId, cancellationToken);

            if (manual == null)
            {
                throw new KeyNotFoundException($"Manual with ID {request.ManualId} not found.");
            }

            manual.Title = request.Title;
            manual.FileUrl = request.FileUrl;
            manual.FileType = request.FileType;

            await _context.SaveChangesAsync(cancellationToken);

            return new ManualDto
            {
                ManualId = manual.ManualId,
                ApplianceId = manual.ApplianceId,
                Title = manual.Title,
                FileUrl = manual.FileUrl,
                FileType = manual.FileType,
                CreatedAt = manual.CreatedAt
            };
        }
    }
}
