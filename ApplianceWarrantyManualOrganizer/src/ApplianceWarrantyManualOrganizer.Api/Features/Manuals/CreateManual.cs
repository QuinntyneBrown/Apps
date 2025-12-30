// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ApplianceWarrantyManualOrganizer.Core;
using FluentValidation;
using MediatR;

namespace ApplianceWarrantyManualOrganizer.Api.Features.Manuals;

public class CreateManual
{
    public class Command : IRequest<ManualDto>
    {
        public Guid ApplianceId { get; set; }
        public string? Title { get; set; }
        public string? FileUrl { get; set; }
        public string? FileType { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.ApplianceId).NotEmpty();
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
            var manual = new Manual
            {
                ManualId = Guid.NewGuid(),
                ApplianceId = request.ApplianceId,
                Title = request.Title,
                FileUrl = request.FileUrl,
                FileType = request.FileType,
                CreatedAt = DateTime.UtcNow
            };

            _context.Manuals.Add(manual);
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
