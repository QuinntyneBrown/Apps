// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeEnergyUsageTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeEnergyUsageTracker.Api.Features.UtilityBills;

public class CreateUtilityBillCommand : IRequest<UtilityBillDto>
{
    public Guid UserId { get; set; }
    public UtilityType UtilityType { get; set; }
    public DateTime BillingDate { get; set; }
    public decimal Amount { get; set; }
    public decimal? UsageAmount { get; set; }
    public string? Unit { get; set; }
}

public class CreateUtilityBillCommandHandler : IRequestHandler<CreateUtilityBillCommand, UtilityBillDto>
{
    private readonly IHomeEnergyUsageTrackerContext _context;

    public CreateUtilityBillCommandHandler(IHomeEnergyUsageTrackerContext context)
    {
        _context = context;
    }

    public async Task<UtilityBillDto> Handle(CreateUtilityBillCommand request, CancellationToken cancellationToken)
    {
        var utilityBill = new UtilityBill
        {
            UtilityBillId = Guid.NewGuid(),
            UserId = request.UserId,
            UtilityType = request.UtilityType,
            BillingDate = request.BillingDate,
            Amount = request.Amount,
            UsageAmount = request.UsageAmount,
            Unit = request.Unit,
            CreatedAt = DateTime.UtcNow
        };

        _context.UtilityBills.Add(utilityBill);
        await _context.SaveChangesAsync(cancellationToken);

        return UtilityBillDto.FromUtilityBill(utilityBill);
    }
}
