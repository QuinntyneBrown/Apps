// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeEnergyUsageTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeEnergyUsageTracker.Api.Features.Usages;

public class DeleteUsageCommand : IRequest<Unit>
{
    public Guid UsageId { get; set; }
}

public class DeleteUsageCommandHandler : IRequestHandler<DeleteUsageCommand, Unit>
{
    private readonly IHomeEnergyUsageTrackerContext _context;

    public DeleteUsageCommandHandler(IHomeEnergyUsageTrackerContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteUsageCommand request, CancellationToken cancellationToken)
    {
        var usage = await _context.Usages
            .FirstOrDefaultAsync(x => x.UsageId == request.UsageId, cancellationToken);

        if (usage == null)
        {
            throw new KeyNotFoundException($"Usage with id {request.UsageId} not found.");
        }

        _context.Usages.Remove(usage);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
