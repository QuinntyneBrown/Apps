// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ClassicCarRestorationLog.Core;
using MediatR;

namespace ClassicCarRestorationLog.Api.Features.Parts;

public class CreatePart
{
    public class Command : IRequest<PartDto>
    {
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? PartNumber { get; set; }
        public string? Supplier { get; set; }
        public decimal? Cost { get; set; }
        public DateTime? OrderedDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public bool IsInstalled { get; set; }
        public string? Notes { get; set; }
    }

    public class Handler : IRequestHandler<Command, PartDto>
    {
        private readonly IClassicCarRestorationLogContext _context;

        public Handler(IClassicCarRestorationLogContext context)
        {
            _context = context;
        }

        public async Task<PartDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var part = new Part
            {
                PartId = Guid.NewGuid(),
                UserId = request.UserId,
                ProjectId = request.ProjectId,
                Name = request.Name,
                PartNumber = request.PartNumber,
                Supplier = request.Supplier,
                Cost = request.Cost,
                OrderedDate = request.OrderedDate,
                ReceivedDate = request.ReceivedDate,
                IsInstalled = request.IsInstalled,
                Notes = request.Notes,
                CreatedAt = DateTime.UtcNow
            };

            _context.Parts.Add(part);
            await _context.SaveChangesAsync(cancellationToken);

            return PartDto.FromEntity(part);
        }
    }
}
