// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeEnergyUsageTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeEnergyUsageTracker.Api.Features.UtilityBills;

public class DeleteUtilityBillCommand : IRequest<Unit>
{
    public Guid UtilityBillId { get; set; }
}

public class DeleteUtilityBillCommandHandler : IRequestHandler<DeleteUtilityBillCommand, Unit>
{
    private readonly IHomeEnergyUsageTrackerContext _context;

    public DeleteUtilityBillCommandHandler(IHomeEnergyUsageTrackerContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteUtilityBillCommand request, CancellationToken cancellationToken)
    {
        var utilityBill = await _context.UtilityBills
            .FirstOrDefaultAsync(x => x.UtilityBillId == request.UtilityBillId, cancellationToken);

        if (utilityBill == null)
        {
            throw new KeyNotFoundException($"UtilityBill with id {request.UtilityBillId} not found.");
        }

        _context.UtilityBills.Remove(utilityBill);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
