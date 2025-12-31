// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MotorcycleRideLog.Core;
using MediatR;

namespace MotorcycleRideLog.Api.Features.Motorcycles;

public class CreateMotorcycle
{
    public class Command : IRequest<MotorcycleDto>
    {
        public Guid UserId { get; set; }
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
            var motorcycle = new Motorcycle
            {
                MotorcycleId = Guid.NewGuid(),
                UserId = request.UserId,
                Make = request.Make,
                Model = request.Model,
                Year = request.Year,
                Type = request.Type,
                VIN = request.VIN,
                LicensePlate = request.LicensePlate,
                CurrentMileage = request.CurrentMileage,
                Color = request.Color,
                Notes = request.Notes,
                CreatedAt = DateTime.UtcNow
            };

            _context.Motorcycles.Add(motorcycle);
            await _context.SaveChangesAsync(cancellationToken);

            return MotorcycleDto.FromEntity(motorcycle);
        }
    }
}
