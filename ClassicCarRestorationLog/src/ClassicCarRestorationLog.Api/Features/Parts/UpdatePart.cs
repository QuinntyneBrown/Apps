// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ClassicCarRestorationLog.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClassicCarRestorationLog.Api.Features.Parts;

public class UpdatePart
{
    public class Command : IRequest<PartDto?>
    {
        public Guid PartId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? PartNumber { get; set; }
        public string? Supplier { get; set; }
        public decimal? Cost { get; set; }
        public DateTime? OrderedDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public bool IsInstalled { get; set; }
        public string? Notes { get; set; }
    }

    public class Handler : IRequestHandler<Command, PartDto?>
    {
        private readonly IClassicCarRestorationLogContext _context;

        public Handler(IClassicCarRestorationLogContext context)
        {
            _context = context;
        }

        public async Task<PartDto?> Handle(Command request, CancellationToken cancellationToken)
        {
            var part = await _context.Parts
                .FirstOrDefaultAsync(p => p.PartId == request.PartId, cancellationToken);

            if (part == null)
            {
                return null;
            }

            part.Name = request.Name;
            part.PartNumber = request.PartNumber;
            part.Supplier = request.Supplier;
            part.Cost = request.Cost;
            part.OrderedDate = request.OrderedDate;
            part.ReceivedDate = request.ReceivedDate;
            part.IsInstalled = request.IsInstalled;
            part.Notes = request.Notes;

            await _context.SaveChangesAsync(cancellationToken);

            return PartDto.FromEntity(part);
        }
    }
}
