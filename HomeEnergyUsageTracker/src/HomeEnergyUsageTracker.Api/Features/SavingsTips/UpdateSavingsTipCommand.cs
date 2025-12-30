// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HomeEnergyUsageTracker.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HomeEnergyUsageTracker.Api.Features.SavingsTips;

public class UpdateSavingsTipCommand : IRequest<SavingsTipDto>
{
    public Guid SavingsTipId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class UpdateSavingsTipCommandHandler : IRequestHandler<UpdateSavingsTipCommand, SavingsTipDto>
{
    private readonly IHomeEnergyUsageTrackerContext _context;

    public UpdateSavingsTipCommandHandler(IHomeEnergyUsageTrackerContext context)
    {
        _context = context;
    }

    public async Task<SavingsTipDto> Handle(UpdateSavingsTipCommand request, CancellationToken cancellationToken)
    {
        var savingsTip = await _context.SavingsTips
            .FirstOrDefaultAsync(x => x.SavingsTipId == request.SavingsTipId, cancellationToken);

        if (savingsTip == null)
        {
            throw new KeyNotFoundException($"SavingsTip with id {request.SavingsTipId} not found.");
        }

        savingsTip.Title = request.Title;
        savingsTip.Description = request.Description;

        await _context.SaveChangesAsync(cancellationToken);

        return SavingsTipDto.FromSavingsTip(savingsTip);
    }
}
