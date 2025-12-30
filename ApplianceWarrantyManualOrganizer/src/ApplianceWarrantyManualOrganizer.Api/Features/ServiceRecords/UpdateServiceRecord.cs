// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ApplianceWarrantyManualOrganizer.Core;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplianceWarrantyManualOrganizer.Api.Features.ServiceRecords;

public class UpdateServiceRecord
{
    public class Command : IRequest<ServiceRecordDto>
    {
        public Guid ServiceRecordId { get; set; }
        public DateTime ServiceDate { get; set; }
        public string? ServiceProvider { get; set; }
        public string? Description { get; set; }
        public decimal? Cost { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.ServiceRecordId).NotEmpty();
            RuleFor(x => x.ServiceDate).NotEmpty();
            RuleFor(x => x.ServiceProvider).MaximumLength(200);
            RuleFor(x => x.Description).MaximumLength(2000);
            RuleFor(x => x.Cost).GreaterThanOrEqualTo(0).When(x => x.Cost.HasValue);
        }
    }

    public class Handler : IRequestHandler<Command, ServiceRecordDto>
    {
        private readonly IApplianceWarrantyManualOrganizerContext _context;

        public Handler(IApplianceWarrantyManualOrganizerContext context)
        {
            _context = context;
        }

        public async Task<ServiceRecordDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var serviceRecord = await _context.ServiceRecords
                .FirstOrDefaultAsync(sr => sr.ServiceRecordId == request.ServiceRecordId, cancellationToken);

            if (serviceRecord == null)
            {
                throw new KeyNotFoundException($"ServiceRecord with ID {request.ServiceRecordId} not found.");
            }

            serviceRecord.ServiceDate = request.ServiceDate;
            serviceRecord.ServiceProvider = request.ServiceProvider;
            serviceRecord.Description = request.Description;
            serviceRecord.Cost = request.Cost;

            await _context.SaveChangesAsync(cancellationToken);

            return new ServiceRecordDto
            {
                ServiceRecordId = serviceRecord.ServiceRecordId,
                ApplianceId = serviceRecord.ApplianceId,
                ServiceDate = serviceRecord.ServiceDate,
                ServiceProvider = serviceRecord.ServiceProvider,
                Description = serviceRecord.Description,
                Cost = serviceRecord.Cost,
                CreatedAt = serviceRecord.CreatedAt
            };
        }
    }
}
