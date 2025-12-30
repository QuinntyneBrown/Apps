// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeEnergyUsageTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeEnergyUsageTracker.Api.Features.SavingsTips;

public class DeleteSavingsTipCommand : IRequest<Unit>
{
    public Guid SavingsTipId { get; set; }
}

public class DeleteSavingsTipCommandHandler : IRequestHandler<DeleteSavingsTipCommand, Unit>
{
    private readonly IHomeEnergyUsageTrackerContext _context;

    public DeleteSavingsTipCommandHandler(IHomeEnergyUsageTrackerContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteSavingsTipCommand request, CancellationToken cancellationToken)
    {
        var savingsTip = await _context.SavingsTips
            .FirstOrDefaultAsync(x => x.SavingsTipId == request.SavingsTipId, cancellationToken);

        if (savingsTip == null)
        {
            throw new KeyNotFoundException($"SavingsTip with id {request.SavingsTipId} not found.");
        }

        _context.SavingsTips.Remove(savingsTip);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
