// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MotorcycleRideLog.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MotorcycleRideLog.Api.Features.Motorcycles;

public class UpdateMotorcycle
{
    public class Command : IRequest<MotorcycleDto>
    {
        public Guid MotorcycleId { get; set; }
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int? Year { get; set; }
        public MotorcycleType Type { get; set; }
        public string? VIN { get; set; }
        public string? LicensePlate { get; set; }
        public int? CurrentMileage { get; set; }
        public string? Color { get; set; }
        public string? Notes { get; set; }
    }

    public class Handler : IRequestHandler<Command, MotorcycleDto>
    {
        private readonly IMotorcycleRideLogContext _context;

        public Handler(IMotorcycleRideLogContext context)
        {
            _context = context;
        }

        public async Task<MotorcycleDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var motorcycle = await _context.Motorcycles
                .FirstOrDefaultAsync(x => x.MotorcycleId == request.MotorcycleId, cancellationToken)
                ?? throw new KeyNotFoundException($"Motorcycle with ID {request.MotorcycleId} not found.");

            motorcycle.Make = request.Make;
            motorcycle.Model = request.Model;
            motorcycle.Year = request.Year;
            motorcycle.Type = request.Type;
            motorcycle.VIN = request.VIN;
            motorcycle.LicensePlate = request.LicensePlate;
            motorcycle.CurrentMileage = request.CurrentMileage;
            motorcycle.Color = request.Color;
            motorcycle.Notes = request.Notes;

            await _context.SaveChangesAsync(cancellationToken);

            return MotorcycleDto.FromEntity(motorcycle);
        }
    }
}
