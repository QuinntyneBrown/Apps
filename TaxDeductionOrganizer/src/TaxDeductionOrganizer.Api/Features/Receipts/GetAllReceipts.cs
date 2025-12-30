// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MediatR;
using Microsoft.EntityFrameworkCore;
using TaxDeductionOrganizer.Core;

namespace TaxDeductionOrganizer.Api.Features.Receipts;

public class GetAllReceipts
{
    public class Query : IRequest<List<ReceiptDto>>
    {
        public Guid? DeductionId { get; set; }
    }

    public class Handler : IRequestHandler<Query, List<ReceiptDto>>
    {
        private readonly ITaxDeductionOrganizerContext _context;

        public Handler(ITaxDeductionOrganizerContext context)
        {
            _context = context;
        }

        public async Task<List<ReceiptDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _context.Receipts.AsQueryable();

            if (request.DeductionId.HasValue)
            {
                query = query.Where(r => r.DeductionId == request.DeductionId.Value);
            }

            var receipts = await query
                .OrderByDescending(r => r.UploadDate)
                .ToListAsync(cancellationToken);

            return receipts.Select(r => r.ToDto()).ToList();
        }
    }
}
