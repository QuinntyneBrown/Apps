// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeEnergyUsageTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeEnergyUsageTracker.Api.Features.Usages;

public class UpdateUsageCommand : IRequest<UsageDto>
{
    public Guid UsageId { get; set; }
    public Guid UtilityBillId { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
}

public class UpdateUsageCommandHandler : IRequestHandler<UpdateUsageCommand, UsageDto>
{
    private readonly IHomeEnergyUsageTrackerContext _context;

    public UpdateUsageCommandHandler(IHomeEnergyUsageTrackerContext context)
    {
        _context = context;
    }

    public async Task<UsageDto> Handle(UpdateUsageCommand request, CancellationToken cancellationToken)
    {
        var usage = await _context.Usages
            .FirstOrDefaultAsync(x => x.UsageId == request.UsageId, cancellationToken);

        if (usage == null)
        {
            throw new KeyNotFoundException($"Usage with id {request.UsageId} not found.");
        }

        usage.UtilityBillId = request.UtilityBillId;
        usage.Date = request.Date;
        usage.Amount = request.Amount;

        await _context.SaveChangesAsync(cancellationToken);

        return UsageDto.FromUsage(usage);
    }
}
