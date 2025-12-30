// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeEnergyUsageTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeEnergyUsageTracker.Api.Features.UtilityBills;

public class UpdateUtilityBillCommand : IRequest<UtilityBillDto>
{
    public Guid UtilityBillId { get; set; }
    public Guid UserId { get; set; }
    public UtilityType UtilityType { get; set; }
    public DateTime BillingDate { get; set; }
    public decimal Amount { get; set; }
    public decimal? UsageAmount { get; set; }
    public string? Unit { get; set; }
}

public class UpdateUtilityBillCommandHandler : IRequestHandler<UpdateUtilityBillCommand, UtilityBillDto>
{
    private readonly IHomeEnergyUsageTrackerContext _context;

    public UpdateUtilityBillCommandHandler(IHomeEnergyUsageTrackerContext context)
    {
        _context = context;
    }

    public async Task<UtilityBillDto> Handle(UpdateUtilityBillCommand request, CancellationToken cancellationToken)
    {
        var utilityBill = await _context.UtilityBills
            .FirstOrDefaultAsync(x => x.UtilityBillId == request.UtilityBillId, cancellationToken);

        if (utilityBill == null)
        {
            throw new KeyNotFoundException($"UtilityBill with id {request.UtilityBillId} not found.");
        }

        utilityBill.UserId = request.UserId;
        utilityBill.UtilityType = request.UtilityType;
        utilityBill.BillingDate = request.BillingDate;
        utilityBill.Amount = request.Amount;
        utilityBill.UsageAmount = request.UsageAmount;
        utilityBill.Unit = request.Unit;

        await _context.SaveChangesAsync(cancellationToken);

        return UtilityBillDto.FromUtilityBill(utilityBill);
    }
}
