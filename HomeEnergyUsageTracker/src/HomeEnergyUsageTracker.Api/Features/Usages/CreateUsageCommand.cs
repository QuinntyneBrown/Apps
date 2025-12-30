// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeEnergyUsageTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeEnergyUsageTracker.Api.Features.Usages;

public class CreateUsageCommand : IRequest<UsageDto>
{
    public Guid UtilityBillId { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
}

public class CreateUsageCommandHandler : IRequestHandler<CreateUsageCommand, UsageDto>
{
    private readonly IHomeEnergyUsageTrackerContext _context;

    public CreateUsageCommandHandler(IHomeEnergyUsageTrackerContext context)
    {
        _context = context;
    }

    public async Task<UsageDto> Handle(CreateUsageCommand request, CancellationToken cancellationToken)
    {
        var usage = new Usage
        {
            UsageId = Guid.NewGuid(),
            UtilityBillId = request.UtilityBillId,
            Date = request.Date,
            Amount = request.Amount,
            CreatedAt = DateTime.UtcNow
        };

        _context.Usages.Add(usage);
        await _context.SaveChangesAsync(cancellationToken);

        return UsageDto.FromUsage(usage);
    }
}
