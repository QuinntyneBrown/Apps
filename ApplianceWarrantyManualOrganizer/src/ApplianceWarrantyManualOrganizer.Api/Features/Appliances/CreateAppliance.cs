// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ApplianceWarrantyManualOrganizer.Core;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplianceWarrantyManualOrganizer.Api.Features.Appliances;

public class CreateAppliance
{
    public class Command : IRequest<ApplianceDto>
    {
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public ApplianceType ApplianceType { get; set; }
        public string? Brand { get; set; }
        public string? ModelNumber { get; set; }
        public string? SerialNumber { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public decimal? PurchasePrice { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Brand).MaximumLength(100);
            RuleFor(x => x.ModelNumber).MaximumLength(100);
            RuleFor(x => x.SerialNumber).MaximumLength(100);
        }
    }

    public class Handler : IRequestHandler<Command, ApplianceDto>
    {
        private readonly IApplianceWarrantyManualOrganizerContext _context;

        public Handler(IApplianceWarrantyManualOrganizerContext context)
        {
            _context = context;
        }

        public async Task<ApplianceDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var appliance = new Appliance
            {
                ApplianceId = Guid.NewGuid(),
                UserId = request.UserId,
                Name = request.Name,
                ApplianceType = request.ApplianceType,
                Brand = request.Brand,
                ModelNumber = request.ModelNumber,
                SerialNumber = request.SerialNumber,
                PurchaseDate = request.PurchaseDate,
                PurchasePrice = request.PurchasePrice,
                CreatedAt = DateTime.UtcNow
            };

            _context.Appliances.Add(appliance);
            await _context.SaveChangesAsync(cancellationToken);

            return new ApplianceDto
            {
                ApplianceId = appliance.ApplianceId,
                UserId = appliance.UserId,
                Name = appliance.Name,
                ApplianceType = appliance.ApplianceType,
                Brand = appliance.Brand,
                ModelNumber = appliance.ModelNumber,
                SerialNumber = appliance.SerialNumber,
                PurchaseDate = appliance.PurchaseDate,
                PurchasePrice = appliance.PurchasePrice,
                CreatedAt = appliance.CreatedAt
            };
        }
    }
}
